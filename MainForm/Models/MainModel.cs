using System.Collections.Generic;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;
using Insight.Utils.Models;

namespace Insight.Utils.MainForm.Models
{
    public class MainModel : BaseModel
    {
        /// <summary>
        /// 获取导航数据
        /// </summary>
        /// <returns>导航数据集合</returns>
        public List<Navigation> getNavigators()
        {
            var url = $"{Setting.gateway}/base/auth/v1.0/navigators";
            var client = new HttpClient<List<Navigation>>(Setting.tokenHelper);
            return client.getData(url);
        }

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

            tokenHelper.deleteToken();

            return false;
        }
    }
}
