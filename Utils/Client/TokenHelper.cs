using System.Collections.Generic;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class TokenHelper
    {
        private string refreshToken;

        /// <summary>
        /// AccessToken字符串
        /// </summary>
        public string accessToken { get; private set; }

        /// <summary>
        /// 用户签名
        /// </summary>
        public string sign { get; private set; }

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
        public bool getTokens()
        {
            accessToken = null;
            refreshToken = null;
            var code = getCode();
            if (code == null) return false;

            var key = Util.hash(sign + code);
            var body = new LoginDto {appId = appId, tenantId = tenantId, deptId = deptId, account = account, signature = key};
            var url = "/base/auth/v1.0/tokens";
            var client = new HttpClient<TokenPackage>();
            var data = client.request(url, RequestMethod.POST, body);
            if (!client.success) return false;

            accessToken = data.accessToken;
            refreshToken = data.refreshToken;

            Setting.userId = data.userInfo.id;
            Setting.userName = data.userInfo.name;
            Setting.tenantId = data.userInfo.tenantId;
            Setting.deptId = data.userInfo.deptId;

            return true;
        }

        /// <summary>
        /// 刷新AccessToken过期时间
        /// </summary>
        public bool refresTokens()
        {
            accessToken = null;
            var url = $"{Setting.gateway}/base/auth/v1.0/tokens";
            var request = new HttpRequest(refreshToken);
            if (!request.send(url, RequestMethod.PUT))
            {
                Messages.showError(request.message);
                return false;
            }

            var result = Util.deserialize<Result<TokenPackage>>(request.data);
            if (!result.success)
            {
                Messages.showError(result.message);
                return false;
            }

            accessToken = result.data.accessToken;
            refreshToken = result.data.refreshToken;

            return true;
        }

        /// <summary>
        /// 注销令牌
        /// </summary>
        public void deleteToken()
        {
            var url = "/base/auth/v1.0/tokens";
            var client = new HttpClient<object>();
            client.getData(url, RequestMethod.DELETE);

            accessToken = null;
            refreshToken = null;
        }

        /// <summary>
        /// 获取Code
        /// </summary>
        /// <returns>string Code</returns>
        private string getCode()
        {
            var url = "/base/auth/v1.0/tokens/codes";
            var dict = new Dictionary<string, object>{{"account", account}};
            var client = new HttpClient<object>();

            return client.getData(url, RequestMethod.GET, dict)?.ToString();
        }
    }
}