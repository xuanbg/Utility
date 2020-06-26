using System.Collections.Generic;
using Insight.Base.BaseForm.Entities;

namespace Insight.Base.BaseForm.Utils
{
    public class BaseDataModel
    {
        /// <summary>
        /// 获取选项数据
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns>ModuleParam 选项数据</returns>
        public List<ModuleParam> getParams(string moduleId)
        {
            const string url = "/common/param/v1.0/params";
            var dict = new Dictionary<string, object> {{"moduleId", moduleId}};
            var client = new HttpClient<List<ModuleParam>>();

            return client.getData(url, dict);
        }

        /// <summary>
        /// 获取选项参数
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <param name="key">参数键</param>
        /// <param name="userId">用户ID</param>
        /// <returns>ModuleParam 选项参数</returns>
        public ModuleParam getParam(string moduleId, string key, string userId = null)
        {
            const string url = "/common/param/v1.0/params";
            var dict = new Dictionary<string, object> {{"moduleId", moduleId}, {"keyword", key}, {"userId", userId}};
            var client = new HttpClient<ModuleParam>();

            var param = client.getData(url, dict);
            if (param != null) return param;

            return new ModuleParam
            {
                moduleId = moduleId,
                key = key,
                userId = userId
            };
        }

        /// <summary>
        /// 保存配置参数
        /// </summary>
        /// <param name="moduleParams">配置参数集合</param>
        /// <returns>bool 是否成功</returns>
        public bool setParam(List<ModuleParam> moduleParams)
        {
            const string url = "/common/param/v1.0/params";
            var msg = "保存选项数据失败！";
            var client = new HttpClient<object>();

            return client.put(url, moduleParams, msg);
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="id">模板ID</param>
        /// <returns>string 模板内容</returns>
        public string getTemplate(string id)
        {
            var url = $"/common/report/v1.0/templates/{id}/content";
            var client = new HttpClient<string>();

            return client.getData(url);
        }
    }
}
