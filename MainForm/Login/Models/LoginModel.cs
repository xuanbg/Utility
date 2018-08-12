using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Controls;
using Insight.Utils.Entity;
using Insight.Utils.Models;

namespace Insight.Utils.MainForm.Login.Models
{
    public class LoginModel : BaseModel
    {
        public Views.Login view;

        private string account = Setting.GetAccount();
        private string password;
        private readonly List<TreeLookUpMember> depts = new List<TreeLookUpMember>();

        /// <summary>
        /// 构造函数，初始化视图
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        public LoginModel()
        {
            view = new Views.Login
            {
                Text = Setting.appName,
                Icon = new Icon("logo.ico"),
                BackgroundImage = Util.GetImage("bg.png"),
                BackgroundImageLayout = ImageLayout.Stretch
            };

            // 订阅控件事件实现数据双向绑定
            view.UserNameInput.EditValueChanged += (sender, args) => account = view.UserNameInput.Text.Trim();
            view.PassWordInput.EditValueChanged += (sender, args) => password = view.PassWordInput.Text;
            view.UserNameInput.Leave += (sender, args) => GetDepts();
            view.lueDept.EditValueChanged += (sender, args) => DeptChanged();

            Format.InitTreeListLookUpEdit(view.lueDept, depts, NodeIconType.Organization);
        }

        /// <summary>
        /// 初始化默认登录用户
        /// </summary>
        public void InitUserName()
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
        public bool Login()
        {
            if (string.IsNullOrEmpty(account))
            {
                Messages.ShowMessage("请输入用户名！");
                view.UserNameInput.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                Messages.ShowWarning("密码不能为空！");
                view.PassWordInput.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(Setting.tokenHelper.tenantId))
            {
                Messages.ShowWarning("请选择登录的企业/部门！");
                view.lueDept.Focus();
                return false;
            }

            tokenHelper.account = account;
            tokenHelper.Signature(password);
            if (tokenHelper.token == null) return false;

            Setting.needChangePw = password == "123456";
            Setting.SaveUserName(account);
            GetUserInfo();

            return true;
        }

        /// <summary>
        /// 获取可登录部门
        /// </summary>
        public void GetDepts()
        {
            if (string.IsNullOrEmpty(account)) return;

            var url = $"{baseServer}/userapi/v1.0/users/{account}/depts";
            var request = new HttpRequest();
            if (!request.Send(url))
            {
                Messages.ShowError(request.message);
                return;
            }

            var result = Util.Deserialize<Result<List<TreeLookUpMember>>>(request.data);
            if (!result.successful)
            {
                Messages.ShowError(result.message);
                return;
            }

            depts.Clear();
            depts.AddRange(result.data);
            view.lueDept.Properties.TreeList.RefreshDataSource();
            if (depts.Count != 1) return;

            view.lueDept.Properties.TreeList.MoveFirst();
            view.lueDept.EditValue = depts[0].id;
        }

        /// <summary>
        /// 登录部门变化后更新相关信息
        /// </summary>
        private void DeptChanged()
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
                Messages.ShowMessage("请选择部门");
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
            }

            Setting.deptName = view.lueDept.Text;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        private void GetUserInfo()
        {
            var url = $"{baseServer}/userapi/v1.0/users/myself";
            var client = new HttpClient<UserInfo>(tokenHelper);
            if (!client.Get(url)) return;

            Setting.userId = client.data.id;
            Setting.userName = client.data.name;
        }
    }
}
