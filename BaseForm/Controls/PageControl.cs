using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Insight.Utils.Controls
{
    public partial class PageControl : XtraUserControl
    {
        private int handle;
        private int rows;
        private int totalPages;
        private int current;
        private Collection<string> pageSizes = new Collection<string> {"20", "40", "60", "80", "100"};

        /// <summary>  
        /// 当前焦点行发生改变，通知修改焦点行
        /// </summary>  
        public event FocusedRowChangedHandle focusedRowChanged;

        /// <summary>
        /// 表示将处理当前焦点行发生改变事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void FocusedRowChangedHandle(object sender, RowHandleEventArgs e);

        /// <summary>  
        /// 当前页需要重新加载，通知重新加载列表数据
        /// </summary>  
        public event PageReloadHandle currentPageChanged;
        
        /// <summary>
        /// 表示将处理列表数据需重新加载事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void PageReloadHandle(object sender, PageReloadEventArgs e);
        
        /// <summary>
        /// 每页行数下拉列表选项
        /// </summary>
        public Collection<string> pageSizeItems
        {
            get => pageSizes;
            set
            {
                pageSizes = value;
                cbeRows.Properties.Items.AddRange(value);
                cbeRows.SelectedIndex = 0;
                size = int.Parse(pageSizes[0]);
            }
        }

        /// <summary>
        /// 总行数
        /// </summary>
        public int totalRows
        {
            set
            {
                rows = value;

                refresh();
            }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page => current + 1;

        /// <summary>
        /// 当前每页行数
        /// </summary>
        public int size { get; private set; }

        /// <summary>
        /// 当前选中行Handle
        /// </summary>
        public int focusedRowHandle
        {
            get => handle - size * current;
            set => handle = size * current + value;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public PageControl()
        {
            InitializeComponent();

            // 订阅控件按钮事件
            cbeRows.EditValueChanged += (sender, args) => pageRowsChanged();
            btnFirst.Click += (sender, args) => changePage(0);
            btnPrev.Click += (sender, args) => changePage(current - 1);
            btnNext.Click += (sender, args) => changePage(current + 1);
            btnLast.Click += (sender, args) => changePage(totalPages);
            btnJump.Click += (sender, args) =>  jumpClick();
            txtPage.KeyPress += (sender, args) => pageInputKeyPress(args);
            txtPage.Leave += (sender, args) => pageInputLeave();
        }

        /// <summary>
        /// 增加列表成员
        /// </summary>
        /// <param name="count">增加数量，默认1个</param>
        public void addItems(int count = 1)
        {
            rows += count;
            handle = rows - 1;

            refresh();
        }

        /// <summary>
        /// 减少列表成员
        /// </summary>
        /// <param name="count">减少数量，默认1个</param>
        public void removeItems(int count = 1)
        {
            rows -= count;

            refresh();
        }

        /// <summary>
        /// 切换每页行数
        /// </summary>
        private void pageRowsChanged()
        {
            size = int.Parse(cbeRows.Text);

            refresh(true);
        }

        /// <summary>
        /// 切换当前页
        /// </summary>
        /// <param name="page">页码</param>
        private void changePage(int page)
        {
            var zeroHandle = page * size;
            handle = zeroHandle;

            refresh();
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        /// <param name="reload">是否强制重新加载</param>
        private void refresh(bool reload = false)
        {
            var currentPage = current;
            if (handle >= rows) handle = rows - 1;

            totalPages = rows / size;
            labRows.Text = $@" 行/页 | 共 {rows} 行 | 分 {totalPages +1} 页";
            labRows.Refresh();

            current = handle / size;
            btnFirst.Enabled = current > 0;
            btnPrev.Enabled = current > 0;
            btnNext.Enabled = current < totalPages - 1;
            btnLast.Enabled = current < totalPages - 1;
            btnJump.Enabled = totalPages > 1;

            var width = (int) Math.Log10(current + 1)*7 + 18;
            btnJump.Width = width;
            btnJump.Text = page.ToString();
            labRows.Focus();

            if (!reload && current == currentPage)
            {
                var eventArgs = new RowHandleEventArgs(focusedRowHandle);
                focusedRowChanged?.Invoke(this, eventArgs);
            }
            else
            {
                var eventArgs = new PageReloadEventArgs(focusedRowHandle, page, size);
                currentPageChanged?.Invoke(this, eventArgs);
            }
        }

        /// <summary>
        /// 跳转到指定页
        /// </summary>
        private void jumpClick()
        {
            txtPage.Visible = true;
            txtPage.Focus();
        }

        /// <summary>
        /// 焦点离开输入框
        /// </summary>
        private void pageInputLeave()
        {
            txtPage.EditValue = null;
            txtPage.Visible = false;
        }

        /// <summary>
        /// 输入页码
        /// </summary>
        /// <param name="e"></param>
        private void pageInputKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                txtPage.EditValue = null;
                txtPage.Visible = false;
                return;
            }

            if (e.KeyChar != 13) return;

            if (string.IsNullOrEmpty(txtPage.Text)) return;

            var val = int.Parse(txtPage.Text);
            if (val < 1 || val > totalPages || val == page)
            {
                txtPage.EditValue = null;
                return;
            }

            txtPage.Visible = false;
            changePage(val - 1);
        }
    }

    /// <summary>
    /// 焦点行改变事件参数
    /// </summary>
    public class RowHandleEventArgs : EventArgs
    {
        /// <summary>
        /// Row handle
        /// </summary>
        public int rowHandle { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="handel">RowsPerPage</param>
        public RowHandleEventArgs(int handel)
        {
            rowHandle = handel;
        }
    }

    /// <summary>
    /// 页面重载事件参数
    /// </summary>
    public class PageReloadEventArgs : EventArgs
    {
        /// <summary>
        /// Row handle
        /// </summary>
        public int handle { get; }

        /// <summary>
        /// Current page
        /// </summary>
        public int page { get; }

        /// <summary>
        /// Page size
        /// </summary>
        public int size { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="handle">Row handle</param>
        /// <param name="page">Current page</param>
        /// <param name="size">Page size</param>
        public PageReloadEventArgs(int handle, int page, int size)
        {
            this.handle = handle;
            this.page = page;
            this.size = size;
        }
    }
}