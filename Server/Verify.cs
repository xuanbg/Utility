using System;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Server
{
    public class Verify
    {
        private AccessToken _Token;
        private UriTemplateMatch _Uri;

        /// <summary>
        /// 验证结果
        /// </summary>
        public Result<object> Result = new Result<object>();

        /// <summary>
        /// Access Token字符串
        /// </summary>
        public string AccessToken { get; private set; }

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
        /// 会话合法性验证
        /// </summary>
        /// <param name="call">CallManage</param>
        /// <param name="verifyurl">验证服务URL</param>
        /// <param name="aid">操作权限代码，默认为空(不进行鉴权)</param>
        /// <param name="limit">限制访问时间间隔（秒），默认不启用</param>
        public Verify(CallManage call, string verifyurl, string aid = null, int limit = 0)
        {
            if (!GetToken())
            {
                Result.InvalidAuth();
                return;
            }

            if (call != null && limit > 0)
            {
                var key = Util.Hash(Token.id.ToString() + _Uri.Data);
                var time = call.LimitCall(key, limit);
                if (time > 0)
                {
                    Result.TooFrequent(time.ToString());
                    return;
                }
            }

            var url = $"{verifyurl}?action={aid}";
            var request = new HttpRequest(AccessToken);
            if (!request.Send(url))
            {
                Result.BadRequest(request.Message);
                return;
            }

            Result = Util.Deserialize<Result<object>>(request.Data);
        }

        /// <summary>
        /// 获取Http请求头部承载的Access Token
        /// </summary>
        /// <returns>boll Http请求头部是否承载Access Token</returns>
        private bool GetToken()
        {
            var context = WebOperationContext.Current;
            if (context == null) return false;

            var request = context.IncomingRequest;
            _Uri = request.UriTemplateMatch;

            var headers = request.Headers;
            AccessToken = headers[HttpRequestHeader.Authorization];
            if (!string.IsNullOrEmpty(AccessToken)) return true;

            var response = context.OutgoingResponse;
            response.StatusCode = HttpStatusCode.Unauthorized;
            return false;
        }
    }
}