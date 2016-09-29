using System.Windows.Forms;

namespace Insight.Utils.Common
{
    public class Messages
    {
        /// <summary>
        /// 显示提示对话框
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowMessage(string msg)
        {
            MessageBox.Show(msg, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示警告对话框
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowWarning(string msg)
        {
            MessageBox.Show(msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示错误对话框
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示确认对话框
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        public static DialogResult ShowConfirm(string msg, MessageBoxDefaultButton button = MessageBoxDefaultButton.Button2)
        {
            return MessageBox.Show(msg, "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, button);
        }

        /// <summary>
        /// 显示提问对话框
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DialogResult ShowQuestion(string msg)
        {
            return MessageBox.Show(msg, "请选择", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
        }

    }
}
