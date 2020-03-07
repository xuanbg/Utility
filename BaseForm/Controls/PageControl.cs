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
        private int totalPages = 1;
        private int current;
        private Collection<string> pageSizes = new Collection<string> {"20", "40", "60", "80", "100"};

        /// <summary>  
        /// 每页显示行数发生改变，通知修改每页显示行数
        /// </summary>  
        public event PageSizeHandle pageSizeChanged;

        /// <summary>  
        /// 当前页发生改变，通知重新加载列表数据
        /// </summary>  
        public event PageReloadHandle currentPageChanged;

        /// <summary>  
        /// 列表总行数发生改变，通知修改FocusedRowHandle
        /// </summary>  
        public event TotalRowsHandle totalRowsChanged;

        /// <summary>
        /// 表示将处理每页显示行数改变事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void PageSizeHandle(object sender, PageSizeEventArgs e);

        /// <summary>
        /// 表示将处理列表数据需重新加载事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void PageReloadHandle(object sender, PageControlEventArgs e);

        /// <summary>
        /// 表示将处理列表总行数改变事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void TotalRowsHandle(object sender, PageControlEventArgs e);

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
                totalPages = (int) Math.Ceiling((decimal) rows/size);
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
            btnLast.Click += (sender, args) => changePage(totalPages - 1);
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

            var currentPage = current;
            refresh();

            var eventArgs = new PageControlEventArgs(focusedRowHandle, current + 1);
            if (current > currentPage)
            {
                // 切换了页码需要重新加载数据
                currentPageChanged?.Invoke(this, eventArgs);
            }
            else
            {
                totalRowsChanged?.Invoke(this, eventArgs);
            }
        }

        /// <summary>
        /// 减少列表成员
        /// </summary>
        /// <param name="count">减少数量，默认1个</param>
        public void removeItems(int count = 1)
        {
            rows -= count;
            if (handle >= rows) handle = rows - 1;

            var currentPage = current;
            refresh();

            var eventArgs = new PageControlEventArgs(focusedRowHandle, current + 1);
            if ((rows > 0 && handle < size*(totalPages - 1)) || current < currentPage)
            {
                // 不是末页或切换了页码需要重新加载数据
                currentPageChanged?.Invoke(this, eventArgs);
            }
            else
            {
                totalRowsChanged?.Invoke(this, eventArgs);
            }
        }

        /// <summary>
        /// 切换每页行数
        /// </summary>
        private void pageRowsChanged()
        {
            var handel = focusedRowHandle;
            var change = size < rows - size*current;
            size = int.Parse(cbeRows.Text);
            pageSizeChanged?.Invoke(this, new PageSizeEventArgs(size));

            var currentPage = current;
            refresh();

            change = change || size < rows - size*current;
            if (!change && current == currentPage && focusedRowHandle == handel) return;

            // 切换了页码或当前页显示行数变化后需要重新加载数据
            currentPageChanged?.Invoke(this, new PageControlEventArgs(focusedRowHandle, current + 1));
        }

        /// <summary>
        /// 切换当前页
        /// </summary>
        /// <param name="page">页码</param>
        private void changePage(int page)
        {
            handle = size*page;

            refresh();

            currentPageChanged?.Invoke(this, new PageControlEventArgs(focusedRowHandle, current + 1));
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        private void refresh()
        {
            if (handle > rows) handle = 0;

            var total = totalPages == 0 ? 1 : totalPages;
            labRows.Text = $@" 行/页 | 共 {rows} 行 | 分 {total} 页";
            labRows.Refresh();

            var val = (int) Math.Floor((decimal) handle/size);
            current = val < 0 ? 0 : val;
            btnFirst.Enabled = current > 0;
            btnPrev.Enabled = current > 0;
            btnNext.Enabled = current < totalPages - 1;
            btnLast.Enabled = current < totalPages - 1;
            btnJump.Enabled = totalPages > 1;

            var width = (int) Math.Log10(current + 1)*7 + 18;
            btnJump.Width = width;
            btnJump.Text = page.ToString();
            labRows.Focus();
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

            var currentPage = int.Parse(txtPage.Text);
            if (currentPage < 1 || currentPage > totalPages || currentPage == current + 1)
            {
                txtPage.EditValue = null;
                return;
            }

            txtPage.Visible = false;
            changePage(currentPage - 1);
        }
    }

    public class PageSizeEventArgs : EventArgs
    {
        /// <summary>
        /// PageSize
        /// </summary>
        public int pageSize { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rows">RowsPerPage</param>
        public PageSizeEventArgs(int rows)
        {
            pageSize = rows;
        }
    }

    public class PageControlEventArgs : EventArgs
    {
        /// <summary>
        /// RowHandle
        /// </summary>
        public int rowHandle { get; }

        /// <summary>
        /// currentPage
        /// </summary>
        public int currentPage { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="handel">RowHandle</param>
        /// <param name="current">currentPage</param>
        public PageControlEventArgs(int handel, int current)
        {
            rowHandle = handel;
            currentPage = current;
        }
    }
}