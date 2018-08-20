using System.Windows.Forms;

namespace Insight.Utils.Common
{
    public class Messages
    {
        /// <summary>
        /// 显示提示对话框
        /// </summary>
        /// <param name="msg">消息内容</param>
        public static void showMessage(string msg)
        {
            MessageBox.Show(msg, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示警告对话框
        /// </summary>
        /// <param name="msg">消息内容</param>
        public static void showWarning(string msg)
        {
            MessageBox.Show(msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示错误对话框
        /// </summary>
        /// <param name="msg">消息内容</param>
        public static void showError(string msg)
        {
            MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示确认对话框
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="button">默认按钮</param>
        /// <returns>bool 是否确认</returns>
        public static bool showConfirm(string msg, MessageBoxDefaultButton button = MessageBoxDefaultButton.Button2)
        {
            return MessageBox.Show(msg, "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, button) == DialogResult.OK;
        }

        /// <summary>
        /// 显示提问对话框
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns>DialogResult</returns>
        public static DialogResult showQuestion(string msg)
        {
            return MessageBox.Show(msg, "请选择", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
        }

    }
}
