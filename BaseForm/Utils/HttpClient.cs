using System.Collections.Generic;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Base.BaseForm.Utils
{
    public class HttpClient<T>
    {
        private readonly string url;
        private readonly bool showError;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="showError">是否显示错误信息</param>
        public HttpClient(string url, bool showError = true)
        {
            this.url = url;
            this.showError = showError;
        }

        /// <summary>
        /// 获取请求结果
        /// </summary>
        /// <param name="body">请求参数/Body中的数据</param>
        /// <returns>T</returns>
        public Result<T> getResult(object body = null)
        {
            return request(RequestMethod.GET, body);
        }
        
        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <param name="body">请求参数/Body中的数据</param>
        /// <returns>T</returns>
        public T getData(object body = null)
        {
            var result = request(RequestMethod.GET, body);
            return result.data;
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="method">请求方法</param>
        /// <param name="body">请求参数/Body中的数据</param>
        /// <returns>T</returns>
        public T commit(RequestMethod method, object body = null)
        {
            var result = request(method, body);
            return result.data;
        }

        /// <summary>
        /// HttpRequest:GET方法
        /// </summary>
        /// <param name="dict">URL参数集合</param>
        /// <returns>bool 是否成功</returns>
        public bool get(Dictionary<string, object> dict = null)
        {
            var result = request(RequestMethod.GET, dict);
            return result.success;
        }

        /// <summary>
        /// HttpRequest:POST方法
        /// </summary>
        /// <param name="body">POST的数据</param>
        /// <returns>bool 是否成功</returns>
        public bool post(object body = null)
        {
            var result = request(RequestMethod.POST, body);
            return result.success;
        }

        /// <summary>
        /// HttpRequest:PUT方法
        /// </summary>
        /// <param name="body">PUT的数据</param>
        /// <returns>bool 是否成功</returns>
        public bool put(object body = null)
        {
            var result = request(RequestMethod.PUT, body);
            return result.success;
        }

        /// <summary>
        /// HttpRequest:DELETE方法
        /// </summary>
        /// <param name="body">DELETE的数据，默认NULL</param>
        /// <returns>bool 是否成功</returns>
        public bool delete(object body = null)
        {
            var result = request(RequestMethod.DELETE, body);
            return result.success;
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="body">请求参数Body中的数据</param>
        /// <param name="method">请求方法</param>
        /// <returns>bool 是否成功</returns>
        public Result<T> request(RequestMethod method, object body)
        {
            while (true)
            {
                var helper = Setting.tokenHelper;
                var request = new HttpRequest(Setting.gateway + url, helper.accessToken);
                if (request.send(method, body))
                {
                    var result = Util.deserialize<Result<T>>(request.data);
                    if (result == null)
                    {
                        return new Result<T>().serverError($"Response data:{request.data}");
                    }

                    if (result.success) return result;

                    switch (result.code)
                    {
                        case 422 when !helper.refresTokens():
                            return result;
                        case 422:
                            continue;
                        case 421 when !helper.getTokens():
                            return result;
                        case 421:
                            continue;
                        default:
                            if (showError) Messages.showError(result.message);

                            return result;
                    }
                }

                if (showError) Messages.showError(request.message);

                return new Result<T>().badRequest(request.message);
            }
        }
    }
}