using System.Collections.Generic;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.MainForm.Models
{
    public class MainModel
    {
        /// <summary>
        /// 保存当前主题样式到配置文件
        /// </summary>
        /// <param name="value">样式值</param>
        public static void saveLookAndFeel(string value)
        {
            Setting.saveLookAndFeel(value);
        }

        /// <summary>
        /// 如注销用户失败，弹出询问对话框。
        /// </summary>
        public bool logout()
        {
            const string msg = "退出应用程序将导致当前未完成的输入内容丢失！\r\n您确定要退出吗？";
            if (!Messages.showConfirm(msg)) return true;

            Setting.tokenHelper.deleteToken();

            return false;
        }
    }
}
