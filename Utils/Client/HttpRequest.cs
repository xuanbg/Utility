using System;
using System.IO;
using System.Net;
using System.Text;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class HttpRequest
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public Result Result = new Result();

        /// <summary>
        /// HttpRequest方法，用于客户端请求接口
        /// </summary>
        /// <param name="token">Token客户端</param>
        /// <param name="url">请求的地址</param>
        /// <param name="method">请求的方法：GET,PUT,POST,DELETE</param>
        /// <param name="data">接口参数</param>
        /// <returns>JsonResult</returns>
        public HttpRequest(TokenHelper token, string url, string method, string data = "")
        {
            Start:
            var request = GetWebRequest(url, method, token?.AccessToken);
            if (method == "GET")
            {
                GetResponse(request);
                goto End;
            }

            var buffer = Encoding.UTF8.GetBytes(data);
            request.ContentLength = buffer.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(buffer, 0, buffer.Length);
            }

            GetResponse(request);

            End:
            if (token == null || Result.Code != "406") return;

            token.GetTokens();
            goto Start;
        }

        /// <summary>
        /// HttpRequest方法，用于服务端请求验证
        /// </summary>
        /// <param name="token">客户端提供的AccessToken</param>
        /// <param name="url">请求的地址</param>
        /// <param name="method">请求的方法：GET,PUT,POST,DELETE</param>
        /// <param name="data">接口参数</param>
        /// <returns>JsonResult</returns>
        public HttpRequest(string token, string url, string method, string data = "")
        {
            var request = GetWebRequest(url, method, token);
            if (method == "GET")
            {
                GetResponse(request);
                return;
            }

            var buffer = Encoding.UTF8.GetBytes(data);
            request.ContentLength = buffer.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(buffer, 0, buffer.Length);
            }

            GetResponse(request);
        }

        /// <summary>
        /// 获取WebRequest对象
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="method">请求的方法：GET,PUT,POST,DELETE</param>
        /// <param name="token">AccessToken</param>
        /// <returns>HttpWebRequest</returns>
        private HttpWebRequest GetWebRequest(string url, string method, string token)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.Accept = "application/json";
            request.ContentType = "application/json";
            if (string.IsNullOrEmpty(token)) return request;

            request.Headers.Add(HttpRequestHeader.Authorization, token);
            return request;
        }

        /// <summary>
        /// 获取Request响应数据
        /// </summary>
        /// <param name="request">WebRequest</param>
        /// <returns>JsonResult</returns>
        private void GetResponse(WebRequest request)
        {
            try
            {
                var response = (HttpWebResponse) request.GetResponse();
                var responseStream = response.GetResponseStream();
                if (responseStream == null)
                {
                    Result.BadRequest("Response was not received data!");
                    return;
                }

                using (var reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                {
                    var result = reader.ReadToEnd();
                    responseStream.Close();
                    Result = Util.Deserialize<Result>(result);
                }
            }
            catch (Exception ex)
            {
                Result.BadRequest(ex);
            }
        }
    }
}
