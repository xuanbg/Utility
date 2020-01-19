using Insight.Utils.BaseViewModels;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.ViewModels
{
    public class ChangPwModel : BaseDialogModel<object, ChangePw>
    {
        private string oldPw;
        private string sing;
        private string newPw;
        private string confirmPw;

        /// <summary>
        /// 构造函数
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        /// <param name="title">窗体标题</param>
        public ChangPwModel(string title) : base(title)
        {
            // 订阅控件事件实现数据双向绑定
            view.Password.EditValueChanged += (sender, args) => sing = Util.hash(Setting.tokenHelper.account + Util.hash(view.Password.Text));
            view.NewPw.EditValueChanged += (sender, args) => newPw = view.NewPw.Text;
            view.ConfirmPw.EditValueChanged += (sender, args) => confirmPw = view.ConfirmPw.Text;
        }

        /// <summary>
        /// 初始化对话框
        /// </summary>
        /// <param name="old">旧密码</param>
        public void init(string old = null)
        {
            oldPw = old;
            view.Password.EditValue = old;
            view.Password.Enabled = old == null;

            view.NewPw.EditValue = null;
            view.ConfirmPw.EditValue = null;
            view.Refresh();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns>bool 是否修改成功</returns>
        public bool save()
        {
            if (sing != Setting.tokenHelper.sign)
            {
                Messages.showError("请输入正确的原密码，否则无法为您更换密码！");
                view.Password.EditValue = null;
                view.Password.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(newPw))
            {
                Messages.showWarning("新密码不能为空，请输入您的新密码并牢记！");
                view.NewPw.Focus();
                return false;
            }

            if (newPw == "123456")
            {
                Messages.showWarning("新密码不能设为初始密码，请输入其它密码并牢记！");
                view.NewPw.EditValue = null;
                view.ConfirmPw.EditValue = null;
                view.NewPw.Focus();
                return false;
            }

            if (newPw != confirmPw)
            {
                Messages.showWarning("两次密码输入不一致！\r\n请重新确认密码，只有两次输入的密码一致，才能为您更换密码。");
                view.ConfirmPw.EditValue = null;
                view.ConfirmPw.Focus();
                return false;
            }

            if (!Model.changPassword(oldPw, newPw)) return false;

            Setting.tokenHelper.signature(newPw);
            Messages.showMessage("更换密码成功！请牢记新密码并使用新密码登录系统。");
            return true;
        }
    }
}