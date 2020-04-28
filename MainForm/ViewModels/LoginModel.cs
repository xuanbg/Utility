using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Insight.Base.BaseForm.Controls;
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
        private readonly bool showDept = Convert.ToBoolean(Util.getAppSetting("ShowDept"));
        private List<TreeLookUpMember> depts = new List<TreeLookUpMember>();
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
            view.peeDept.Visible = showDept;
            view.lueDept.Visible = showDept;

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
            if (showDept)
            {
                view.txtPassWord.Enter += (sender, args) => callback("loadDept", new object[]{account});
                view.lueDept.EditValueChanged += (sender, args) => deptChanged();

                Format.initTreeListLookUpEdit(view.lueDept, depts);
            }
        }

        /// <summary>
        /// 打开登录对话框
        /// </summary>
        public void showDialog()
        {
            view.Show();
            view.panMain.Visible = true;
            view.Refresh();
            if (!string.IsNullOrEmpty(account)) view.txtPassWord.Focus();
        }

        /// <summary>
        /// 验证用户输入，通过验证后获取用户AccessToken
        /// </summary>
        public void login()
        {
            if (string.IsNullOrEmpty(account))
            {
                Messages.showMessage("请输入用户名！");
                view.txtAccount.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                Messages.showWarning("密码不能为空！");
                view.txtPassWord.Focus();
                return;
            }

            if (showDept && string.IsNullOrEmpty(Setting.tokenHelper.tenantId))
            {
                Messages.showWarning("请选择登录的企业/部门！");
                view.lueDept.Focus();
                return;
            }

            tokenHelper.account = account;
            tokenHelper.signature(Util.hash(password));
            if (!tokenHelper.getTokens())
            {
                view.txtPassWord.EditValue = null;
                return;
            }

            Setting.needChangePw = password == "123456";
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
        /// 初始化可登录部门
        /// </summary>
        /// <param name="list">可登录部门</param>
        public void initDepts(List<TreeLookUpMember> list)
        {
            depts = list;
            var tree = view.lueDept.Properties.TreeList;
            depts.Clear();
            depts.AddRange(depts);
            tree.RefreshDataSource();
            if (depts.Count == 1)
            {
                tree.MoveFirst();
                view.lueDept.EditValue = depts[0].id;

                return;
            }

            if (depts.Count(i => i.type == 1) > 1) return;

            var id = depts.Single(i => i.type == 1).id;
            var node = tree.FindNodeByKeyID(id);
            view.lueDept.Properties.TreeList.FocusedNode = node;
            view.lueDept.EditValue = id;
        }

        /// <summary>
        /// 登录部门变化后更新相关信息
        /// </summary>
        private void deptChanged()
        {
            var id = view.lueDept.EditValue?.ToString();
            if (string.IsNullOrEmpty(id))
            {
                tokenHelper.tenantId = null;
                tokenHelper.deptId = null;

                return;
            }

            var node = view.lueDept.Properties.TreeList.FocusedNode;
            if (node?.HasChildren ?? false)
            {
                Messages.showMessage("请选择部门");
                view.lueDept.EditValue = null;

                return;
            }

            var dept = depts.Single(i => i.id == id);
            if (dept.parentId == null)
            {
                tokenHelper.tenantId = id;
                tokenHelper.deptId = null;
            }
            else
            {
                tokenHelper.tenantId = dept.remark;
                tokenHelper.deptId = id;
                Setting.deptCode = dept.code;
            }

            Setting.deptName = dept.name;
        }
    }
}
