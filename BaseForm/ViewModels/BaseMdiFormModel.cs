using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Insight.Base.BaseForm.Controls;
using Insight.Base.BaseForm.Forms;
using Insight.Base.BaseForm.Utils;

namespace Insight.Base.BaseForm.ViewModels
{
    public class BaseMdiDialogModel<T, TV> : BaseModel<T, TV> where TV : BaseMdiForm, new()
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="item">对话框数据对象</param>
        public BaseMdiDialogModel(string title, T item) : base(title)
        {
            this.item = item;
            view.MdiParent = Application.OpenForms["MainWindow"];
            view.Name = title;
        }

        /// <summary>
        /// 初始化列表控件
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="gridView">列表View控件</param>
        /// <param name="callMethod">列表数据改变事件调用方法名称</param>
        /// <param name="callbackMethod">列表控件双击事件回调方法名称</param>
        /// <param name="pageControl">列表分页控件</param>
        /// <param name="getDataMethod">列表获取数据方法名称</param>
        public void initGrid(GridControl grid, GridView gridView, string callMethod = null, string callbackMethod = null, PageControl pageControl = null, string getDataMethod = "loadData")
        {
            gridView.FocusedRowObjectChanged += (sender, args) =>
            {
                call(callMethod, new object[] {args.FocusedRowHandle});
                if (pageControl != null) pageControl.rowHandle = args.FocusedRowHandle;
            };
            gridView.DoubleClick += (sender, args) =>
            {
                if (callbackMethod == null) return;

                callback(callbackMethod);
            };
            grid.MouseDown += (sender, args) => mouseDownEvent(gridView, args);

            Format.gridFormat(gridView);
            grid.ContextMenuStrip = createContextMenu(gridView);

            // 注册分页事件
            if (pageControl == null) return;

            pageControl.focusedRowChanged += (sender, args) => gridView.FocusedRowHandle = args.rowHandle;
            pageControl.selectDataChanged += (sender, args) =>
            {
                gridView.RefreshData();
                gridView.FocusedRowHandle = args.rowHandle;
            };
            pageControl.pageReload += (sender, args) =>
            {
                call(getDataMethod, new object[] { args.page });
                gridView.FocusedRowHandle = args.handle;
            };
        }
    }
}
