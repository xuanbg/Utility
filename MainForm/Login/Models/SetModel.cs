using Insight.Utils.Client;
using Insight.Utils.MainForm.Login.Views;
using Insight.Utils.Models;

namespace Insight.Utils.MainForm.Login.Models
{
    public class SetModel : BaseModel
    {
        public LoginSet view;

        private bool saveUser = Setting.isSaveUserInfo();

        /// <summary>
        /// 构造函数，初始化视图
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        public SetModel()
        {
            view = new LoginSet
            {
                BaseInupt = {Text = baseServer },
                SaveUserCheckBox = {Checked = saveUser}
            };

            // 订阅控件事件实现数据双向绑定
            view.BaseInupt.EditValueChanged += (sender, args) => baseServer = view.BaseInupt.Text;
            view.SaveUserCheckBox.CheckStateChanged += (sender, args) => saveUser = view.SaveUserCheckBox.Checked;
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        public void save()
        {
            if (!saveUser) Setting.saveUserName(string.Empty);

            Setting.saveIsSaveUserInfo(saveUser);
            Setting.saveBaseServer();
        }
    }
}