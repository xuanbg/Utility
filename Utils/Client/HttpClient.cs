using System.Collections.Generic;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class HttpClient<T> where T : new()
    {
        private Result<T> result = new Result<T>();

        /// <summary>
        /// 返回是否成功
        /// </summary>
        public bool success => result.success;

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
        /// <param name="body">请求参数/Body中的数据</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>T</returns>
        public T getData(string url, RequestMethod method = RequestMethod.GET, object body = null, string msg = null)
        {
            request(url, method, body, msg);

            return data;
        }

        /// <summary>
        /// 获取请求结果
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="method">请求方法</param>
        /// <param name="body">请求参数/Body中的数据</param>
        /// <returns>T</returns>
        public Result<T> getResult(string url, RequestMethod method = RequestMethod.GET, object body = null)
        {
            request(url, method, body, null, false);

            return result;
        }

        /// <summary>
        /// HttpRequest:GET方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="dict">URL参数集合</param>
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
        /// <param name="body">POST的数据</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool post(string url, object body = null, string msg = null)
        {
            return request(url, RequestMethod.POST, body, msg);
        }

        /// <summary>
        /// HttpRequest:PUT方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="body">PUT的数据</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool put(string url, object body = null, string msg = null)
        {
            return request(url, RequestMethod.PUT, body, msg);
        }

        /// <summary>
        /// HttpRequest:DELETE方法
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="body">DELETE的数据，默认NULL</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool delete(string url, object body = null, string msg = null)
        {
            return request(url, RequestMethod.DELETE, body, msg);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="method">请求方法</param>
        /// <param name="body">请求参数/Body中的数据</param>
        /// <returns>T</returns>
        public T request(string url, RequestMethod method = RequestMethod.GET, object body = null)
        {
            var request = new HttpRequest();
            if (request.send(Setting.gateway + url, method, body))
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
        /// <param name="body">请求参数/Body中的数据</param>
        /// <param name="msg">错误消息，默认NULL</param>
        /// <param name="log">是否输出错误消息</param>
        /// <returns>bool 是否成功</returns>
        private bool request(string url, RequestMethod method, object body, string msg, bool log = true)
        {
            while (true)
            {
                var helper = Setting.tokenHelper;
                var request = new HttpRequest(helper.accessToken);
                if (request.send(Setting.gateway + url, method, body))
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