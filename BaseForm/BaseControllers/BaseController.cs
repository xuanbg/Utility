using System.ComponentModel;
using System.Windows.Forms;
using Insight.Utils.BaseForm;
using Insight.Utils.Common;

namespace Insight.Utils.BaseControllers
{
    public class BaseController
    {
        /// <summary>
        /// 订阅对话框关闭事件
        /// </summary>
        /// <param name="dialog">对话框视图</param>
        protected void subCloseEvent(BaseDialog dialog)
        {
            dialog.Cancel.Click += (sender, args) => dialog.Close();
            dialog.Closing += (sender, args) => dialogClosing(dialog, args);
            dialog.Closed += (sender, args) => dialog.Dispose();
        }

        /// <summary>
        /// 订阅对话框关闭事件
        /// </summary>
        /// <param name="dialog">对话框视图</param>
        /// <param name="confirm">是否订阅确定按钮默认事件</param>
        protected void subCloseEvent(BaseDialog dialog, bool confirm)
        {
            subCloseEvent(dialog);
            if (!confirm) return;

            dialog.Confirm.Click += (sender, args) => closeDialog(dialog);
        }

        /// <summary>
        /// 订阅向导关闭事件
        /// </summary>
        /// <param name="wizard">向导视图</param>
        protected void subCloseEvent(BaseWizard wizard)
        {
            wizard.FormClosing += (sender, args) => wizardClosing(wizard, args);
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        /// <param name="dialog">对话框视图</param>
        protected void closeDialog(BaseDialog dialog)
        {
            dialog.Confirm.Click -= (sender, args) => closeDialog(dialog);
            dialog.Cancel.Click -= (sender, args) => dialog.Close();
            dialog.Closing -= (sender, args) => dialogClosing(dialog, args);
            dialog.Closed -= (sender, args) => dialog.Dispose();

            dialog.DialogResult = DialogResult.OK;
            dialog.Close();
        }

        /// <summary>
        /// 对话框关闭时弹出确认信息
        /// </summary>
        /// <param name="dialog">对话框视图</param>
        /// <param name="e">对话框视图关闭事件</param>
        private void dialogClosing(BaseDialog dialog, CancelEventArgs e)
        {
            if (dialog.DialogResult == DialogResult.OK) return;

            const string msg = "您确定要放弃所做的变更，并关闭对话框吗？";
            if (!Messages.showConfirm(msg)) e.Cancel = true;

            dialog.Confirm.Click -= (sender, args) => closeDialog(dialog);
            dialog.Cancel.Click -= (sender, args) => dialog.Close();
            dialog.Closing -= (sender, args) => dialogClosing(dialog, args);
            dialog.Closed -= (sender, args) => dialog.Dispose();

            dialog.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 关闭向导时弹出确认对话框
        /// </summary>
        /// <param name="view">向导视图</param>
        /// <param name="e">向导视图关闭事件</param>
        private static void wizardClosing(Form view, CancelEventArgs e)
        {
            if (view.DialogResult == DialogResult.OK) return;

            const string msg = "您确定要放弃所做的变更，离开向导吗？";
            if (!Messages.showConfirm(msg)) e.Cancel = true;

            view.FormClosing -= (sender, args) => wizardClosing(view, args);
            view.DialogResult = DialogResult.Cancel;
        }
    }
}
