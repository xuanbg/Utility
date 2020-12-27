using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Insight.Base.BaseForm.Entities;
using Insight.Base.BaseForm.Utils;
using Insight.Utils.Common;

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

        /// <summary>
        /// 获取服务器上的客户端文件版本信息
        /// </summary>
        /// <param name="isStart">是否启动</param>
        /// <returns>文件版本信息</returns>
        internal Update checkUpdate(bool isStart)
        {
            var url = $"{Setting.updateUrl}/update.json";
            var client = new HttpRequest(url);
            if (!client.send())
            {
                if (!isStart) Messages.showError("无法获取更新信息，请稍后再试……");

                return null;
            }

            // 获取更新信息
            var info = Util.deserialize<Update>(client.data);
            var root = Application.StartupPath;

            // 清除本地备份
            var locals = getClientFiles(".bak");
            locals.ForEach(i =>
            {
                var filePath = root;
                if (string.IsNullOrEmpty(i.localPath)) filePath = $"{filePath}\\{i.localPath}\\";

                deleteFile(filePath + i.file);
            });

            // 比较文件版本
            locals = getClientFiles(".exe|.dll|.frl");
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
            try
            {
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
            catch(Exception)
            {
                Messages.showError($"下载文件【{file}】失败，已跳过该文件。");
                return null;
            }
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="version">版本信息</param>
        /// <param name="bytes">文件字节流</param>
        /// <returns>bool 是否重命名</returns>
        public bool updateFile(FileVersion version, byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return false;

            var rename = false;
            var filePath = Application.StartupPath;
            if (string.IsNullOrEmpty(version.localPath)) filePath = $"{filePath}\\{version.localPath}\\";

            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

            var file = filePath + version.file;
            try
            {
                File.Delete(file);
            }
            catch
            {
                File.Move(file, file + ".bak");
                rename = true;
            }

            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            return rename;
        }

        /// <summary>
        /// 获取本地文件列表
        /// </summary>
        /// <param name="ext">扩展名，默认为*.*，表示全部文件；否则列举扩展名，例如：".exe|.dll"</param>
        /// <param name="path">当前目录</param>
        private static List<FileVersion> getClientFiles(string ext = "*.*", string path = null)
        {
            // 读取目录下文件信息
            var root = Application.StartupPath;
            var dirInfo = new DirectoryInfo(path ?? root);
            var files = dirInfo.GetFiles().Where(f => f.DirectoryName != null && (ext == "*.*" || ext.Contains(f.Extension)));

            return files.Select(file => new FileVersion
            {
                file = file.Name,
                version = FileVersionInfo.GetVersionInfo(file.FullName).FileVersion,
                localPath = file.DirectoryName == root ? null : file.DirectoryName?.Replace(root, "")
            }).ToList();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="warning">是否显示删除信息</param>
        /// <returns>bool 是否删除成功</returns>
        private static void deleteFile(string path, bool warning = false)
        {
            if (string.IsNullOrEmpty(path)) return;

            if (!File.Exists(path))
            {
                Messages.showWarning("未找到指定的文件！");
                return;
            }

            try
            {
                File.Delete(path);
                if (warning) Messages.showMessage("指定的文件已删除！");
            }
            catch
            {
                Messages.showWarning("未能删除指定的文件！");
            }
        }
    }
}
