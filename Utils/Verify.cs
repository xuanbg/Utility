using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using Insight.Utils.Entity;

namespace Insight.Utils
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
        public Verify(string server, int limit = 0)
        {
            Result.InvalidAuth();
            if (!GetToken()) return;

            Result = new HttpRequest(server, "GET", Token).Result;
            if (Result.Successful) return;

            var time = LimitCall(limit);
            if (time <= 0)
            {
                Result.Success();
                return;
            }

            Result.TooFrequent(time.ToString());
        }

        /// <summary>
        /// 带鉴权的会话合法性验证
        /// </summary>
        /// <param name="server">验证服务URL</param>
        /// <param name="aid">操作ID</param>
        /// <param name="limit">限制调用时间间隔（秒），默认不启用</param>
        public Verify(string server, Guid aid, int limit = 0)
        {
            Result.InvalidAuth();
            if (!GetToken()) return;

            var url =  $"{server}/auth?action={aid}";
            Result = new HttpRequest(url, "GET", Token).Result;
            if (Result.Successful && limit == 0) return;

            var time = LimitCall(limit);
            if (time <= 0) return;

            Result.TooFrequent(time.ToString());
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

        /// <summary>
        /// 根据传入的时长返回当前调用的剩余限制时间（秒）
        /// </summary>
        /// <param name="seconds">限制访问时长（秒）</param>
        /// <returns>int 剩余限制时间（秒）</returns>
        private int LimitCall(int seconds)
        {
            var properties = OperationContext.Current.IncomingMessageProperties;
            var endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpoint == null) return 0;

            var ip = endpoint.Address;
            if (!Util.Requests.ContainsKey(ip))
            {
                Util.Requests.Add(ip, DateTime.Now);
                return 0;
            }

            var span = Util.Requests[ip].AddSeconds(seconds) - DateTime.Now;
            var surplus = (int)Math.Floor(span.TotalSeconds);
            if (seconds - surplus > 0 && seconds - surplus < 3)
            {
                Util.Requests[ip] = DateTime.Now;
                return seconds;
            }

            if (surplus > 0) return surplus;

            Util.Requests[ip] = DateTime.Now;
            return 0;
        }
    }
}
