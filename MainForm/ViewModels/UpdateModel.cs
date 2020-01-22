using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils.Extensions;
using Insight.Utils.BaseViewModels;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.ViewModels
{
    public class UpdateModel : BaseDialogModel<ClientFile, Update>
    {
        public bool restart;

        private List<ClientFile> updates;
        private readonly string root = Application.StartupPath;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="title">窗体标题</param>
        public UpdateModel(string title) : base(title, null, true)
        {
            view.Shown += (sender, args) => update();
        }

        /// <summary>
        /// 完成更新
        /// </summary>
        public void confirm()
        {
            callback("complete", new object[]{restart});
        }

        /// <summary>
        /// 检查客户端文件是否有更新
        /// </summary>
        /// <param name="files">远端文件信息</param>
        /// <returns>int 更新文件数</returns>
        public int checkUpdate(Dictionary<string, ClientFile> files)
        {
            // 读取本地客户端文件信息
            var appId = Setting.appId;
            var locals = new Dictionary<string, ClientFile>();
            Util.getClientFiles(locals, appId, root, ".bak");
            locals.ForEach(f => Util.deleteFile(f.Value.fullPath));

            locals = new Dictionary<string, ClientFile>();
            Util.getClientFiles(locals, appId, root, ".exe|.dll|.frl");

            // 根据服务器上文件信息，通过比对版本号得到可更新文件列表
            updates = (from sf in files
                let cf = locals.ContainsKey(sf.Key) ? locals[sf.Key] : null
                let cv = new Version(cf?.version ?? "1.0.0")
                let sv = new Version(sf.Value?.version ?? "1.0.0")
                where cf == null || cv < sv
                select sf.Value).ToList();
            return updates.Count;
        }

        /// <summary>
        /// 更新本地文件
        /// </summary>
        /// <param name="data">文件数据</param>
        public void updateFile(string data)
        {
            var buffer = Convert.FromBase64String(data);
            var bytes = Util.decompress(buffer);
            restart = Util.updateFile(item, root, bytes) || restart;
        }

        /// <summary>
        /// 生成批处理文件
        /// </summary>
        public ProcessStartInfo createBat()
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
        private void update()
        {
            view.confirm.Enabled = false;
            foreach (var file in updates)
            {
                item = file;
                view.Progress.EditValue = $@"正在更新：{item.name}……";
                view.Refresh();
                Thread.Sleep(1000);

                callback("getFile", new object[]{item});
            }

            view.confirm.Enabled = true;
            view.Progress.EditValue = restart ? "已更新关键文件，需要重新运行客户端程序！" : "更新完成！";
            view.confirm.Text = restart ? "重  启" : "关  闭";
            view.Refresh();
        }
    }
}
