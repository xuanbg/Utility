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
        private readonly string _AccessToken;
        private readonly TokenHelper _Token;

        /// <summary>
        /// 是否记录接口调用日志
        /// </summary>
        public bool Logging { get; set; } = true;

        /// <summary>
        /// 构造函数，传入AccessToken
        /// </summary>
        /// <param name="token">AccessToken</param>
        public HttpClient(string token)
        {
            if (string.IsNullOrEmpty(token)) Logging = false;

            _AccessToken = token;
        }

        /// <summary>
        /// 构造函数，传入TokenHelper
        /// </summary>
        /// <param name="token">TokenHelper</param>
        public HttpClient(TokenHelper token)
        {
            _Token = token;
            _AccessToken = token.AccessToken;
        }

        /// <summary>
        /// HttpRequest:GET方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Get<T>(string url, string message = null)
        {
            var result = Request(url);
            if (result.Successful) return Util.Deserialize<T>(result.Data);

            var msg = $"{result.Message}{"\r\n" + message}";
            Messages.ShowError(msg);
            return default(T);
        }

        /// <summary>
        /// HttpRequest:POST方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="data">POST的数据</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Post<T>(string url, object data, string message = null)
        {
            var result = Request(url, "POST", Util.Serialize(data));
            if (result.Successful) return Util.Deserialize<T>(result.Data);

            var msg = $"{result.Message}{"\r\n" + message}";
            Messages.ShowError(msg);
            return default(T);
        }

        /// <summary>
        /// HttpRequest:PUT方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="data">PUT的数据</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Put<T>(string url, object data, string message = null)
        {
            var result = Request(url, "PUT", Util.Serialize(data));
            if (result.Successful) return Util.Deserialize<T>(result.Data);

            var msg = $"{result.Message}\r\n{message}";
            Messages.ShowError(msg);
            return default(T);
        }

        /// <summary>
        /// HttpRequest:DELETE方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="data">DELETE的数据，默认NULL</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Delete<T>(string url, object data = null, string message = null)
        {
            var result = Request(url, "DELETE", Util.Serialize(data));
            if (result.Successful) return Util.Deserialize<T>(result.Data);

            var msg = $"{result.Message}{"\r\n" + message}";
            Messages.ShowError(msg);
            return default(T);
        }

        /// <summary>
        /// HttpRequest方法，用于客户端请求接口
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法，默认GET</param>
        /// <param name="data">请求数据，默认NULL</param>
        /// <returns>Result</returns>
        public Result Request(string url, string method = "GET", string data = null)
        {
#if DEBUG
            var time = DateTime.Now;
#endif
            Start:
            var request = GetWebRequest(method, url, _AccessToken);
            if (!string.IsNullOrEmpty(data))
            {
                var buffer = Encoding.UTF8.GetBytes(data);
                request.ContentLength = buffer.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }

            var result = GetResponse(request);
#if DEBUG
            if (Logging) Log(_AccessToken, method, url, time, result.Message);
#endif
            if (_Token == null || result.Code != "406") return result;

            _Token.GetTokens();
            goto Start;
        }

        /// <summary>
        /// 获取WebRequest对象
        /// </summary>
        /// <param name="method">请求方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="token">AccessToken</param>
        /// <returns>HttpWebRequest</returns>
        private HttpWebRequest GetWebRequest(string method, string url, string token)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
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
        /// <param name="token">AccessToken</param>
        /// <param name="method">请求方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="time">接口调用开始时间</param>
        /// <param name="message">接口返回消息</param>
        private void Log(string token, string method, string url, DateTime time, string message)
        {
            var ts = DateTime.Now - time;
            var server = $"http://{Util.GetAppSetting("BaseServer")}:{Util.GetAppSetting("BasePort")}";
            var loginfo = new LogInfo
            {
                Interface = $"{server}/logapi/v1.0/logs",
                Token = token,
                Code = "700101",
                Source = "系统平台",
                Action = "接口调用",
                Message = $"[{method}]{url};{message};用时{ts.TotalMilliseconds}毫秒"
            };
            var log = new LogClient(loginfo);
            log.LogToServer();
        }
#endif
    }
}
