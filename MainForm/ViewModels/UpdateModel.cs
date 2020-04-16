using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Insight.Utils.BaseViewModels;
using Insight.Utils.Common;
using Insight.Utils.Entity;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.ViewModels
{
    public class UpdateModel : BaseDialogModel<Update, UpdateDialog>
    {
        public bool restart;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="title">窗体标题</param>
        /// <param name="item">更新信息</param>
        public UpdateModel(string title, Update item) : base(title, item)
        {
            view.sbeUpdate.Click += (sender, args) => startUpdate();
        }

        /// <summary>
        /// 更新本地文件
        /// </summary>
        /// <param name="version"></param>
        /// <param name="data">文件数据</param>
        public void updateFile(FileVersion version, string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            var buffer = Convert.FromBase64String(data);
            var bytes = Util.decompress(buffer);
            restart = Util.updateFile(version, bytes) || restart;
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
                WindowStyle = ProcessWindowStyle.Hidden
            };
        }

        /// <summary>
        /// 完成更新
        /// </summary>
        public new void confirm()
        {
            callback("complete", new object[] { restart });
        }

        /// <summary>
        /// 开始更新
        /// </summary>
        private void startUpdate()
        {
            var count = 0;
            view.sbeUpdate.Visible = false;
            view.cancel.Visible = false;
            foreach (var version in item.data)
            {
                view.LabFile.Text = $"正在更新：{version.file}……";
                view.pceUpdate.Position = 100 * count / item.data.Count;
                count++;
                view.Refresh();
                Thread.Sleep(1000);

                callback("updateFile", new object[] { version });
            }

            view.confirm.Enabled = true;
            view.pceUpdate.EditValue = restart ? "已更新关键文件，需要重新运行客户端程序！" : "更新完成！";
            view.confirm.Text = restart ? "重  启" : "关  闭";
            view.Refresh();
        }
    }
}
