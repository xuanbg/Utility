using Insight.Base.BaseForm.BaseViewModels;
using Insight.Base.BaseForm.Utils;
using Insight.Base.MainForm.Views;

namespace Insight.Base.MainForm.ViewModels
{
    public class SetModel : BaseDialogModel<object, LoginSetDialog>
    {
        private bool saveUser = Setting.isSaveUserInfo();

        /// <summary>
        /// 构造函数，初始化视图
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        /// <param name="title">窗体标题</param>
        public SetModel(string title) : base(title)
        {
            view.BaseInupt.EditValue = Setting.gateway;
            view.SaveUserCheckBox.Checked = saveUser;

            // 订阅控件事件实现数据双向绑定
            view.BaseInupt.EditValueChanged += (sender, args) => Setting.gateway = view.BaseInupt.Text;
            view.SaveUserCheckBox.CheckStateChanged += (sender, args) => saveUser = view.SaveUserCheckBox.Checked;
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        public new void confirm()
        {
            if (!saveUser) Setting.saveUserName(string.Empty);

            Setting.saveIsSaveUserInfo(saveUser);
            Setting.saveGateway();

            closeDialog();
        }
    }
}