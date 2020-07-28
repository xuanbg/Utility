using System;
using System.Collections.ObjectModel;
using DevExpress.XtraEditors;
using Insight.Base.BaseForm.Entities;

namespace Insight.Base.BaseForm.Controls
{
    public partial class PageControl : XtraUserControl
    {
        /// <summary>
        /// 每页行数选项集合
        /// </summary>
        private Collection<string> pageSizes = new Collection<string> {"20", "40", "60", "80", "100"};

        /// <summary>
        /// 总行数
        /// </summary>
        private int rows;

        /// <summary>
        /// 当前行(0 - rows)
        /// </summary>
        private int currentRow;

        /// <summary>
        /// 总页数
        /// </summary>
        private int pages => size > 0 ? ((rows - 1) / size) + 1 : 1;

        /// <summary>
        /// 当前页(0 - pages)
        /// </summary>
        private int currentPage;

        /// <summary>
        /// 表示将处理当前焦点行数据发生改变事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DataChangedHandle(object sender, RowHandleEventArgs e);

        /// <summary>
        /// 表示将处理列表数据需重新加载事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ReloadPageHandle(object sender, ReloadPageEventArgs e);

        /// <summary>  
        /// 当前焦点行数据发生改变，通知刷新列表数据
        /// </summary>  
        public event DataChangedHandle dataChanged;

        /// <summary>  
        /// 当前页需要重新加载，通知重新加载列表数据
        /// </summary>  
        public event ReloadPageHandle reloadPage;

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
        /// 排序方式
        /// </summary>
        public OrderBy orderBy { get; set; } = OrderBy.POSITIVE;

        /// <summary>
        /// 页码(1 - pages)
        /// </summary>
        public int page => currentPage + 1;

        /// <summary>
        /// 当前每页行数
        /// </summary>
        public int size { get; private set; }

        /// <summary>
        /// 当前选中行(0 - size)
        /// </summary>
        public int rowHandle
        {
            private get => rows > 0 ? currentRow % size : -1;
            set => currentRow = currentPage * size + value;
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
        /// 构造方法
        /// </summary>
        public PageControl()
        {
            InitializeComponent();

            // 每页行数改变事件
            cbeRows.EditValueChanged += (sender, args) =>
            {
                size = int.Parse(cbeRows.Text);
                refresh(true);
            };

            // 导航按钮点击事件
            btnFirst.Click += (sender, args) => changePage(1);
            btnPrev.Click += (sender, args) => changePage(page - 1);
            btnNext.Click += (sender, args) => changePage(page + 1);
            btnLast.Click += (sender, args) => changePage(pages);
            btnJump.Click += (sender, args) =>
            {
                txtPage.Visible = true;
                txtPage.Focus();
            };

            // 页跳转输入事件
            txtPage.Leave += (sender, args) =>
            {
                txtPage.EditValue = null;
                txtPage.Visible = false;
            };
            txtPage.KeyPress += (sender, args) =>
            {
                if (args.KeyChar == 27)
                {
                    txtPage.EditValue = null;
                    txtPage.Visible = false;
                    return;
                }

                if (args.KeyChar != 13) return;

                if (string.IsNullOrEmpty(txtPage.Text)) return;

                var val = int.Parse(txtPage.Text);
                if (val < 1 || val > pages || val == page)
                {
                    txtPage.EditValue = null;
                    return;
                }

                txtPage.Visible = false;
                changePage(val);
            };
        }

        /// <summary>
        /// 增加列表成员
        /// </summary>
        /// <param name="count">增加数量，默认1个</param>
        public void addItems(int count = 1)
        {
            rows += count;
            currentRow = orderBy == OrderBy.POSITIVE ? rows - 1 : 0;

            refresh();
        }

        /// <summary>
        /// 减少列表成员
        /// </summary>
        /// <param name="count">减少数量，默认1个</param>
        public void removeItems(int count = 1)
        {
            rows -= count;
            if (currentRow >= rows) currentRow = rows - 1;

            refresh(currentPage < rows / size);
        }

        /// <summary>
        /// 切换当前页
        /// </summary>
        /// <param name="page">页码</param>
        private void changePage(int page)
        {
            currentRow = (page - 1) * size;
            currentPage = page - 1;
            txtPage.EditValue = page;

            reloadPage?.Invoke(this, new ReloadPageEventArgs(page, size, rowHandle));
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        /// <param name="reload">页数据是否重新加载</param>
        private void refresh(bool reload = false)
        {
            var cp = currentPage;
            labRows.Text = $@" 行/页 | 共 {rows} 行 | 分 {pages} 页";
            labRows.Refresh();
            labRows.Focus();

            // 根据当前选中行定位当前页并刷新导航按钮可用状态
            currentPage = size == 0 ? 0 : currentRow / size;
            if (currentPage < 0) currentPage = 0;

            btnFirst.Enabled = currentPage > 0;
            btnPrev.Enabled = currentPage > 0;
            btnNext.Enabled = currentPage < pages - 1;
            btnLast.Enabled = currentPage < pages - 1;
            btnJump.Enabled = pages > 3;
            btnJump.Width = (int) Math.Log10(page) * 7 + 18;
            btnJump.Text = page.ToString();

            // 根据是否需要重新加载页面或当前页是否改变，触发重新加载事件。否则刷新数据
            if (reload || currentPage != cp)
            {
                reloadPage?.Invoke(this, new ReloadPageEventArgs(page, size, rowHandle));
            }
            else
            {
                dataChanged?.Invoke(this, new RowHandleEventArgs(rowHandle));
            }
        }
    }

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum OrderBy
    {
        POSITIVE,
        REVERSE
    }
}