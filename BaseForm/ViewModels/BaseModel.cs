using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Insight.Base.BaseForm.Entities;
using Insight.Base.BaseForm.Utils;

namespace Insight.Base.BaseForm.ViewModels
{
    public class BaseModel<T, TV> where TV : XtraForm, new()
    {
        private GridHitInfo hitInfo = new GridHitInfo();

        /// <summary>
        /// 回调
        /// </summary>
        public event CallbackHandle callbackEvent;

        /// <summary>
        /// 处理回调事件的委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void CallbackHandle(object sender, CallbackEventArgs e);

        /// <summary>
        /// 数据实体
        /// </summary>
        public T item;

        /// <summary>
        /// 对话框视图
        /// </summary>
        protected readonly TV view;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">窗体标题</param>
        protected BaseModel(string title)
        {
            view = new TV {Text = title};
        }

        /// <summary>
        /// 回调方法
        /// </summary>
        /// <param name="methodName">回调方法名称</param>
        /// <param name="param">回调参数</param>
        protected void callback(string methodName, object[] param = null)
        {
            if (string.IsNullOrEmpty(methodName)) return;

            callbackEvent?.Invoke(this, new CallbackEventArgs(methodName, param));
        }

        /// <summary>
        /// 方法调用
        /// </summary>
        /// <param name="methodName">回调方法名称</param>
        /// <param name="param">回调参数</param>
        protected void call(string methodName, object[] param = null)
        {
            if (string.IsNullOrEmpty(methodName)) return;

            var method = GetType().GetMethod(methodName);
            if (method == null)
                Messages.showError("对不起，该功能尚未实现！");
            else
                method.Invoke(this, param);
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="args">MouseEventArgs</param>
        protected void mouseDownEvent(GridView gridView, MouseEventArgs args)
        {
            if (args.Button != MouseButtons.Right) return;

            var point = new Point(args.X, args.Y);
            hitInfo = gridView.CalcHitInfo(point);
        }

        /// <summary>
        /// 创建右键菜单并注册事件
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <returns>ContextMenuStrip</returns>
        protected ContextMenuStrip createContextMenu(GridView gridView)
        {
            var tsmi = new ToolStripMenuItem { Text = @"复制" };
            tsmi.Click += (sender, args) =>
            {
                if (hitInfo.Column == null) return;

                var content = gridView.GetRowCellDisplayText(hitInfo.RowHandle, hitInfo.Column);
                if (string.IsNullOrEmpty(content)) return;

                Clipboard.Clear();
                Clipboard.SetData(DataFormats.Text, content);
            };

            var menu = new ContextMenuStrip();
            menu.Items.Add(tsmi);
            return menu;
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        public void show()
        {
            view.Show();
            view.Refresh();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        public void close()
        {
            view.Close();
        }

        /// <summary>
        /// 关闭对话框确认
        /// </summary>
        /// <param name="args">取消事件参数</param>
        protected void closeConfirm(CancelEventArgs args)
        {
            const string msg = "您确定要放弃所做的变更，并关闭对话框吗？";
            args.Cancel = view.DialogResult != DialogResult.OK && !Messages.showConfirm(msg);
        }
    }
}