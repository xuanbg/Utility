using System;
using System.IO;
using System.Net;
using System.Text;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class HttpClient
    {
        private readonly string _Url;
        private readonly string _Method;
        private readonly string _Data;

        /// <summary>
        /// 是否记录接口调用日志
        /// </summary>
        public bool Logging { get; set; } = true;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="method">请求的方法：GET,PUT,POST,DELETE，默认值为GET</param>
        /// <param name="data">接口参数，默认值为Null</param>
        public HttpClient(string url, string method = "GET", string data = "")
        {
            _Url = url;
            _Method = method;
            _Data = data;
        }

        /// <summary>
        /// HttpRequest方法，用于客户端请求接口
        /// </summary>
        /// <param name="token">TokenHelper</param>
        /// <returns>Result</returns>
        public Result Request(TokenHelper token)
        {
#if DEBUG
            var time = DateTime.Now;
#endif
            var result = new Result();
            if (token == null)
            {
                result.BadRequest("缺少AccessToken！");
                return result;
            }

            Start:
            var request = GetWebRequest(token.AccessToken);
            if (_Method == "GET")
            {
                result = GetResponse(request);
                goto End;
            }

            var buffer = Encoding.UTF8.GetBytes(_Data);
            request.ContentLength = buffer.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(buffer, 0, buffer.Length);
            }

            result = GetResponse(request);

            End:
#if DEBUG
            Log(token.AccessToken, time, result.Message);
#endif
            if (result.Code != "406") return result;

            token.GetTokens();
            goto Start;
        }

        /// <summary>
        /// HttpRequest方法，用于服务端请求验证
        /// </summary>
        /// <param name="token">客户端提供的AccessToken</param>
        /// <returns>Result</returns>
        public Result Request(string token = null)
        {
#if DEBUG
            var time = DateTime.Now;
#endif
            var request = GetWebRequest(token);
            if (_Method == "GET") goto End;

            var buffer = Encoding.UTF8.GetBytes(_Data);
            request.ContentLength = buffer.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(buffer, 0, buffer.Length);
            }

            End:
            var result = GetResponse(request);
#if DEBUG
            if (Logging && token != null) Log(token, time, result.Message);
#endif
            return result;
        }

        /// <summary>
        /// 获取WebRequest对象
        /// </summary>
        /// <param name="token">AccessToken</param>
        /// <returns>HttpWebRequest</returns>
        private HttpWebRequest GetWebRequest(string token)
        {
            var request = (HttpWebRequest) WebRequest.Create(_Url);
            request.Method = _Method;
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
        /// <returns>Result</returns>
        private Result GetResponse(WebRequest request)
        {
            var result = new Result();
            try
            {
                var response = (HttpWebResponse) request.GetResponse();
                var responseStream = response.GetResponseStream();
                if (responseStream == null)
                {
                    result.BadRequest("Response was not received data!");
                    return result;
                }

                using (var reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                {
                    var stream = reader.ReadToEnd();
                    responseStream.Close();
                    result = Util.Deserialize<Result>(stream);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.BadRequest(ex);
                return result;
            }
        }

#if DEBUG
        /// <summary>
        /// 记录接口调用日志
        /// </summary>
        /// <param name="token"></param>
        /// <param name="time"></param>
        /// <param name="message"></param>
        private void Log(string token, DateTime time, string message)
        {
            var ts = DateTime.Now - time;
            var server = Util.GetAppSetting("BaseServer");
            var loginfo = new LogInfo
            {
                Interface = $"{server}/logapi/v1.0/logs",
                Token = token,
                Code = "700101",
                Source = "系统平台",
                Action = "接口调用",
                Message = $"[{_Method}]{_Url};{message};用时{ts.TotalMilliseconds}毫秒"
            };
            var log = new LogClient(loginfo);
            log.LogToServer();
        }
#endif
    }
}
