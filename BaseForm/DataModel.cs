using System.Collections.Generic;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils
{
    internal class DataModel
    {
        /// <summary>
        /// 获取模块功能按钮集合
        /// </summary>
        /// <returns>功能按钮集合</returns>
        internal IEnumerable<FunctionDto> getActions(string moduleId)
        {
            var url = $"/base/auth/v1.0/navigators/{moduleId}/functions";
            var client = new HttpClient<List<FunctionDto>>();

            return client.getData(url);
        }

        /// <summary>
        /// 获取选项数据
        /// </summary>
        /// <returns>选项数据集合</returns>
        internal List<ModuleParam> getParams()
        {
            var url = "/common/v1.0/params";
            var client = new HttpClient<List<ModuleParam>>();

            return client.getData(url);
        }

        /// <summary>
        /// 保存选项数据
        /// </summary>
        /// <param name="moduleParams">选项数据集合</param>
        /// <returns>bool 是否成功</returns>
        internal void saveParam(List<ModuleParam> moduleParams)
        {
            var url = "/common/v1.0/params";
            var dict = new Dictionary<string, object> { { "list", moduleParams } };
            var client = new HttpClient<List<ModuleParam>>();
            client.put(url, dict);
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="item">分类数据对象</param>
        /// <returns>分类对象实体</returns>
        public Catalog<T> add<T>(string url, Catalog<T> item)
        {
            var msg = "新建分类失败！";
            var dict = new Dictionary<string, object> { { "catalog", item } };
            var client = new HttpClient<Catalog<T>>();

            return client.post(url, dict, msg) ? client.data : null;
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="url">接口URL</param>
        /// <param name="item">分类数据对象</param>
        /// <returns>分类对象实体</returns>
        public Catalog<T> edit<T>(string url, Catalog<T> item)
        {
            var msg = "编辑分类失败！";
            var dict = new Dictionary<string, object> { { "catalog", item } };
            var client = new HttpClient<object>();

            return client.put(url, dict, msg) ? item : null;
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="id">模板ID</param>
        /// <returns>string 模板内容</returns>
        internal string getTemplate(string id)
        {
            var url = $"/report/v1.0/templates/{id}";
            var client = new HttpClient<object>();

            return client.getData(url)?.ToString();
        }

        /// <summary>
        /// 获取电子影像
        /// </summary>
        /// <param name="id">电子影像ID</param>
        /// <returns>ImageData 电子影像对象</returns>
        internal ImageData getImage(string id)
        {
            var url = $"/report/v1.0/images/{id}";
            var client = new HttpClient<ImageData>();

            return client.getData(url);
        }

        /// <summary>
        /// 新增电子影像
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="tid">模板ID</param>
        /// <returns></returns>
        internal ImageData newImage(string id, string tid)
        {
            var url = $"/report/v1.0/images/{id}";
            var client = new HttpClient<ImageData>();
            var dict = new Dictionary<string, object>
            {
                {"templateId", tid},
                {"deptName", Setting.deptName}
            };

            return client.getData(url, RequestMethod.POST, dict);
        }
    }
}
