using System.Linq;
using Insight.Utils.BaseForm;
using Insight.Utils.Common;

namespace Insight.Utils.BaseViewModels
{
    public class BaseDialogModel<T, TV> : BaseModel<TV> where TV : BaseDialog, new()
    {
        /// <summary>
        /// 对话框数据实体
        /// </summary>
        protected readonly T item;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="item">对话框数据对象</param>
        /// <param name="isShow">是否查看模式</param>
        protected BaseDialogModel(T item = default(T), bool isShow = false)
        {
            this.item = item;
            view.Confirm.Visible = !isShow;
            view.Cancel.Visible = !isShow;
            view.Close.Visible = isShow;

            if (isShow)
            {
                view.Close.Click += (sender, args) => view.Close();
            }
            else
            {
                view.Confirm.Click += (sender, args) => buttonClick("confirm");
                view.Cancel.Click += (sender, args) => view.Close();
            }
        }

        /// <summary>
        /// 检查输入检查对象是否都有值
        /// </summary>
        /// <returns>bool 对象是否都有值</returns>
        protected bool inputExamine()
        {
            var propertys = typeof(T).GetProperties();
            foreach (var property in propertys)
            {
                if (!property.CanRead) continue;

                var attributes = property.GetCustomAttributes(typeof(InputCheck), false);
                if (!(attributes.FirstOrDefault() is InputCheck att)) continue;

                var val = property.GetValue(item);
                if (val != null)
                {
                    if (!att.notEmpty) continue;

                    if (!string.IsNullOrEmpty(val as string)) continue;
                }

                Messages.showError(att.message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 按钮点击事件路由
        /// </summary>
        /// <param name="action">按钮名称</param>
        protected void buttonClick(string action)
        {
            var method = GetType().GetMethod(action);
            if (method == null) Messages.showError("对不起，该功能尚未实现！");
            else method.Invoke(this, null);
        }
    }
}