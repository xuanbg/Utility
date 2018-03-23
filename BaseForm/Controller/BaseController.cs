using System.ComponentModel;
using System.Windows.Forms;
using Insight.Utils.BaseForm;
using Insight.Utils.Common;

// ReSharper disable EventUnsubscriptionViaAnonymousDelegate
namespace Insight.Utils.Controller
{
    public class BaseController<T>
    {
        protected T manage;

        /// <summary>
        /// 订阅对话框关闭事件
        /// </summary>
        /// <param name="view">对话框视图</param>
        protected void SubCloseEvent(BaseDialog view)
        {
            view.Cancel.Click += (sender, args) => view.Close();
            view.Closing += (sender, args) => DialogClosing(view, args);
            view.Closed += (sender, args) => view.Dispose();
        }

        /// <summary>
        /// 订阅对话框关闭事件
        /// </summary>
        /// <param name="view">对话框视图</param>
        /// <param name="confirm">是否订阅确定按钮默认事件</param>
        protected void SubCloseEvent(BaseDialog view, bool confirm)
        {
            SubCloseEvent(view);
            if (!confirm) return;

            view.Confirm.Click += (sender, args) => CloseDialog(view);
        }

        /// <summary>
        /// 订阅向导关闭事件
        /// </summary>
        /// <param name="view">向导视图</param>
        protected void SubCloseEvent(BaseWizard view)
        {
            view.FormClosing += (sender, args) => WizardClosing(view, args);
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        /// <param name="view">对话框视图</param>
        protected void CloseDialog(BaseDialog view)
        {
            view.Confirm.Click -= (sender, args) => CloseDialog(view);
            view.Cancel.Click -= (sender, args) => view.Close();
            view.Closing -= (sender, args) => DialogClosing(view, args);
            view.Closed -= (sender, args) => view.Dispose();

            view.DialogResult = DialogResult.OK;
            view.Close();
        }

        /// <summary>
        /// 对话框关闭时弹出确认信息
        /// </summary>
        /// <param name="view">对话框视图</param>
        /// <param name="e">对话框视图关闭事件</param>
        private void DialogClosing(BaseDialog view, CancelEventArgs e)
        {
            if (view.DialogResult == DialogResult.OK) return;

            const string msg = "您确定要放弃所做的变更，并关闭对话框吗？";
            if (!Messages.ShowConfirm(msg)) e.Cancel = true;

            view.Confirm.Click -= (sender, args) => CloseDialog(view);
            view.Cancel.Click -= (sender, args) => view.Close();
            view.Closing -= (sender, args) => DialogClosing(view, args);
            view.Closed -= (sender, args) => view.Dispose();

            view.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 关闭向导时弹出确认对话框
        /// </summary>
        /// <param name="view">向导视图</param>
        /// <param name="e">向导视图关闭事件</param>
        private static void WizardClosing(Form view, CancelEventArgs e)
        {
            if (view.DialogResult == DialogResult.OK) return;

            const string msg = "您确定要放弃所做的变更，离开向导吗？";
            if (!Messages.ShowConfirm(msg)) e.Cancel = true;

            view.FormClosing -= (sender, args) => WizardClosing(view, args);
            view.DialogResult = DialogResult.Cancel;
        }
    }
}