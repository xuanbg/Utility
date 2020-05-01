using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Insight.Base.BaseForm.Entities;
using Insight.Base.BaseForm.Utils;
using Insight.Base.BaseForm.ViewModels;
using Insight.Base.MainForm.Views;
using Insight.Utils.Common;

namespace Insight.Base.MainForm.ViewModels
{
    public class LoginModel : BaseModel<object, LoginDialog>
    {
        private readonly TokenHelper tokenHelper = Setting.tokenHelper;
        private readonly bool showTenant = Convert.ToBoolean(Util.getAppSetting("ShowTenant"));
        private string account = Setting.getAccount();
        public string password;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">窗体标题</param>
        public LoginModel(string title) : base(title)
        {
            view.Icon = new Icon("logo.ico");
            view.BackgroundImage = Util.getImageFromFile("bg.png");
            view.BackgroundImageLayout = ImageLayout.Stretch;
            view.txtAccount.EditValue = account;
            view.peeDept.Visible = showTenant;
            view.lueTenant.Visible = showTenant;

            view.sbeSet.Click += (sender, args) => callback("setServerIp");
            view.sbeCacel.Click += (sender, args) => callback("exit");
            view.sbeLogin.Click += (sender, args) => login();

            // 订阅控件事件实现数据双向绑定
            view.txtAccount.EditValueChanged += (sender, args) => account = view.txtAccount.Text.Trim();
            view.txtPassWord.EditValueChanged += (sender, args) => password = view.txtPassWord.Text;
            view.txtPassWord.KeyPress += (sender, args) =>
            {
                if (args.KeyChar != 13) return;

                login();
            };
            if (!showTenant) return;

            view.txtPassWord.Enter += (sender, args) => callback("loadTenants", new object[] {account});
            view.lueTenant.EditValueChanged += (sender, args) =>
            {
                tokenHelper.tenantId = view.lueTenant.EditValue.ToString();
                Setting.tenantName = view.lueTenant.Text;
            };
        }

        /// <summary>
        /// 打开登录对话框
        /// </summary>
        public void showDialog()
        {
            view.Show();
            view.panMain.Visible = true;
            view.Refresh();
            if (!string.IsNullOrEmpty(account)) view.txtPassWord.Select();
        }

        /// <summary>
        /// 验证用户输入，通过验证后获取用户AccessToken
        /// </summary>
        public void login()
        {
            if (string.IsNullOrEmpty(account))
            {
                Messages.showMessage("请输入用户名！");
                view.txtAccount.Select();

                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                Messages.showWarning("密码不能为空！");
                view.txtPassWord.Select();

                return;
            }

            if (showTenant && string.IsNullOrEmpty(Setting.tokenHelper.tenantId))
            {
                Messages.showWarning("请选择登录的企业！");
                view.lueTenant.Select();

                return;
            }

            tokenHelper.account = account;
            tokenHelper.signature(Util.hash(password));
            if (!tokenHelper.getTokens())
            {
                view.txtPassWord.EditValue = null;
                return;
            }

            Setting.saveUserName(account);
            callback("loadMainWindow");
        }

        /// <summary>
        /// 隐藏登录界面
        /// </summary>
        public void hide()
        {
            view.labLoading.Visible = true;
            view.panMain.Visible = false;
            view.Refresh();
        }

        /// <summary>
        /// 初始化可登录租户
        /// </summary>
        /// <param name="list">可登录部门</param>
        public void initTenants(List<LookUpMember> list)
        {
            if (list.Any())
            {
                Format.initLookUpEdit(view.lueTenant, list);
                if (list.Count == 1) view.lueTenant.EditValue = list[0].id;

                return;
            }

            Messages.showError("该用户无法登录此系统，请更换用户");
            view.lueTenant.Properties.DataSource = null;
            view.txtAccount.EditValue = null;
            view.txtAccount.Select();
        }
    }
}
