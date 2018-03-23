using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils.Extensions;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;
using Insight.Utils.MainForm.Views;
using Insight.Utils.Models;

namespace Insight.Utils.MainForm.Models
{
    public class UpdateModel : BaseModel
    {
        public Update view = new Update();
        public bool restart;

        private List<ClientFile> updates;
        private readonly string root = Application.StartupPath;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UpdateModel()
        {
            view.Shown += (sender, args) => Update();
        }

        /// <summary>
        /// 检查客户端文件更新
        /// </summary>
        /// <returns>int 更新文件数</returns>
        public int CheckUpdate()
        {
            // 读取本地客户端文件信息
            var appId = Setting.tokenHelper.appId;
            var locals = new Dictionary<string, ClientFile>();
            Util.GetClientFiles(locals, appId, root, ".bak");
            locals.ForEach(f => Util.DeleteFile(f.Value.fullPath));

            locals = new Dictionary<string, ClientFile>();
            Util.GetClientFiles(locals, appId, root, ".exe|.dll|.frl");

            // 根据服务器上文件信息，通过比对版本号得到可更新文件列表
            updates = (from sf in GetFiles(appId)
                        let cf = locals[sf.Key]
                        let cv = new Version(cf?.version ?? "1.0.0")
                        let sv = new Version(sf.Value?.version ?? "1.0.0")
                        where cf == null || cv < sv
                        select sf.Value).ToList();
            return updates.Count;
        }

        /// <summary>
        /// 生成批处理文件
        /// </summary>
        public ProcessStartInfo CreateBat()
        {
            using (var bat = File.CreateText("restart.bat"))
            {
                bat.WriteLine(@"start """" ""{0}""", Application.ExecutablePath);
                bat.WriteLine("del /s /q *.bak");
                bat.WriteLine("del %0%");
            }

            return new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = "restart.bat",
                WindowStyle = ProcessWindowStyle.Hidden,
            };
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        private void Update()
        {
            view.Confirm.Enabled = false;
            foreach (var file in updates)
            {
                view.Progress.EditValue = $"正在更新：{file.name}……";
                view.Refresh();
                Thread.Sleep(1000);
                var data = GetFile(file.id);
                if (data == null) continue;

                var buffer = Convert.FromBase64String(data);
                var bytes = Util.Decompress(buffer);
                restart = Util.UpdateFile(file, root, bytes) || restart;
            }

            view.Confirm.Enabled = true;
            view.Progress.EditValue = restart ? "已更新关键文件，需要重新运行客户端程序！" : "更新完成！";
            view.Confirm.Text = restart ? "重  启" : "关  闭";
            view.Refresh();
        }

        /// <summary>
        /// 获取服务器上的客户端文件版本信息
        /// </summary>
        /// <param name="id">应用ID</param>
        /// <returns>文件版本信息</returns>
        private Dictionary<string, ClientFile> GetFiles(string id)
        {
            var url = $"{baseServer}/commonapi/v1.0/apps/{id}/files";
            var client = new HttpClient<Dictionary<string, ClientFile>>(tokenHelper);

            return client.Get(url) ? client.data : new Dictionary<string, ClientFile>();
        }

        /// <summary>
        /// 根据文件ID获取更新文件
        /// </summary>
        /// <param name="id">更新文件ID</param>
        /// <returns>Result</returns>
        private string GetFile(string id)
        {
            var url = $"{baseServer}/commonapi/v1.0/apps/files/{id}";
            var client = new HttpClient<object>(tokenHelper);

            return client.Get(url) ? client.data.ToString() : null;
        }
    }
}
