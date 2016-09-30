using System;
using System.Text;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{

    public class TokenHelper
    {
        /// <summary>
        /// AccessToken字符串
        /// </summary>
        public string AccessToken
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
        public AccessToken Token { get; private set; }

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
        /// 当前登录部门ID
        /// </summary>
        public Guid? DeptId { get; set; }

        private string _Token;
        private string _RefreshToken;
        private DateTime _ExpiryTime;
        private DateTime _FailureTime;

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
        public bool GetTokens()
        {
            var url = $"{BaseServer}/security/v1.0/tokens?account={Account}&signature={Sign}&deptid={DeptId}";
            var result = new HttpRequest(url, "GET", null).Result;
            if (!result.Successful)
            {
                const string str = "配置错误！请检查配置文件中的BaseServer项是否配置正确。";
                var msg = result.Code == "400" ? str : result.Message;
                Messages.ShowError(msg);
                return false;
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
            return true;
        }

        /// <summary>
        /// 刷新AccessToken过期时间
        /// </summary>
        private void RefresTokens()
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
