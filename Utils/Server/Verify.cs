using System;
using System.Net;
using System.ServiceModel.Web;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Server
{
    public class Verify
    {
        /// <summary>
        /// 验证结果
        /// </summary>
        public Result Result = new Result();

        /// <summary>
        /// Access Token
        /// </summary>
        public string Token;

        /// <summary>
        /// 通过Access Token校验是否有权限访问
        /// </summary>
        /// <param name="server">验证服务URL</param>
        /// <param name="limit">限制调用时间间隔（秒），默认不启用</param>
        /// <param name="anonymous">是否允许匿名访问（默认不允许）</param>
        public Verify(string server, int limit = 0, bool anonymous = false)
        {
            if (anonymous)
            {
                Result.InvalidAuth();
                if (!GetToken()) return;

                Result = new HttpRequest(server, "GET", Token).Result;
                if (Result.Successful) return;

                var time = Util.LimitCall(limit <= 0 ? 60 : limit);
                if (time > 0)
                {
                    Result.TooFrequent(time.ToString());
                    return;
                }

                Result.Success();
            }
            else
            {
                var time = Util.LimitCall(limit);
                if (time > 0)
                {
                    Result.TooFrequent(time.ToString());
                    return;
                }

                Result.InvalidAuth();
                if (!GetToken()) return;

                Result = new HttpRequest(server, "GET", Token).Result;
            }
        }

        /// <summary>
        /// 带鉴权的会话合法性验证
        /// </summary>
        /// <param name="server">验证服务URL</param>
        /// <param name="aid">操作ID</param>
        /// <param name="limit">限制调用时间间隔（秒），默认不启用</param>
        public Verify(string server, Guid aid, int limit = 0)
        {
            var time = Util.LimitCall(limit);
            if (time > 0)
            {
                Result.TooFrequent(time.ToString());
                return;
            }

            if (!GetToken())
            {
                Result.InvalidAuth();
                return;
            }

            var url =  $"{server}/auth?action={aid}";
            Result = new HttpRequest(url, "GET", Token).Result;
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
            Token = headers[HttpRequestHeader.Authorization];
            if (!string.IsNullOrEmpty(Token)) return true;

            response.StatusCode = HttpStatusCode.Unauthorized;
            return false;
        }
    }
}
