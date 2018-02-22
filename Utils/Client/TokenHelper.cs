using System;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{

    public class TokenHelper
    {
        private DateTime time;
        private string accessToken;
        private string refreshToken;
        private int expiryTime;
        private int failureTime;

        /// <summary>
        /// 请求状态
        /// </summary>
        public bool success { get; private set; }

        /// <summary>
        /// AccessToken字符串
        /// </summary>
        public string token
        {
            get
            {
                var now = DateTime.Now;
                if (string.IsNullOrEmpty(accessToken) || now > time.AddSeconds(failureTime))
                {
                    GetTokens();
                    if (!success) return null;
                }

                if (expiryTime >= 3600 && now > time.AddSeconds(expiryTime)) RefresTokens();

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
        public string baseServer { get; set; }

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
        /// 获取AccessToken
        /// </summary>
        /// <returns>bool 是否获取成功</returns>
        public void GetTokens()
        {
            var code = GetCode();
            if (code == null) return;

            var key = Util.Hash(sign + code);
            var url = $"{baseServer}/authapi/v1.0/tokens?tenantid={tenantId}&appid={appId}&account={account}&signature={key}&deptid={deptId}";
            var request = new HttpRequest(refreshToken);
            success = request.Send(url);
            if (!success)
            {
                Messages.ShowError(request.Message);
                return;
            }

            var result = Util.Deserialize<Result<TokenPackage>>(request.Data);
            if (!result.successful)
            {
                Messages.ShowError(result.message);
                return;
            }

            time = DateTime.Now;
            accessToken = result.data.accessToken;
            refreshToken = result.data.refreshToken;
            expiryTime = result.data.expiryTime;
            failureTime = result.data.failureTime;
        }

        /// <summary>
        /// 获取Code
        /// </summary>
        /// <returns>string Code</returns>
        private string GetCode()
        {
            var url = $"{baseServer}/authapi/v1.0/tokens/codes?account={account}";
            var request = new HttpRequest(refreshToken);
            success = request.Send(url);
            if (!success)
            {
                Messages.ShowError(request.Message);
                return null;
            }

            var result = Util.Deserialize<Result<string>>(request.Data);
            if (result.successful) return result.data;

            Messages.ShowError(result.message);
            return null;
        }

        /// <summary>
        /// 刷新AccessToken过期时间
        /// </summary>
        private void RefresTokens()
        {
            var url = $"{baseServer}/authapi/v1.0/tokens";
            var request = new HttpRequest(refreshToken);
            if (!request.Send(url))
            {
                Messages.ShowError(request.Message);
                return;
            }

            var result = Util.Deserialize<Result<TokenPackage>>(request.Data);
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

            time = DateTime.Now;
            accessToken = result.data.accessToken;
            refreshToken = result.data.refreshToken;
            expiryTime = result.data.expiryTime;
            failureTime = result.data.failureTime;
        }
    }
}