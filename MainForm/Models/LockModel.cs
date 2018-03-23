using Insight.Utils.Common;
using Insight.Utils.MainForm.Views;
using Insight.Utils.Models;

namespace Insight.Utils.MainForm.Models
{
    public class LockModel : BaseModel
    {
        public Locked view = new Locked();

        private string sing;

        /// <summary>
        /// 构造函数
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        public LockModel()
        {
            view.Password.EditValueChanged += (sender, args) => sing = Util.Hash(tokenHelper.account + Util.Hash(view.Password.Text));
        }

        /// <summary>
        /// 初始化对话框
        /// </summary>
        public void Init()
        {
            view.Password.EditValue = null;
            view.Refresh();
        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <returns>bool 是否解锁成功</returns>
        public bool Unlock()
        {
            if (sing == tokenHelper.sign) return true;

            Messages.ShowError("请输入正确的密码，否则无法为您解除锁定！");
            view.Password.Text = string.Empty;
            view.Password.Focus();
            return false;
        }
    }
}