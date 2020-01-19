using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Controls;
using Insight.Utils.Entity;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.ViewModels
{
    public class LoginModel
    {
        public LoginDialog view;
        private static readonly TokenHelper TokenHelper = Setting.tokenHelper;
        private string account = Setting.getAccount();
        private string password;
        private readonly bool showDept = Convert.ToBoolean(Util.getAppSetting("ShowDept"));
        private List<TreeLookUpMember> depts = new List<TreeLookUpMember>();

        /// <summary>
        /// 构造函数，初始化视图
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        public LoginModel()
        {
            view = new LoginDialog
            {
                Text = Setting.appName,
                Icon = new Icon("logo.ico"),
                BackgroundImage = Util.getImage("bg.png"),
                BackgroundImageLayout = ImageLayout.Stretch
            };

            // 订阅控件事件实现数据双向绑定
            view.UserNameInput.EditValueChanged += (sender, args) => account = view.UserNameInput.Text.Trim();
            view.PassWordInput.EditValueChanged += (sender, args) => password = view.PassWordInput.Text;
            view.peeDept.Visible = showDept;
            view.lueDept.Visible = showDept;
            if (showDept)
            {
                view.UserNameInput.Leave += (sender, args) => getDepts();
                view.lueDept.EditValueChanged += (sender, args) => deptChanged();

                Format.initTreeListLookUpEdit(view.lueDept, depts, NodeIconType.ORGANIZATION);
            }
        }

        /// <summary>
        /// 初始化默认登录用户
        /// </summary>
        public void initUserName()
        {
            if (string.IsNullOrEmpty(account)) return;

            view.UserNameInput.EditValue = account;
            if (string.IsNullOrEmpty(account)) return;

            view.PassWordInput.Focus();
        }

        /// <summary>
        /// 验证用户输入，通过验证后获取用户AccessToken
        /// </summary>
        /// <returns>bool 是否登录成功</returns>
        public bool login()
        {
            if (string.IsNullOrEmpty(account))
            {
                Messages.showMessage("请输入用户名！");
                view.UserNameInput.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                Messages.showWarning("密码不能为空！");
                view.PassWordInput.Focus();
                return false;
            }

            if (showDept && string.IsNullOrEmpty(Setting.tokenHelper.tenantId))
            {
                Messages.showWarning("请选择登录的企业/部门！");
                view.lueDept.Focus();
                return false;
            }

            TokenHelper.account = account;
            TokenHelper.signature(password);
            if (!TokenHelper.getTokens()) return false;

            Setting.needChangePw = password == "123456";
            Setting.saveUserName(account);

            return true;
        }

        /// <summary>
        /// 获取可登录部门
        /// </summary>
        public void getDepts()
        {
            if (string.IsNullOrEmpty(account)) return;

            depts = Model.getDepts(account);
            if (!depts.Any()) return;

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

            if (depts.Count(i => i.nodeType == 2) > 1) return;

            var id = depts.Single(i => i.nodeType == 2).id;
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
                TokenHelper.tenantId = null;
                TokenHelper.deptId = null;

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
                TokenHelper.tenantId = id;
                TokenHelper.deptId = null;
            }
            else
            {
                TokenHelper.tenantId = dept.remark;
                TokenHelper.deptId = id;
                Setting.deptCode = dept.code;
            }

            Setting.deptName = dept.name;
        }
    }
}
