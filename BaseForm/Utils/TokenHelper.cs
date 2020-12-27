using System.Collections.Generic;
using Insight.Base.BaseForm.Entities;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Base.BaseForm.Utils
{
    public class TokenHelper
    {
        private readonly string appId = Util.getAppSetting("AppId");
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
        /// 登录账号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 是否需要修改密码
        /// </summary>
        public bool needChangePw => sign == Util.hash(account + Util.hash("123456"));

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="secret">用户密钥(MD5)</param>
        public void signature(string secret)
        {
            sign = Util.hash(account + secret);
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
            var body = new LoginDto {appId = appId, tenantId = tenantId, account = account, signature = key};
            var url = "/base/auth/v1.0/tokens";
            var client = new HttpClient<TokenPackage>(url);
            var result = client.request(RequestMethod.POST, body);
            if (!result.success) return false;

            var data = result.data;
            accessToken = data.accessToken;
            refreshToken = data.refreshToken;

            Setting.userId = data.userInfo.id;
            Setting.userName = data.userInfo.name;
            Setting.tenantId = data.userInfo.tenantId;

            return true;
        }

        /// <summary>
        /// 刷新AccessToken过期时间
        /// </summary>
        public bool refresTokens()
        {
            accessToken = null;
            var url = $"{Setting.gateway}/base/auth/v1.0/tokens";
            var request = new HttpRequest(url, refreshToken);
            if (!request.send(RequestMethod.PUT))
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
            const string url = "/base/auth/v1.0/tokens";
            var client = new HttpClient<object>(url);
            if (!client.delete()) return;

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
            var dict = new Dictionary<string, object> {{"account", account}};
            var client = new HttpClient<string>(url);

            return client.getData(dict);
        }
    }
}