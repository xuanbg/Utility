using System;
using System.Text;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{

    public static class Tokens
    {
        /// <summary>
        /// AccessToken字符串
        /// </summary>
        public static string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(_Token)) GetTokens();

                if (DateTime.Now > _ExpiryTime) RefresTokens();

                return _Token;
            }
            set { _Token = value; }
        }

        /// <summary>
        /// AccessToken对象
        /// </summary>
        public static AccessToken Token { get; private set; }

        /// <summary>
        /// 用户签名
        /// </summary>
        public static string Sign { get; private set; }

        /// <summary>
        /// 当前连接基础应用服务器
        /// </summary>
        public static string BaseServer;

        /// <summary>
        /// 应用ID
        /// </summary>
        public static string Account;

        /// <summary>
        /// 当前登录部门ID
        /// </summary>
        public static Guid? DeptId;

        // AccessToken
        private static string _Token;

        // RefresToken字符串
        private static string _RefreshToken;

        // Secret过期时间
        private static DateTime _ExpiryTime;

        // Secret失效时间
        private static DateTime _FailureTime;

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="secret">用户密钥</param>
        public static void Signature(string secret)
        {
            Sign = Util.Hash(Account.ToUpper() + Util.Hash(secret));
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        public static void GetTokens()
        {
            var url = $"{BaseServer}/security/v1.0/tokens?account={Account}&signature={Sign}&deptid={DeptId}";
            var result = new HttpRequest(url, "GET", null).Result;
            if (!result.Successful)
            {
                const string str = "配置错误！请检查配置文件中的BaseServer项是否配置正确。";
                var msg = result.Code == "400" ? str : result.Message;
                Messages.ShowError(msg);
                return;
            }

            if (result.Code == "202") Messages.ShowWarning("您已在另一台设备登录！如登录者不是本人，请联系管理员。");

            var data = Util.Deserialize<TokenResult>(result.Data);
            _Token = data.AccessToken;
            _RefreshToken = data.RefreshToken;
            _ExpiryTime = data.ExpiryTime;
            _FailureTime = data.FailureTime;

            var buffer = Convert.FromBase64String(_Token);
            var json = Encoding.UTF8.GetString(buffer);
            Token = Util.Deserialize<AccessToken>(json);
        }

        /// <summary>
        /// 刷新AccessToken过期时间
        /// </summary>
        public static void RefresTokens()
        {
            if (DateTime.Now > _FailureTime)
            {
                GetTokens();
                return;
            }
            var url = $"{BaseServer}/security/v1.0/tokens";
            var result = new HttpRequest(url, "PUT", _RefreshToken).Result;
            if (!result.Successful)
            {
                Messages.ShowError(result.Message);
                return;
            }

            var data = Util.Deserialize<TokenResult>(result.Data);
            _ExpiryTime = data.ExpiryTime;
        }
    }
}
