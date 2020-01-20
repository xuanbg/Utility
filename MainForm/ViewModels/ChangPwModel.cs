using Insight.Utils.BaseViewModels;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.MainForm.Dtos;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.ViewModels
{
    public class ChangPwModel : BaseDialogModel<PasswordDto, ChangePw>
    {
        private string sing;

        /// <summary>
        /// 构造函数
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        /// <param name="title">窗体标题</param>
        /// <param name="dto">对话框数据对象</param>
        public ChangPwModel(string title, PasswordDto dto) : base(title, dto)
        {
            view.Password.EditValue = item.old;
            view.Password.Enabled = item.old == null;

            // 订阅控件事件实现数据双向绑定
            view.Password.EditValueChanged += (sender, args) => { item.old = Util.hash(view.Password.Text); };
            view.NewPw.EditValueChanged += (sender, args) => { item.password = Util.hash(view.NewPw.Text); };
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public void confirm()
        {
            if (!inputExamine()) return;

            sing = Util.hash(Setting.tokenHelper.account + item.old);
            if (sing != Setting.tokenHelper.sign)
            {
                Messages.showError("请输入正确的原密码，否则无法为您更换密码！");
                view.Password.EditValue = null;
                view.Password.Focus();
                return;
            }

            if (view.NewPw.Text == "123456")
            {
                Messages.showWarning("新密码不能设为初始密码，请输入其它密码并牢记！");
                view.NewPw.EditValue = null;
                view.ConfirmPw.EditValue = null;
                view.NewPw.Focus();
                return;
            }

            if (view.NewPw.Text != view.ConfirmPw.Text)
            {
                Messages.showWarning("请重新确认密码，只有两次输入的密码一致，才能为您更换密码。");
                view.ConfirmPw.EditValue = null;
                view.ConfirmPw.Focus();
                return;
            }

            callback(null, new object[]{item});
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        public void close()
        {
            Setting.tokenHelper.signature(item.password);
            Messages.showMessage("更换密码成功！请牢记新密码并使用新密码登录系统。");

            closeDialog();
        }
    }
}