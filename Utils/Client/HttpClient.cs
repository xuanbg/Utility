using System.Collections.Generic;
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
        /// <param name="dict">参数集合</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Get(string url, Dictionary<string, object> dict = null, string msg = null)
        {
            if (Request(url, RequestMethod.GET, dict)) return true;

            var newline = string.IsNullOrEmpty(msg) ? "" : "\r\n";
            Messages.ShowError($"{message}{newline}{msg}");

            return false;
        }

        /// <summary>
        /// HttpRequest:POST方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="dict">POST的数据</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Post(string url, Dictionary<string, object> dict, string msg = null)
        {
            if (Request(url, RequestMethod.POST, dict)) return true;

            var newline = string.IsNullOrEmpty(msg) ? "" : "\r\n";
            Messages.ShowError($"{message}{newline}{msg}");

            return false;
        }

        /// <summary>
        /// HttpRequest:PUT方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="dict">PUT的数据</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Put(string url, Dictionary<string, object> dict, string msg = null)
        {
            if (Request(url, RequestMethod.PUT, dict)) return true;

            var newline = string.IsNullOrEmpty(msg) ? "" : "\r\n";
            Messages.ShowError($"{message}{newline}{msg}");

            return false;
        }

        /// <summary>
        /// HttpRequest:DELETE方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="dict">DELETE的数据，默认NULL</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool Delete(string url, Dictionary<string, object> dict = null, string msg = null)
        {
            if (Request(url, RequestMethod.DELETE, dict)) return true;

            var newline = string.IsNullOrEmpty(msg) ? "" : "\r\n";
            Messages.ShowError($"{result.message}{newline}{msg}");

            return false;
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="method">请求方法</param>
        /// <param name="dict">请求参数/Body中的数据</param>
        /// <returns>bool 是否成功</returns>
        private bool Request(string url, RequestMethod method, Dictionary<string, object> dict = null)
        {
            if (helper?.token == null)
            {
                result.BadRequest("Auth服务异常，未能获取Token！");
            }
            else
            {
                var request = new HttpRequest(helper.token);
                if (request.Send(url, method, dict))
                {
                    result = Util.Deserialize<Result<T>>(request.data);
                    if (result.successful) return true;

                    switch (code)
                    {
                        case "405":
                            helper.RefresTokens();
                            return Request(url, method, dict);

                        case "406":
                            helper.GetTokens();
                            return Request(url, method, dict);
                    }
                }
                else
                {
                    result.BadRequest(request.message);
                }
            }

            result.data = typeof(T).Name.Contains("List") ? new T() : default(T);
            return false;
        }
    }
}