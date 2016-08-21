using System;
using System.IO;
using System.Net;
using System.Text;
using Insight.Utils.Entity;

namespace Insight.Utils
{
    public class HttpRequest
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public Result Result;

        /// <summary>
        /// HttpRequest方法
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="method">请求的方法：GET,PUT,POST,DELETE</param>
        /// <param name="author">接口认证数据</param>
        /// <param name="data">接口参数</param>
        /// <returns>JsonResult</returns>
        public HttpRequest(string url, string method, string author, string data = "")
        {
            var request = GetWebRequest(url, method, author);
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
        /// <param name="author">接口认证数据</param>
        /// <returns>HttpWebRequest</returns>
        private HttpWebRequest GetWebRequest(string url, string method, string author)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, author);

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
                var response = (HttpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();
                if (responseStream == null)
                {
                    Result = new Result().BadRequest("Response was not received data!");
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
                Result = new Result().BadRequest(ex.ToString());
            }
        }
    }
}
