using System.Linq;
using System.Windows.Forms;
using Insight.Base.BaseForm.Forms;
using Insight.Base.BaseForm.Utils;

namespace Insight.Base.BaseForm.ViewModels
{
    public class BaseWizardModel<T, TV> : BaseModel<T, TV> where TV : BaseWizard, new()
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="item">对话框数据对象</param>
        protected BaseWizardModel(string title, T item = default(T)) : base(title)
        {
            this.item = item;
        }

        /// <summary>
        /// 显示对话框
        /// </summary>
        public void showDialog()
        {
            view.ShowDialog();
        }

        /// <summary>
        /// 确认操作
        /// </summary>
        public void confirm()
        {
            view.DialogResult = DialogResult.OK;
            callback("confirm", new object[] { item });
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        protected void closeDialog()
        {
            view.DialogResult = DialogResult.OK;
            view.Close();
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
    }
}
