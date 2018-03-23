using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Insight.Utils.Controls
{
    public partial class PageControl : XtraUserControl
    {
        private int handle;
        private int pageSize;
        private int rows;
        private int totalPages = 1;
        private int current;
        private Collection<string> pageSizes = new Collection<string> {"20", "40", "60", "80", "100"};

        /// <summary>  
        /// 每页显示行数发生改变，通知修改每页显示行数
        /// </summary>  
        public event PageSizeHandle PageSizeChanged;

        /// <summary>  
        /// 当前页发生改变，通知重新加载列表数据
        /// </summary>  
        public event PageReloadHandle CurrentPageChanged;

        /// <summary>  
        /// 列表总行数发生改变，通知修改FocusedRowHandle
        /// </summary>  
        public event TotalRowsHandle TotalRowsChanged;

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
        /// 当前选中行Handle
        /// </summary>
        public int focusedRowHandle
        {
            get => handle - pageSize*current;
            set => handle = pageSize*current + value;
        }

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
                pageSize = int.Parse(pageSizes[0]);
            }
        }

        /// <summary>
        /// 总行数
        /// </summary>
        public int totalRows
        {
            get => rows;
            set
            {
                rows = value;
                totalPages = (int) Math.Ceiling((decimal) rows/pageSize);
                Refresh();
            }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int currentPage => current + 1;

        /// <summary>
        /// 构造方法
        /// </summary>
        public PageControl()
        {
            InitializeComponent();

            // 订阅控件按钮事件
            cbeRows.EditValueChanged += (sender, args) => PageRowsChanged();
            btnFirst.Click += (sender, args) => ChangePage(0);
            btnPrev.Click += (sender, args) => ChangePage(current - 1);
            btnNext.Click += (sender, args) => ChangePage(current + 1);
            btnLast.Click += (sender, args) => ChangePage(totalPages - 1);
        }

        /// <summary>
        /// 增加列表成员
        /// </summary>
        /// <param name="count">增加数量，默认1个</param>
        public void AddItems(int count = 1)
        {
            rows += count;
            handle = rows - 1;

            var page = current;
            Refresh();

            if (current > page)
            {
                // 切换了页码需要重新加载数据
                CurrentPageChanged?.Invoke(this, new PageControlEventArgs(focusedRowHandle));
            }
            else
            {
                TotalRowsChanged?.Invoke(this, new PageControlEventArgs(focusedRowHandle));
            }
        }

        /// <summary>
        /// 减少列表成员
        /// </summary>
        /// <param name="count">减少数量，默认1个</param>
        public void RemoveItems(int count = 1)
        {
            rows -= count;
            if (handle >= rows) handle = rows - 1;

            var page = current;
            Refresh();

            if ((rows > 0 && handle < pageSize*(totalPages - 1)) || current < page)
            {
                // 不是末页或切换了页码需要重新加载数据
                CurrentPageChanged?.Invoke(this, new PageControlEventArgs(focusedRowHandle));
            }
            else
            {
                TotalRowsChanged?.Invoke(this, new PageControlEventArgs(focusedRowHandle));
            }
        }

        /// <summary>
        /// 切换每页行数
        /// </summary>
        private void PageRowsChanged()
        {
            var handel = focusedRowHandle;
            var change = pageSize < rows - pageSize*current;
            pageSize = int.Parse(cbeRows.Text);
            PageSizeChanged?.Invoke(this, new PageSizeEventArgs(pageSize));

            var page = current;
            Refresh();

            change = change || pageSize < rows - pageSize*current;
            if (!change && current == page && focusedRowHandle == handel) return;

            // 切换了页码或当前页显示行数变化后需要重新加载数据
            CurrentPageChanged?.Invoke(this, new PageControlEventArgs(focusedRowHandle));
        }

        /// <summary>
        /// 切换当前页
        /// </summary>
        /// <param name="page">页码</param>
        private void ChangePage(int page)
        {
            handle = pageSize*page;

            Refresh();

            CurrentPageChanged?.Invoke(this, new PageControlEventArgs(focusedRowHandle));
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        private new void Refresh()
        {
            if (handle > rows) handle = 0;

            var total = totalPages == 0 ? 1 : totalPages;
            labRows.Text = $" 行/页 | 共 {rows} 行 | 分 {total} 页";
            labRows.Refresh();

            var val = (int) Math.Floor((decimal) handle/pageSize);
            current = val < 0 ? 0 : val;
            btnFirst.Enabled = current > 0;
            btnPrev.Enabled = current > 0;
            btnNext.Enabled = current < totalPages - 1;
            btnLast.Enabled = current < totalPages - 1;
            btnJump.Enabled = totalPages > 1;

            var width = (int) Math.Log10(current + 1)*7 + 18;
            btnJump.Width = width;
            btnJump.Text = currentPage.ToString();
            labRows.Focus();
        }

        /// <summary>
        /// 跳转到指定页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Jump_Click(object sender, EventArgs e)
        {
            txtPage.Visible = true;
            txtPage.Focus();
        }

        /// <summary>
        /// 焦点离开输入框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageInput_Leave(object sender, EventArgs e)
        {
            txtPage.EditValue = null;
            txtPage.Visible = false;
        }

        /// <summary>
        /// 输入页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                txtPage.EditValue = null;
                txtPage.Visible = false;
                return;
            }

            if (e.KeyChar != 13) return;

            if (string.IsNullOrEmpty(txtPage.Text)) return;

            var page = int.Parse(txtPage.Text);
            if (page < 1 || page > totalPages || page == current + 1)
            {
                txtPage.EditValue = null;
                return;
            }

            txtPage.Visible = false;
            ChangePage(page - 1);
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
        /// 构造函数
        /// </summary>
        /// <param name="handel">RowHandle</param>
        public PageControlEventArgs(int handel)
        {
            rowHandle = handel;
        }
    }
}