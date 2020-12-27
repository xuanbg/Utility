﻿using System.Linq;
using System.Windows.Forms;
using Insight.Base.BaseForm.Forms;
using Insight.Base.BaseForm.Utils;

namespace Insight.Base.BaseForm.ViewModels
{
    public class BaseDialogModel<T, TV> : BaseModel<T, TV> where TV : BaseDialog, new()
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="item">对话框数据对象</param>
        /// <param name="isShow">是否查看模式</param>
        protected BaseDialogModel(string title, T item = default(T), bool isShow = false) : base(title)
        {
            this.item = item;
            view.confirm.Visible = !isShow;
            view.cancel.Visible = !isShow;
            view.close.Visible = isShow;
            view.Closed += (sender, args) => view.Dispose();

            if (isShow)
            {
                view.close.Select();
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

            view.confirm.Enabled = false;
            view.Refresh();

            callback("confirm", new object[] {item});
        }

        /// <summary>
        /// 启用确认按钮
        /// </summary>
        public void enableConfirm()
        {
            view.confirm.Enabled = true;
            view.Refresh();
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