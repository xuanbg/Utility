using System;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class TokenHelper
    {
        private bool isAutoRefres;
        private string accessToken;
        private string refreshToken;
        private DateTime expiryTime;
        private DateTime failureTime;

        /// <summary>
        /// AccessToken字符串
        /// </summary>
        public string token
        {
            get
            {
                var now = DateTime.Now;
                if (string.IsNullOrEmpty(accessToken) || now > failureTime)
                {
                    GetTokens();
                }
                else if (!isAutoRefres && now > expiryTime)
                {
                    RefresTokens();
                }

                return accessToken;
            }
        }

        /// <summary>
        /// 用户签名
        /// </summary>
        public string sign { get; private set; }

        /// <summary>
        /// 当前连接基础应用服务器
        /// </summary>
        public string baseServer { get; set; } = Util.GetAppSetting("BaseServer");

        /// <summary>
        /// 租户ID
        /// </summary>
        public string tenantId { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 登录部门ID
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="secret">用户密钥</param>
        public void Signature(string secret)
        {
            sign = Util.Hash(account + Util.Hash(secret));
        }

        /// <summary>
        /// 获取令牌
        /// </summary>
        public void GetTokens()
        {
            accessToken = null;
            refreshToken = null;

            var code = GetCode();
            if (code == null) return;

            var key = Util.Hash(sign + code);
            var url = $"{baseServer}/authapi/v1.0/{account}/tokens?signature={key}&tenantid={tenantId}&appid={appId}&deptid={deptId}";
            var request = new HttpRequest();
            if (!request.Send(url))
            {
                Messages.ShowError(request.message);
                return;
            }

            var result = Util.Deserialize<Result<TokenPackage>>(request.data);
            if (!result.successful)
            {
                Messages.ShowError(result.message);
                return;
            }

            accessToken = result.data.accessToken;
            refreshToken = result.data.refreshToken;

            var now = DateTime.Now;
            expiryTime = now.AddSeconds(result.data.expiryTime);
            failureTime = now.AddSeconds(result.data.failureTime);
            isAutoRefres = result.data.expiryTime < 3600;
        }

        /// <summary>
        /// 注销令牌
        /// </summary>
        public void DeleteToken()
        {
            var url = $"{baseServer}/authapi/v1.0/tokens";
            var request = new HttpRequest(token);
            if (!request.Send(url, RequestMethod.DELETE))
            {
                Messages.ShowError(request.message);
                return;
            }

            accessToken = null;
            refreshToken = null;
        }

        /// <summary>
        /// 获取Code
        /// </summary>
        /// <returns>string Code</returns>
        private string GetCode()
        {
            var url = $"{baseServer}/authapi/v1.0/{account}/codes";
            var request = new HttpRequest();
            if (!request.Send(url))
            {
                Messages.ShowError(request.message);
                return null;
            }

            var result = Util.Deserialize<Result<string>>(request.data);
            if (result.successful) return result.data;

            Messages.ShowError(result.message);
            return null;
        }

        /// <summary>
        /// 刷新AccessToken过期时间
        /// </summary>
        private void RefresTokens()
        {
            accessToken = null;
            refreshToken = null;

            var url = $"{baseServer}/authapi/v1.0/tokens";
            var request = new HttpRequest(refreshToken);
            if (!request.Send(url, RequestMethod.PUT))
            {
                Messages.ShowError(request.message);
                return;
            }

            var result = Util.Deserialize<Result<TokenPackage>>(request.data);
            if (result.code == "406")
            {
                GetTokens();
                return;
            }

            if (!result.successful)
            {
                Messages.ShowError(result.message);
                return;
            }

            accessToken = result.data.accessToken;
            refreshToken = result.data.refreshToken;

            var now = DateTime.Now;
            expiryTime = now.AddSeconds(result.data.expiryTime);
            failureTime = now.AddSeconds(result.data.failureTime);
            isAutoRefres = result.data.expiryTime < 3600;
        }
    }
}