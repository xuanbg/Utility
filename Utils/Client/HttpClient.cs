using System;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class HttpClient
    {
        private readonly TokenHelper _Token;
        private readonly DateTime _Time = DateTime.Now;

        /// <summary>
        /// 构造函数，传入TokenHelper
        /// </summary>
        /// <param name="token">TokenHelper</param>
        public HttpClient(TokenHelper token)
        {
            _Token = token;
        }

        /// <summary>
        /// HttpRequest:GET方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Get<T>(string url, string message = null) where T : new()
        {
            var result = Request(url);
            if (result.successful) return (T)result.data;

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            Messages.ShowError(msg);

            return new T();
        }

        /// <summary>
        /// HttpRequest:POST方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="data">POST的数据</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Post<T>(string url, object data, string message = null) where T : new()
        {
            var result = Request(url, RequestMethod.POST, data);
            if (result.successful) return (T)result.data;

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            Messages.ShowError(msg);

            return new T();
        }

        /// <summary>
        /// HttpRequest:PUT方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="data">PUT的数据</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Put<T>(string url, object data, string message = null) where T : new()
        {
            var result = Request(url, RequestMethod.PUT, data);
            if (result.successful) return (T)result.data;

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            Messages.ShowError(msg);

            return new T();
        }

        /// <summary>
        /// HttpRequest:DELETE方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="data">DELETE的数据，默认NULL</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Delete<T>(string url, object data = null, string message = null) where T : new()
        {
            var result = Request(url, RequestMethod.DELETE, data);
            if (result.successful) return (T)result.data;

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            Messages.ShowError(msg);

            return new T();
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="method">请求方法</param>
        /// <param name="data">Body中的数据</param>
        /// <returns>Result</returns>
        public Result Request(string url, RequestMethod method = RequestMethod.GET, object data = null)
        {
            var body = new JavaScriptSerializer().Serialize(data ?? "");
            var result = new HttpRequest(_Token.AccessToken, url, method, body).Result;
            if (result.code == "406" || result.code == "401")
            {
                _Token.GetTokens();
                return Request(url, method, data);
            }

#if DEBUG
            // 在DEBUG模式下且AccessToken有效时记录接口调用日志
            LogAsync(method.ToString(), url, result.message);
#endif
            return result;
        }

        /// <summary>
        /// 记录接口调用日志
        /// </summary>
        /// <param name="token">AccessToken</param>
        /// <param name="method">请求方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="message">接口返回消息</param>
        private void Log(string token, string method, string url, string message)
        {
            if (string.IsNullOrEmpty(token)) return;

            var ts = DateTime.Now - _Time;
            var loginfo = new LogInfo
            {
                Interface = $"{Util.GetAppSetting("BaseServer")}/logapi/v1.0/logs",
                Token = token,
                Code = "700101",
                Source = "系统平台",
                Action = "接口调用",
                Message = $"[{method}]{url};{message};用时{ts.TotalMilliseconds}毫秒"
            };
            var log = new LogClient(loginfo);
            log.LogToServer();
        }

        /// <summary>
        /// 异步记录接口调用日志
        /// </summary>
        /// <param name="method">请求方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="message">接口返回消息</param>
        private void LogAsync(string method, string url, string message)
        {
            Task.Run(() => Log(_Token.AccessToken, method, url, message));
        }
    }
}