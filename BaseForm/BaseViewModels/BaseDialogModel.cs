using System.Linq;
using System.Windows.Forms;
using Insight.Utils.BaseForms;
using Insight.Utils.Common;

namespace Insight.Utils.BaseViewModels
{
    public class BaseDialogModel<T, TV> : BaseModel<T, TV> where TV : BaseDialog, new()
    {
        private readonly bool isShow;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="item">对话框数据对象</param>
        /// <param name="isShow">是否查看模式</param>
        protected BaseDialogModel(string title, T item = default(T), bool isShow = false) : base(title)
        {
            this.item = item;
            this.isShow = isShow;
            view.confirm.Visible = !isShow;
            view.cancel.Visible = !isShow;
            view.close.Visible = isShow;
            view.Closed += (sender, args) => view.Dispose();

            if (isShow)
            {
                view.close.Click += (sender, args) =>
                {
                    view.DialogResult = DialogResult.OK;
                    view.Close();
                };
            }
            else
            {
                view.confirm.Click += (sender, args) =>call("confirm");
                view.cancel.Click += (sender, args) => view.Close();
                view.Closing += (sender, args) => closeConfirm(args);
            }
        }

        /// <summary>
        /// 显示对话框
        /// </summary>
        public void showDialog()
        {
            if(isShow) view.close.Select();

            view.ShowDialog();
        }

        /// <summary>
        /// 确认操作
        /// </summary>
        public void confirm()
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
                return;
            }

            callback("confirm", new object[] {item});
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        public void closeDialog()
        {
            view.DialogResult = DialogResult.OK;
            view.Close();
        }
    }
}