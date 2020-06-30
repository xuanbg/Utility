using System.Collections.Generic;
using System.Linq;
using Insight.Base.BaseForm.Entities;

namespace Insight.Base.BaseForm.Utils
{
    public class BaseDataModel
    {
        /// <summary>
        /// 获取指定键名的键值集合
        /// </summary>
        /// <param name="key">字典键名</param>
        /// <returns>键值集合</returns>
        public List<DictKeyDto> getDictValues(string key)
        {
            const string url = "/common/dict/v1.0/dicts/values";
            var dict = new Dictionary<string, object> {{"key", key}};
            var client = new HttpClient<List<DictKeyDto>>();

            return client.getData(url, dict);
        }

        /// <summary>
        /// 获取选项数据
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <param name="keys">参数键集合</param>
        /// <param name="userId">用户ID</param>
        /// <returns>ModuleParam 选项数据</returns>
        public List<ModuleParam> getParams(string moduleId, IEnumerable<string> keys, string userId = null)
        {
            const string url = "/common/param/v1.0/params";
            var dict = new Dictionary<string, object> {{"moduleId", moduleId}};
            var client = new HttpClient<List<ModuleParam>>();
            var list = client.getData(url, dict);

            return keys.Select(key => list.FirstOrDefault(i => i.key == key && i.userId == userId)
                                      ?? new ModuleParam {moduleId = moduleId, key = key, userId = userId}).ToList();
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
            const string url = "/common/param/v1.0/params/value";
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