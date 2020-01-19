using System.Linq;
using System.Windows.Forms;
using Insight.Utils.BaseForms;
using Insight.Utils.Common;

namespace Insight.Utils.BaseViewModels
{
    public class BaseDialogModel<T, TV> : BaseModel<TV> where TV : BaseDialog, new()
    {
        /// <summary>
        /// 对话框数据实体
        /// </summary>
        protected T item;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="item">对话框数据对象</param>
        /// <param name="isShow">是否查看模式</param>
        protected BaseDialogModel(string title, T item = default(T), bool isShow = false) : base(title)
        {
            this.item = item;
            view.Confirm.Visible = !isShow;
            view.Cancel.Visible = !isShow;
            view.Close.Visible = isShow;

            if (isShow)
            {
                view.Close.Click += (sender, args) =>
                {
                    view.DialogResult = DialogResult.OK;
                    view.Close();
                };
            }
            else
            {
                view.Confirm.Click += (sender, args) => buttonClick("confirm");
                view.Cancel.Click += (sender, args) => view.Close();
            }
        }

        /// <summary>
        /// 显示对话框
        /// </summary>
        public void showDialog()
        {
            view.ShowDialog();
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