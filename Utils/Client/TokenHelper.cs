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
        public string AccessToken
        {
            get
            {
                var now = DateTime.Now;
                if (string.IsNullOrEmpty(_Token) || now > _FailureTime)
                {
                    var result = GetTokens();
                    if (!result) return null;
                }

                if (now > _ExpiryTime) RefresTokens();

                return _Token;

            }
        }

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
            var client = new HttpClient<TokenResult>(null);
            if (!client.Get(url)) return false;

            _Token = client.Data.accessToken;
            _RefreshToken = client.Data.refreshToken;
            _ExpiryTime = client.Data.expiryTime;
            _FailureTime = client.Data.failureTime;

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
            var url = $"{BaseServer}/securityapi/v1.0/tokens/codes?account={Account}";
            var client = new HttpClient<string>(null);
            return client.Get(url) ? client.Data : null;
        }

        /// <summary>
        /// 刷新AccessToken过期时间
        /// </summary>
        private void RefresTokens()
        {
            var url = $"{BaseServer}/securityapi/v1.0/tokens";
            var request = new HttpRequest(url, _RefreshToken);
            if (!request.Request())
            {
                Messages.ShowError(request.Message);
                return;
            }

            var result = Util.Deserialize<Result<DateTime>>(request.Data);
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

            _ExpiryTime = result.data;
        }
    }
}