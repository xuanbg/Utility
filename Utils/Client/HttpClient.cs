using System;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class HttpClient<T>
    {
        private readonly TokenHelper token;
        private readonly DateTime time = DateTime.Now;
        private Result<T> result = new Result<T>();

        /// <summary>
        /// 返回的错误代码
        /// </summary>
        public string Code => result.code;

        /// <summary>
        /// 返回的错误消息
        /// </summary>
        public string Message => result.message;

        /// <summary>
        /// 返回的数据
        /// </summary>
        public T Data => result.data;

        /// <summary>
        /// 返回的可选项
        /// </summary>
        public object Option => result.option;

        /// <summary>
        /// 构造函数，传入TokenHelper
        /// </summary>
        /// <param name="token">TokenHelper</param>
        public HttpClient(TokenHelper token)
        {
            this.token = token;
        }

        /// <summary>
        /// HttpRequest:GET方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Get(string url, string message = null)
        {
            if (Request(url)) return true;

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            Messages.ShowError(msg);
            return false;
        }

        /// <summary>
        /// HttpRequest:POST方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="data">POST的数据</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Post(string url, object data, string message = null)
        {
            if (Request(url, RequestMethod.POST, data)) return true;

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            Messages.ShowError(msg);
            return false;
        }

        /// <summary>
        /// HttpRequest:PUT方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="data">PUT的数据</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Put(string url, object data, string message = null)
        {
            if (Request(url, RequestMethod.PUT, data)) return true;

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            Messages.ShowError(msg);
            return false;
        }

        /// <summary>
        /// HttpRequest:DELETE方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="data">DELETE的数据，默认NULL</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Delete(string url, object data = null, string message = null)
        {
            if (Request(url, RequestMethod.DELETE, data)) return true;

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            Messages.ShowError(msg);
            return false;
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="method">请求方法</param>
        /// <param name="data">Body中的数据</param>
        /// <returns>bool 是否成功</returns>
        private bool Request(string url, RequestMethod method = RequestMethod.GET, object data = null)
        {
            var request = new HttpRequest(token?.token);
            if (!token?.success ?? false)
            {
                result.BadRequest("Auth服务异常，未能获取Token！");
                return false;
            }

            var body = new JavaScriptSerializer().Serialize(data ?? "");
            if (!request.Send(url, body, method))
            {
                result.BadRequest(request.Message);
                return false;
            } 

            result = Util.Deserialize<Result<T>>(request.Data);
            if (token != null && "401,406".Contains(Code))
            {
                token.GetTokens();
                return Request(url, method, data);
            }

#if DEBUG
            // 在DEBUG模式下且AccessToken有效时记录接口调用日志
            LogAsync(method.ToString(), url, Message);
#endif
            return result.successful;
        }

        /// <summary>
        /// 记录接口调用日志
        /// </summary>
        /// <param name="method">请求方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="message">接口返回消息</param>
        private void Log(string method, string url, string message)
        {
            var ts = DateTime.Now - time;
            var loginfo = new LogInfo
            {
                logUrl = $"{Util.GetAppSetting("BaseServer")}/logapi/v1.0/logs",
                token = token.token,
                code = "700101",
                source = "系统平台",
                action = "接口调用",
                message = $"[{method}]{url};{message};用时{ts.TotalMilliseconds}毫秒"
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
            if (token == null) return;

            new Task(() => Log(method, url, message)).Start();
        }
    }
}