using System.Collections.Generic;
using System.Linq;
using Insight.Base.BaseForm.Entities;
using Insight.Utils.Entity;

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
            var client = new HttpClient<List<DictKeyDto>>(url);

            return client.getData(dict);
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
            var client = new HttpClient<List<ModuleParam>>(url);
            var list = client.getData(dict);

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
            var client = new HttpClient<ModuleParam>(url);

            var param = client.getData(dict);
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
            var client = new HttpClient<object>(url);

            return client.put(moduleParams);
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="id">模板ID</param>
        /// <returns>string 模板内容</returns>
        public string getTemplate(string id)
        {
            var url = $"/common/report/v1.0/templates/{id}/content";
            var client = new HttpClient<string>(url);

            return client.getData();
        }

        /// <summary>
        /// 获取全部省级行政区划
        /// </summary>
        /// <returns>省级行政区划集合</returns>
        public List<LookUpMember> getProvinces()
        {
            const string url = "/common/area/v1.0/areas/provinces";
            var client = new HttpClient<List<LookUpMember>>(url);

            return client.getData();
        }

        /// <summary>
        /// 获取指定上级ID的行政区划集合数据
        /// </summary>
        /// <param name="id">上级区划ID</param>
        /// <returns>行政区划集合</returns>
        public List<Region> getRegions(string id)
        {
            var url = $"/common/area/v1.0/areas/{id}/subs";
            var client = new HttpClient<List<Region>>(url);

            return client.getData();
        }

        /// <summary>
        /// 获取提交数据用临时Token
        /// </summary>
        /// <param name="key">接口Hash: MD5(userId:method:url)</param>
        /// <returns>临时Token</returns>
        public string getToken(string key)
        {
            const string url = "/base/auth/v1.0/tokens";
            var dict = new Dictionary<string, object> {{"key", key}};
            var client = new HttpClient<string>(url);

            return client.getData(dict);
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="type">验证码类型:0.验证手机号;1.用户注册;2.重置密码;3.修改支付密码;4.修改手机号</param>
        /// <param name="mobile">手机号</param>
        /// <param name="length">验证码长度</param>
        /// <param name="minutes">验证码有效时长(分钟)</param>
        /// <returns>临时Token</returns>
        public bool seedSmsCode(int type, string mobile, int length = 6, int minutes = 15)
        {
            const string url = "/common/message/v1.0/codes";
            var dict = new Dictionary<string, object>
            {
                {"type", type},
                {"mobile", mobile},
                {"length", length},
                {"minutes", minutes}
            };
            var client = new HttpClient<string>(url);

            return client.post(dict);
        }

        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <param name="key">验证参数,MD5(type + mobile + code)</param>
        /// <returns>临时Token</returns>
        public bool verifySmsCode(string key)
        {
            var url = $"/common/message/v1.0/codes/{key}/status";
            var dict = new Dictionary<string, object> {{"isCheck", true}};
            var client = new HttpClient<string>(url);
            var result = client.getResult(dict);

            return result.success;
        }
    }
}