using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.MainForm
{
    internal class DataModel
    {
        private const string authService = "/base/auth";
        private const string userService = "/base/user";

        /// <summary>
        /// 获取导航数据
        /// </summary>
        /// <returns>导航数据集合</returns>
        internal List<ModuleDto> getNavigators()
        {
            var url = $"{authService}/v1.0/navigators";
            var client = new HttpClient<List<ModuleDto>>();

            return client.getData(url);
        }

        /// <summary>
        /// 获取可登录部门列表
        /// </summary>
        /// <param name="account">登录账号</param>
        /// <returns>可登录部门列表</returns>
        internal List<TreeLookUpMember> getDepts(string account)
        {
            var url = $"{authService}/v1.0/departments";
            var dict = new Dictionary<string, object> {{"account", account}};
            var client = new HttpClient<List<TreeLookUpMember>>();

            return client.request(url, dict);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        internal bool changPassword(PasswordDto dto)
        {
            const string msg = "更换密码失败！请检查网络状况，并再次进行更换密码操作。";
            var url = $"{userService}/v1.0/users/password";
            var client = new HttpClient<object>();

            return client.put(url, dto, msg);
        }

        /// <summary>
        /// 获取服务器上的客户端文件版本信息
        /// </summary>
        /// <param name="isStart">是否启动</param>
        /// <returns>文件版本信息</returns>
        internal Update checkUpdate(bool isStart)
        {
            var url = $"{Setting.updateUrl}/update.json";
            var client = new HttpRequest();
            if (!client.send(url))
            {
                if (!isStart) Messages.showError("无法获取更新信息，请稍后再试……");

                return null;
            }

            // 获取更新信息
            var info = Util.deserialize<Update>(client.data);
            var root = Application.StartupPath;

            // 清除本地备份
            var locals = Util.getClientFiles(".bak");
            locals.ForEach(i =>
            {
                var filePath = root;
                if (string.IsNullOrEmpty(i.localPath)) filePath = $"{filePath}\\{i.localPath}\\";

                Util.deleteFile(filePath + i.file);
            });

            // 比较文件版本
            locals = Util.getClientFiles(".exe|.dll|.frl");
            var updates = new List<FileVersion>();
            foreach (var ver in info.data)
            {
                var cf = locals.FirstOrDefault(i => i.file == ver.file && i.localPath == ver.localPath);
                if (cf == null)
                {
                    updates.Add(ver);
                    continue;
                }

                var cv = new Version(cf.version);
                var sv = new Version(ver.version);
                if (cv.CompareTo(sv) >= 0) continue;

                updates.Add(ver);
            }
            info.data = updates;

            return info;
        }

        /// <summary>
        /// 根据URL获取更新文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>Result</returns>
        internal byte[] getFile(string file)
        {
            var url = $"{Setting.updateUrl}/{file}";
            using (var stream = WebRequest.Create(url).GetResponse().GetResponseStream())
            {
                if (stream == null) return null;

                var buffer = new byte[1024];
                using (var ms = new MemoryStream())
                {
                    int actual;
                    while ((actual = stream.Read(buffer, 0, 1024)) > 0)
                    {
                        ms.Write(buffer, 0, actual);
                    }

                    ms.Position = 0;

                    return ms.ToArray();
                }
            }
        }
    }
}
