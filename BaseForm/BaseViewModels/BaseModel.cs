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
        protected BaseModel()
        {
            view = new TV();

            view.Closing += (sender, args) =>
            {
                const string msg = "您确定要放弃所做的变更，并关闭对话框吗？";
                if (view.DialogResult == DialogResult.OK || Messages.showConfirm(msg)) view.Close();
                else args.Cancel = true;
            };
            view.Closed += (sender, args) => view.Dispose();
        }
    }
}