using Insight.Base.BaseForm.Entities;
using Insight.Base.BaseForm.Utils;
using Insight.Base.BaseForm.ViewModels;
using Insight.Base.MainForm.Views;
using Insight.Utils.Common;

namespace Insight.Base.MainForm.ViewModels
{
    public class ChangPwModel : BaseDialogModel<PasswordDto, PasswordDialog>
    {
        private string sing;

        /// <summary>
        /// 构造函数
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        /// <param name="password">初始化数据</param>
        /// <param name="title">View标题</param>
        public ChangPwModel(string password, string title) : base(title)
        {
            item = new PasswordDto {old = Util.hash(password)};

            view.txtOld.EditValue = password;
            view.txtOld.Enabled = password == null;

            view.txtOld.EditValueChanged += (sender, args) => item.old = Util.hash(view.txtOld.Text);
            view.txtPassword.EditValueChanged += (sender, args) => item.password = Util.hash(view.txtPassword.Text);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public new void confirm()
        {
            sing = Util.hash(Setting.tokenHelper.account + item.old);
            if (sing != Setting.tokenHelper.sign)
            {
                Messages.showError("请输入正确的原密码，否则无法为您更换密码！");
                view.txtOld.EditValue = null;
                view.txtOld.Focus();
                return;
            }

            if (view.txtPassword.Text == "123456")
            {
                Messages.showWarning("新密码不能设为初始密码，请输入其它密码并牢记！");
                view.txtPassword.EditValue = null;
                view.txtConfirm.EditValue = null;
                view.txtPassword.Focus();
                return;
            }

            if (view.txtPassword.Text != view.txtConfirm.Text)
            {
                Messages.showWarning("请重新确认密码，只有两次输入的密码一致，才能为您更换密码。");
                view.txtConfirm.EditValue = null;
                view.txtConfirm.Focus();
                return;
            }
            
            base.confirm();
        }
    }
}