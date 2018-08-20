using System;
using System.Collections.Generic;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class TokenHelper
    {
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
                    getTokens();
                }
                else if (now > expiryTime)
                {
                    refresTokens();
                }

                return accessToken;
            }
        }

        /// <summary>
        /// 用户签名
        /// </summary>
        public string sign { get; private set; }

        /// <summary>
        /// 当前连接鉴权服务器
        /// </summary>
        public string authServer { get; set; } = Util.getAppSetting("AuthServer");

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
        public void signature(string secret)
        {
            sign = Util.hash(account + Util.hash(secret));
        }

        /// <summary>
        /// 获取令牌
        /// </summary>
        public void getTokens()
        {
            accessToken = null;
            refreshToken = null;

            var code = getCode();
            if (code == null) return;

            var key = Util.hash(sign + code);
            var url = $"{authServer}/authapi/v1.0/tokens";
            var dict = new Dictionary<string, object>
            {
                {"appid", appId},
                {"tenantid", tenantId},
                {"deptid", deptId},
                {"account", account},
                {"signature", key}
            };
            var request = new HttpRequest();
            if (!request.send(url, RequestMethod.GET, dict))
            {
                Messages.showError(request.message);
                return;
            }

            var result = Util.deserialize<Result<TokenPackage>>(request.data);
            if (!result.successful)
            {
                Messages.showError(result.message);
                return;
            }

            accessToken = result.data.accessToken;
            refreshToken = result.data.refreshToken;

            var now = DateTime.Now;
            expiryTime = now.AddSeconds(result.data.expiryTime);
            failureTime = now.AddSeconds(result.data.failureTime);
        }

        /// <summary>
        /// 刷新AccessToken过期时间
        /// </summary>
        public void refresTokens()
        {
            accessToken = null;

            var url = $"{authServer}/authapi/v1.0/tokens";
            var request = new HttpRequest(refreshToken);
            if (!request.send(url, RequestMethod.PUT))
            {
                Messages.showError(request.message);
                return;
            }

            var result = Util.deserialize<Result<TokenPackage>>(request.data);
            if (result.code == "406")
            {
                getTokens();
                return;
            }

            if (!result.successful)
            {
                Messages.showError(result.message);
                return;
            }

            accessToken = result.data.accessToken;
            refreshToken = result.data.refreshToken;

            var now = DateTime.Now;
            expiryTime = now.AddSeconds(result.data.expiryTime);
            failureTime = now.AddSeconds(result.data.failureTime);
        }

        /// <summary>
        /// 注销令牌
        /// </summary>
        public void deleteToken()
        {
            var url = $"{authServer}/authapi/v1.0/tokens";
            var request = new HttpRequest(token);
            if (!request.send(url, RequestMethod.DELETE))
            {
                Messages.showError(request.message);
                return;
            }

            accessToken = null;
            refreshToken = null;
        }

        /// <summary>
        /// 获取Code
        /// </summary>
        /// <returns>string Code</returns>
        private string getCode()
        {
            var url = $"{authServer}/authapi/v1.0/tokens/codes";
            var dict = new Dictionary<string, object>
            {
                {"account", account},
                {"type", 0}
            };
            var request = new HttpRequest();
            if (!request.send(url, RequestMethod.GET, dict))
            {
                Messages.showError(request.message);
                return null;
            }

            var result = Util.deserialize<Result<string>>(request.data);
            if (result.successful) return result.data;

            Messages.showError(result.message);
            return null;
        }
    }
}