using Insight.Utils.BaseViewModels;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.ViewModels
{
    public class LockModel : BaseDialogModel<string, Locked>
    {
        /// <summary>
        /// 构造函数
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        /// <param name="title">窗体标题</param>
        public LockModel(string title) : base(title, null, true)
        {
            view.Password.EditValueChanged += (sender, args) => item = Util.hash(Setting.tokenHelper.account + Util.hash(view.Password.Text));
        }

        /// <summary>
        /// 初始化对话框
        /// </summary>
        public void init()
        {
            view.Password.EditValue = null;
            view.Refresh();
        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <returns>bool 是否解锁成功</returns>
        public bool unlock()
        {
            if (item == Setting.tokenHelper.sign) return true;

            Messages.showError("请输入正确的密码，否则无法为您解除锁定！");
            view.Password.Text = string.Empty;
            view.Password.Focus();
            return false;
        }
    }
}