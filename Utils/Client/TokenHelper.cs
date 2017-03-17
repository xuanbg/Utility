using System;
using System.Text;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{

    public class TokenHelper
    {
        private string _Token;
        private string _RefreshToken;
        private DateTime _ExpiryTime;
        private DateTime _FailureTime;

        /// <summary>
        /// AccessToken字符串
        /// </summary>
        public string AccessToken => GetToken();

        /// <summary>
        /// AccessToken对象
        /// </summary>
        public AccessToken Token { get; private set; } = new AccessToken();

        /// <summary>
        /// 用户签名
        /// </summary>
        public string Sign { get; private set; }

        /// <summary>
        /// 当前连接基础应用服务器
        /// </summary>
        public string BaseServer { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="secret">用户密钥</param>
        public void Signature(string secret)
        {
            Sign = Util.Hash(Account.ToUpper() + Util.Hash(secret));
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns>bool 是否获取成功</returns>
        public bool GetTokens()
        {
            var code = GetCode();
            if (code == null) return false;

            var key = Util.Hash(Sign + code);
            var url = $"{BaseServer}/securityapi/v1.0/tokens?account={Account}&signature={key}&deptid={Token.deptId}";
            var result = new HttpClient().Get<TokenResult>(url);
            if (result == null) return false;

            _Token = result.accessToken;
            _RefreshToken = result.refreshToken;
            _ExpiryTime = result.expiryTime;
            _FailureTime = result.failureTime;

            var buffer = Convert.FromBase64String(_Token);
            var json = Encoding.UTF8.GetString(buffer);
            Token = Util.Deserialize<AccessToken>(json);
            return true;
        }

        /// <summary>
        /// 获取Code
        /// </summary>
        /// <returns>Dictionary Code</returns>
        private string GetCode()
        {
            var url = $"{BaseServer}/securityapi/v1.0/codes?account={Account}";
            return new HttpClient().Request(url).data;
        }

        /// <summary>
        /// 刷新AccessToken过期时间
        /// </summary>
        private void RefresTokens()
        {
            var url = $"{BaseServer}/securityapi/v1.0/tokens";
            var result = new HttpClient(_RefreshToken).Request(url, "PUT");
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

            var data = Util.Deserialize<TokenResult>(result.data);
            _ExpiryTime = data.expiryTime;
        }

        /// <summary>
        /// 检查当前Token并返回
        /// </summary>
        /// <returns>string AccessToken</returns>
        private string GetToken()
        {
            var now = DateTime.Now;
            if (string.IsNullOrEmpty(_Token) || now > _FailureTime) GetTokens();

            if (now > _ExpiryTime) RefresTokens();

            return _Token;
        }
    }
}
