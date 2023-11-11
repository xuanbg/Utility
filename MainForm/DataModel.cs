using System.Collections.Generic;
using Insight.Base.BaseForm.Entities;
using Insight.Base.BaseForm.Utils;

namespace Insight.Base.MainForm
{
    internal class DataModel
    {
        private const string authService = "/base/auth";
        private const string userService = "/base/user";

        /// <summary>
        /// 获取可登录租户列表
        /// </summary>
        /// <param name="account">登录账号</param>
        /// <returns>可登录租户列表</returns>
        internal List<LookUpMember> getTenants(string account)
        {
            var url = $"{authService}/v1.0/{Setting.appId}/tenants";
            var dict = new Dictionary<string, object> {{"account", account}};
            var client = new HttpClient<List<LookUpMember>>(url);

            return client.getData(dict);
        }

        /// <summary>
        /// 获取导航数据
        /// </summary>
        /// <returns>导航数据集合</returns>
        internal List<ModuleDto> getNavigators()
        {
            var url = $"{authService}/v1.0/navigators";
            var client = new HttpClient<List<ModuleDto>>(url);

            return client.getData();
        }

        /// <summary>
        /// 获取模块功能按钮集合
        /// </summary>
        /// <returns>功能按钮集合</returns>
        internal List<FunctionDto> getActions(string moduleId)
        {
            var url = $"{authService}/v1.0/navigators/{moduleId}/functions";
            var client = new HttpClient<List<FunctionDto>>(url);

            return client.getData();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        internal bool changPassword(PasswordDto dto)
        {
            var url = $"{userService}/v1.0/users/password";
            var client = new HttpClient<object>(url);

            return client.put(dto);
        }
    }
}
