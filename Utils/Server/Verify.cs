using System;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Server
{
    public class Verify
    {
        private AccessToken _Token;

        /// <summary>
        /// 验证结果
        /// </summary>
        public Result Result = new Result();

        /// <summary>
        /// Access Token对象
        /// </summary>
        public AccessToken Token
        {
            get
            {
                if (_Token != null) return _Token;

                var buffer = Convert.FromBase64String(AccessToken);
                var json = Encoding.UTF8.GetString(buffer);
                _Token = Util.Deserialize<AccessToken>(json);
                return _Token;
            }
        }

        /// <summary>
        /// Access Token字符串
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// 允许非授权访问，限制访问间隔60秒以上；如通过授权则不限制间隔时间
        /// </summary>
        /// <param name="verifyurl">验证服务URL</param>
        /// <param name="limit">限制访问时间间隔（秒）</param>
        public Verify(string verifyurl, int limit)
        {
            GetToken();
            Result = new HttpClient(AccessToken) {Logging = false}.Request(verifyurl);
            if (Result.Successful) return;

            var time = CallManage.LimitCall(limit <= 60 ? 60 : limit);
            if (time > 0)
            {
                Result.TooFrequent(time);
                return;
            }

            Result.Success();
        }

        /// <summary>
        /// 会话合法性验证
        /// </summary>
        /// <param name="verifyurl">验证服务URL</param>
        /// <param name="aid">操作权限代码，默认为空(不进行鉴权)</param>
        /// <param name="limit">限制访问时间间隔（秒），默认不启用</param>
        public Verify(string verifyurl, string aid = null, int limit = 0)
        {
            var time = CallManage.LimitCall(limit);
            if (time > 0)
            {
                Result.TooFrequent(time);
                return;
            }

            if (!GetToken())
            {
                Result.InvalidAuth();
                return;
            }

            var url =  $"{verifyurl}?action={aid}";
            Result = new HttpClient(AccessToken) {Logging = false}.Request(url);
        }

        /// <summary>
        /// 获取Http请求头部承载的Access Token
        /// </summary>
        /// <returns>boll Http请求头部是否承载Access Token</returns>
        private bool GetToken()
        {
            var context = WebOperationContext.Current;
            if (context == null) return false;

            var headers = context.IncomingRequest.Headers;
            var response = context.OutgoingResponse;
            AccessToken = headers[HttpRequestHeader.Authorization];
            if (!string.IsNullOrEmpty(AccessToken)) return true;

            response.StatusCode = HttpStatusCode.Unauthorized;
            return false;
        }
    }
}
