using Insight.Utils.BaseViewModels;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.MainForm.Dtos;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.ViewModels
{
    public class ChangPwModel : BaseDialogModel<string, ChangePw>
    {
        private string sing;

        /// <summary>
        /// 构造函数
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        /// <param name="title">窗体标题</param>
        /// <param name="password">对话框数据对象</param>
        public ChangPwModel(string title, string password) : base(title, password)
        {
            view.Password.EditValue = password;
            view.Password.Enabled = password == null;

            // 订阅控件事件实现数据双向绑定
            view.NewPw.EditValueChanged += (sender, args) => item = view.NewPw.Text;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public void confirm()
        {
            if (!inputExamine()) return;

            sing = Util.hash(Setting.tokenHelper.account + Util.hash(view.Password.Text));
            if (sing != Setting.tokenHelper.sign)
            {
                Messages.showError("请输入正确的原密码，否则无法为您更换密码！");
                view.Password.EditValue = null;
                view.Password.Focus();
                return;
            }

            if (item == "123456")
            {
                Messages.showWarning("新密码不能设为初始密码，请输入其它密码并牢记！");
                view.NewPw.EditValue = null;
                view.ConfirmPw.EditValue = null;
                view.NewPw.Focus();
                return;
            }

            if (item != view.ConfirmPw.Text)
            {
                Messages.showWarning("请重新确认密码，只有两次输入的密码一致，才能为您更换密码。");
                view.ConfirmPw.EditValue = null;
                view.ConfirmPw.Focus();
                return;
            }

            var data = new PasswordDto {old = Util.hash(view.Password.Text), password = Util.hash(item)};
            callback(null, new object[]{data});
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        public void close()
        {
            Setting.tokenHelper.signature(item);
            Messages.showMessage("更换密码成功！请牢记新密码并使用新密码登录系统。");

            closeDialog();
        }
    }
}