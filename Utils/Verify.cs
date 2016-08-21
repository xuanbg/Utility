using System;
using System.Net;
using System.ServiceModel.Web;
using Insight.Utils.Entity;

namespace Insight.Utils
{
    public class Verify
    {
        /// <summary>
        /// 验证结果
        /// </summary>
        public Result Result = new Result().InvalidAuth();

        /// <summary>
        /// Access Token
        /// </summary>
        public string Token;

        /// <summary>
        /// 当前Web操作上下文
        /// </summary>
        private readonly WebOperationContext Context = WebOperationContext.Current;

        /// <summary>
        /// 通过Access Token校验是否有权限访问
        /// </summary>
        /// <param name="server">验证服务URL</param>
        public Verify(string server)
        {
            if (!GetToken()) return;

            Result = new HttpRequest(server, "GET", Token).Result;
        }

        /// <summary>
        /// 带鉴权的会话合法性验证
        /// </summary>
        /// <param name="server">验证服务URL</param>
        /// <param name="aid">操作ID</param>
        public Verify(string server, Guid aid)
        {
            if (!GetToken()) return;

            var url =  $"{server}/auth?action={aid}";
            Result = new HttpRequest(url, "GET", Token).Result;
        }

        /// <summary>
        /// 获取Http请求头部承载的Access Token
        /// </summary>
        /// <returns>boll Http请求头部是否承载Access Token</returns>
        private bool GetToken()
        {
            var headers = Context.IncomingRequest.Headers;
            var response = Context.OutgoingResponse;
            Token = headers[HttpRequestHeader.Authorization];
            if (!string.IsNullOrEmpty(Token)) return true;

            response.StatusCode = HttpStatusCode.Unauthorized;
            return false;
        }
    }
}
