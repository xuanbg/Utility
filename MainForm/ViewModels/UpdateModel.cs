using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using Insight.Base.BaseForm.Entities;
using Insight.Base.BaseForm.ViewModels;
using Insight.Base.MainForm.Views;

namespace Insight.Base.MainForm.ViewModels
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
            view.confirm.Visible = false;
            view.close.Visible = false;
            view.sbeUpdate.Click += (sender, args) => startUpdate();
            view.Shown += (sender, args) =>
            {
                if (item.update)
                {
                    startUpdate();
                    return;
                }

                view.LabFile.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                view.LabFile.Text = $"有{item.data.Count}个文件需要更新，请点击更新按钮开始更新。";
            };
        }

        /// <summary>
        /// 更新重启标识
        /// </summary>
        /// <param name="restart">是否需要重启</param>
        public void updateFlag(bool restart)
        {
            this.restart = this.restart || restart;
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
            view.LabFile.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
            view.cancel.Visible = false;
            view.sbeUpdate.Enabled = false;
            foreach (var version in item.data)
            {
                count++;
                view.LabFile.Text = $"正在更新：{version.file}……";
                view.Refresh();

                Thread.Sleep(1000);
                callback("updateFile", new object[] { version });
                view.pceUpdate.Position = 100 * count / item.data.Count;
                view.pceUpdate.Refresh();
            }

            view.confirm.Text = restart ? "重  启" : "关  闭";
            view.LabFile.Text = restart ? "已更新关键文件，需要重新运行客户端程序！" : "更新完成！";
            view.LabFile.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            view.confirm.Visible = true;
            view.sbeUpdate.Visible = false;
            view.Refresh();
        }
    }
}
