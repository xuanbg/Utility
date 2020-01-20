using System.Collections.Generic;
using Insight.Utils.Client;
using Insight.Utils.Entity;
using Insight.Utils.MainForm.Dtos;

namespace Insight.Utils.MainForm
{
    internal class Model
    {
        /// <summary>
        /// 获取导航数据
        /// </summary>
        /// <returns>导航数据集合</returns>
        internal List<ModuleDto> getNavigators()
        {
            var url = "/base/auth/v1.0/navigators";
            var client = new HttpClient<List<ModuleDto>>();

            return client.getData(url);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        internal bool changPassword(PasswordDto dto)
        {
            const string msg = "更换密码失败！请检查网络状况，并再次进行更换密码操作。";
            var url = "/base/user/v1.0/users/password";
            var client = new HttpClient<object>();

            return client.put(url, dto, msg);
        }

        /// <summary>
        /// 获取可登录部门列表
        /// </summary>
        /// <param name="account">登录账号</param>
        /// <returns>可登录部门列表</returns>
        internal List<TreeLookUpMember> getDepts(string account)
        {
            var url = $"/base/user/v1.0/users/{account}/depts";
            var client = new HttpClient<List<TreeLookUpMember>>();

            return client.request(url);
        }

        /// <summary>
        /// 获取服务器上的客户端文件版本信息
        /// </summary>
        /// <param name="id">应用ID</param>
        /// <returns>文件版本信息</returns>
        internal Dictionary<string, ClientFile> getFiles(string id)
        {
            var url = $"/commonapi/v1.0/apps/{id}/files";
            var client = new HttpClient<Dictionary<string, ClientFile>>();

            return client.get(url) ? client.data : new Dictionary<string, ClientFile>();
        }

        /// <summary>
        /// 根据文件ID获取更新文件
        /// </summary>
        /// <param name="id">更新文件ID</param>
        /// <returns>Result</returns>
        internal string getFile(string id)
        {
            var url = $"/commonapi/v1.0/apps/files/{id}";
            var client = new HttpClient<object>();

            return client.get(url) ? client.data.ToString() : null;
        }
    }
}
