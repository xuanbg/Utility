using System.Threading;
using DevExpress.Utils;
using Insight.Base.BaseForm.Entities;
using Insight.Base.BaseForm.ViewModels;
using Insight.Base.MainForm.Views;

namespace Insight.Base.MainForm.ViewModels
{
    public class UpdateModel : BaseDialogModel<Update, UpdateDialog>
    {
        public bool exit;

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
                if (!item.update)
                {
                    view.LabFile.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    view.LabFile.Text = $"有{item.data.Count}个文件需要更新，请点击更新按钮开始更新。";

                    return;
                }

                startUpdate();
            };
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

            view.sbeUpdate.Visible = false;
            view.confirm.Text = exit ? "重  启" : "关  闭";
            view.confirm.Visible = true;
            view.confirm.Select();

            view.LabFile.Text = exit ? "已更新关键文件，需要重新运行客户端程序！" : "更新完成！";
            view.LabFile.Appearance.TextOptions.HAlignment = HorzAlignment.Center; view.confirm.Select();
            view.Refresh();
        }
    }
}
