using System.Windows.Forms;
using Insight.Utils.BaseForm;
using Insight.Utils.Common;

namespace Insight.Utils.BaseViewModels
{
    public class BaseModel<TV> where TV : BaseDialog, new()
    {
        /// <summary>
        /// 对话框视图
        /// </summary>
        public TV view;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">View标题</param>
        protected BaseModel(string title)
        {
            view = new TV {Text = title};

            view.Closing += (sender, args) =>
            {
                const string msg = "您确定要放弃所做的变更，并关闭对话框吗？";
                args.Cancel = view.DialogResult != DialogResult.OK && !Messages.showConfirm(msg);
            };
            view.Closed += (sender, args) => view.Dispose();
        }
    }
}