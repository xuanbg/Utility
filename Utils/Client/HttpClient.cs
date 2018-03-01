using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class HttpClient<T> where T : new()
    {
        private readonly TokenHelper helper;
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
        /// <param name="helper">TokenHelper</param>
        public HttpClient(TokenHelper helper)
        {
            this.helper = helper;
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
        /// <param name="url">接口URL</param>
        /// <param name="method">请求方法</param>
        /// <param name="body">Body中的数据</param>
        /// <returns>bool 是否成功</returns>
        private bool Request(string url, RequestMethod method = RequestMethod.GET, object body = null)
        {
            if (helper?.token == null)
            {
                result.BadRequest("Auth服务异常，未能获取Token！");
            }
            else
            {
                var request = new HttpRequest(helper.token);
                if (request.Send(url, method, body))
                {
                    result = Util.Deserialize<Result<T>>(request.data);
                    if ("401,406".Contains(code))
                    {
                        helper.GetTokens();
                        return Request(url, method, body);
                    }

                    if (result.successful) return true;

                }

                result.BadRequest(request.message);
            }

            if (typeof(T).Name.Contains("List")) result.data = new T();

            return false;
        }
    }
}