using Insight.Utils.BaseViewModels;
using Insight.Utils.Client;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.ViewModels
{
    public class SetModel : BaseDialogModel<object, LoginSet>
    {
        private bool saveUser = Setting.isSaveUserInfo();

        /// <summary>
        /// 构造函数，初始化视图
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        public SetModel()
        {
            view = new LoginSet
            {
                BaseInupt = {Text = Setting.gateway },
                SaveUserCheckBox = {Checked = saveUser}
            };

            // 订阅控件事件实现数据双向绑定
            view.BaseInupt.EditValueChanged += (sender, args) => Setting.gateway = view.BaseInupt.Text;
            view.SaveUserCheckBox.CheckStateChanged += (sender, args) => saveUser = view.SaveUserCheckBox.Checked;
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        public void save()
        {
            if (!saveUser) Setting.saveUserName(string.Empty);

            Setting.saveIsSaveUserInfo(saveUser);
            Setting.saveGateway();
        }
    }
}