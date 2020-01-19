using System.Collections.Generic;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class HttpClient<T> where T : new()
    {
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
        /// 获取请求数据
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="method">请求方法</param>
        /// <param name="dict">参数集合</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>T</returns>
        public T getData(string url, RequestMethod method = RequestMethod.GET, Dictionary<string, object> dict = null,
            string msg = null)
        {
            request(url, method, dict, msg);

            return data;
        }

        /// <summary>
        /// 获取请求结果
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="method">请求方法</param>
        /// <param name="dict">参数集合</param>
        /// <returns>T</returns>
        public Result<T> getResult(string url, RequestMethod method = RequestMethod.GET,
            Dictionary<string, object> dict = null)
        {
            request(url, method, dict, null, false);

            return result;
        }

        /// <summary>
        /// HttpRequest:GET方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="dict">参数集合</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool get(string url, Dictionary<string, object> dict = null, string msg = null)
        {
            return request(url, RequestMethod.GET, dict, msg);
        }

        /// <summary>
        /// HttpRequest:POST方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="dict">POST的数据</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool post(string url, Dictionary<string, object> dict = null, string msg = null)
        {
            return request(url, RequestMethod.POST, dict, msg);
        }

        /// <summary>
        /// HttpRequest:PUT方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="dict">PUT的数据</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool put(string url, Dictionary<string, object> dict = null, string msg = null)
        {
            return request(url, RequestMethod.PUT, dict, msg);
        }

        /// <summary>
        /// HttpRequest:DELETE方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="dict">DELETE的数据，默认NULL</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool delete(string url, Dictionary<string, object> dict = null, string msg = null)
        {
            return request(url, RequestMethod.DELETE, dict, msg);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="method">请求方法</param>
        /// <param name="dict">请求参数/Body中的数据</param>
        /// <returns>T</returns>
        public T request(string url, RequestMethod method = RequestMethod.GET, Dictionary<string, object> dict = null)
        {
            var request = new HttpRequest();
            if (request.send(Setting.gateway + url, method, dict))
            {
                result = Util.deserialize<Result<T>>(request.data);
                if (result.success) return result.data;
            }
            else
            {
                result.badRequest(request.message);
            }

            result.data = typeof(T).Name.Contains("List") ? new T() : default(T);
            Messages.showError(result.message);

            return data;
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="method">请求方法</param>
        /// <param name="dict">请求参数/Body中的数据</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <param name="log">是否输出错误消息</param>
        /// <returns>bool 是否成功</returns>
        private bool request(string url, RequestMethod method, Dictionary<string, object> dict, string msg, bool log = true)
        {
            while (true)
            {
                var helper = Setting.tokenHelper;
                var request = new HttpRequest(helper.accessToken);
                if (request.send(Setting.gateway + url, method, dict))
                {
                    result = Util.deserialize<Result<T>>(request.data);
                    if (result == null)
                    {
                        result = new Result<T>().serverError($"Response data:{request.data}");
                        return false;
                    }

                    if (result.success) return true;

                    switch (code)
                    {
                        case "422" when !helper.refresTokens():
                            return false;
                        case "422":
                            continue;
                        case "421" when !helper.getTokens():
                            return false;
                        case "421":
                            continue;
                    }
                }
                else
                {
                    result.badRequest(request.message);
                }

                result.data = typeof(T).Name.Contains("List") ? new T() : default(T);
                if (!log) return false;

                var newline = string.IsNullOrEmpty(msg) ? "" : "\r\n";
                Messages.showError($"{result.message}{newline}{msg}");

                return false;
            }
        }
    }
}