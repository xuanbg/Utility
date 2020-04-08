using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using Insight.Utils.BaseForms;
using Insight.Utils.Common;
using Insight.Utils.Controls;
using Insight.Utils.Entity;

namespace Insight.Utils.BaseViewModels
{
    public class BaseMdiDialogModel<T, TV> : BaseModel<T, TV> where TV : BaseMdiDialog, new()
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="item">对话框数据对象</param>
        protected BaseMdiDialogModel(string title, T item) : base(title)
        {
            this.item = item;
            view.MdiParent = Application.OpenForms["MainWindow"];
            view.Name = title;
        }

        /// <summary>
        /// 初始化MDI窗体
        /// </summary>
        /// <param name="module">模块信息</param>
        public void initMdiView(ModuleDto module)
        {
            var icon = Util.getImage(module.moduleInfo.iconUrl);
            view.ControlBox = module.index > 0;
            view.MdiParent = Application.OpenForms["MainWindow"];
            view.Icon = Icon.FromHandle(new Bitmap(icon).GetHicon());
            view.Name = module.moduleInfo.module;
            view.Text = module.name;
        }

        /// <summary>
        /// 初始化列表控件
        /// </summary>
        /// <param name="grid">列表View控件</param>
        /// <param name="callMethod">列表数据改变事件调用方法名称</param>
        /// <param name="callbackMethod">列表控件双击事件回调方法名称</param>
        /// <param name="pageControl">列表分页控件</param>
        /// <param name="getDataMethod">列表获取数据方法名称</param>
        protected void initGrid(GridView grid, string callMethod = null, string callbackMethod = null, PageControl pageControl = null, string getDataMethod = "loadData")
        {
            grid.FocusedRowObjectChanged += (sender, args) =>
            {
                if (pageControl != null) pageControl.focusedRowHandle = args.FocusedRowHandle;

                call(callMethod, new object[] { args.FocusedRowHandle });
            };
            grid.DoubleClick += (sender, args) =>
            {
                if (callbackMethod == null) return;

                callback(callbackMethod);
            };

            Format.gridFormat(grid);
            if (pageControl == null) return;

            pageControl.pageReload += (sender, args) => call(getDataMethod, new object[] { args.page, args.handle });
            pageControl.focusedRowChanged += (sender, args) => grid.FocusedRowHandle = args.rowHandle;
            pageControl.selectDataChanged += (sender, args) => grid.RefreshData();
        }
    }
}
