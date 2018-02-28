using System;
using System.Threading.Tasks;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class HttpClient<T> where T : new()
    {
        private readonly TokenHelper token;
        private readonly DateTime time = DateTime.Now;
        private Result<T> result = new Result<T>();

        /// <summary>
        /// 返回的错误代码
        /// </summary>
        public string code => result.code;

        /// <summary>
        /// 返回的错误消息
        /// </summary>
        public string message => result.message;

        /// <summary>
        /// 返回的数据
        /// </summary>
        public T data => result.data;

        /// <summary>
        /// 返回的可选项
        /// </summary>
        public object option => result.option;

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
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Get(string url, string msg = null)
        {
            if (Request(url)) return true;

            var newline = string.IsNullOrEmpty(msg) ? "" : "\r\n";
            Messages.ShowError($"{message}{newline}{msg}");

            return false;
        }

        /// <summary>
        /// HttpRequest:POST方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="body">POST的数据</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Post(string url, object body, string msg = null)
        {
            if (Request(url, RequestMethod.POST, body)) return true;

            var newline = string.IsNullOrEmpty(msg) ? "" : "\r\n";
            Messages.ShowError($"{message}{newline}{msg}");

            return false;
        }

        /// <summary>
        /// HttpRequest:PUT方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="body">PUT的数据</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Put(string url, object body, string msg = null)
        {
            if (Request(url, RequestMethod.PUT, body)) return true;

            var newline = string.IsNullOrEmpty(msg) ? "" : "\r\n";
            Messages.ShowError($"{message}{newline}{msg}");

            return false;
        }

        /// <summary>
        /// HttpRequest:DELETE方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="body">DELETE的数据，默认NULL</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Delete(string url, object body = null, string msg = null)
        {
            if (Request(url, RequestMethod.DELETE, body)) return true;

            var newline = string.IsNullOrEmpty(msg) ? "" : "\r\n";
            Messages.ShowError($"{result.message}{newline}{msg}");

            return false;
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="u">接口URL</param>
        /// <param name="method">请求方法</param>
        /// <param name="body">Body中的数据</param>
        /// <returns>bool 是否成功</returns>
        private bool Request(string u, RequestMethod method = RequestMethod.GET, object body = null)
        {
            var request = new HttpRequest(token?.token);
            if (!token?.success ?? false)
            {
                result.BadRequest("Auth服务异常，未能获取Token！");
                return false;
            }

            if (!request.Send(u, method, body))
            {
                result.BadRequest(request.message);
                return false;
            } 

            result = Util.Deserialize<Result<T>>(request.data);
            if (token != null && "401,406".Contains(code))
            {
                token.GetTokens();
                return Request(u, method, body);
            }

#if DEBUG
            // 在DEBUG模式下且AccessToken有效时记录接口调用日志
            LogAsync(method.ToString(), u, message);
#endif
            if (result.successful) return true;

            result.data = new T();
            return false;
        }

        /// <summary>
        /// 记录接口调用日志
        /// </summary>
        /// <param name="method">请求方法</param>
        /// <param name="u">请求地址</param>
        /// <param name="m">接口返回消息</param>
        private void Log(string method, string u, string m)
        {
            var ts = DateTime.Now - time;
            var loginfo = new LogInfo
            {
                logUrl = $"{Util.GetAppSetting("BaseServer")}/logapi/v1.0/logs",
                token = token.token,
                code = "700101",
                source = "系统平台",
                action = "接口调用",
                message = $"[{method}]{u};{m};用时{ts.TotalMilliseconds}毫秒"
            };
            var log = new LogClient(loginfo);
            log.LogToServer();
        }

        /// <summary>
        /// 异步记录接口调用日志
        /// </summary>
        /// <param name="method">请求方法</param>
        /// <param name="u">请求地址</param>
        /// <param name="m">接口返回消息</param>
        private void LogAsync(string method, string u, string m)
        {
            if (token == null) return;

            new Task(() => Log(method, u, m)).Start();
        }
    }
}