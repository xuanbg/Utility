﻿using System;
using System.Collections.ObjectModel;
using DevExpress.XtraEditors;
using Insight.Base.BaseForm.Entities;

namespace Insight.Base.BaseForm.Controls
{
    public partial class PageControl : XtraUserControl
    {
        private Collection<string> pageSizes = new Collection<string> {"20", "40", "60", "80", "100"};
        private int currentRow;
        private int currentPage;
        private int rows;

        /// <summary>
        /// 总页数
        /// </summary>
        private int pages => size == 0 ? 0 : (rows - 1) / size;

        /// <summary>  
        /// 当前焦点行发生改变，通知修改焦点行
        /// </summary>  
        public event SelectDataChangedHandle selectDataChanged;

        /// <summary>
        /// 表示将处理当前焦点行发生改变事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SelectDataChangedHandle(object sender, EventArgs e);

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
        public event PageReloadHandle pageReload;
        
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
                refresh(0, false, true);
            }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page
        {
            get => currentPage + 1;
            set
            {
                var handle = focusedRowHandle;
                currentPage = value - 1;
                currentRow = currentPage * size + handle;

                refresh(currentRow, false, true);
            } 
        }

        /// <summary>
        /// 当前每页行数
        /// </summary>
        public int size { get; private set; }

        /// <summary>
        /// 当前选中行Handle
        /// </summary>
        public int focusedRowHandle
        {
            get => currentRow - size * currentPage;
            set => currentRow = size * currentPage + value;
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
                refresh(currentRow, true);
            };

            // 导航按钮点击事件
            btnFirst.Click += (sender, args) => changePage(0);
            btnPrev.Click += (sender, args) => changePage(currentPage - 1);
            btnNext.Click += (sender, args) => changePage(currentPage + 1);
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
                if (val < 1 || val > pages + 1 || val == page)
                {
                    txtPage.EditValue = null;
                    return;
                }

                txtPage.Visible = false;
                changePage(val - 1);
            };
        }

        /// <summary>
        /// 增加列表成员
        /// </summary>
        /// <param name="count">增加数量，默认1个</param>
        public void addItems(int count = 1)
        {
            var focusedRow = currentRow;
            rows += count;
            currentRow = rows - 1;

            refresh(focusedRow);
            selectDataChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 减少列表成员
        /// </summary>
        /// <param name="count">减少数量，默认1个</param>
        public void removeItems(int count = 1)
        {
            var focusedRow = currentRow;
            rows -= count;

            refresh(focusedRow);
            selectDataChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 切换当前页
        /// </summary>
        /// <param name="page">页码</param>
        private void changePage(int page)
        {
            currentRow = page * size;
            currentPage = page;
            pageReload?.Invoke(this, new PageReloadEventArgs(this.page, size, focusedRowHandle));
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        /// <param name="focusedRow">当前焦点行</param>
        /// <param name="reload">是否强制重新加载</param>
        /// <param name="first">是否首次加载</param>
        private void refresh(int focusedRow, bool reload = false, bool first = false)
        {
            labRows.Text = $@" 行/页 | 共 {rows} 行 | 分 {pages + 1} 页";
            labRows.Refresh();
            labRows.Focus();

            // 对当前选中行的异常值进行修正
            if (currentRow >= rows) currentRow = rows - 1;

            // 根据当前选中行定位当前页并刷新导航按钮可用状态
            var cp = currentPage;
            currentPage = size == 0 ? 0 : currentRow / size;
            if (currentPage < 0) currentPage = 0;

            btnFirst.Enabled = currentPage > 0;
            btnPrev.Enabled = currentPage > 0;
            btnNext.Enabled = currentPage < pages;
            btnLast.Enabled = currentPage < pages;
            btnJump.Enabled = pages > 2;
            btnJump.Width = (int) Math.Log10(page) * 7 + 18;
            btnJump.Text = page.ToString();

            // 根据当前页是否改变或页内容是否需要重新加载触发重新加载事件
            if (!first && (reload || currentPage != cp))
            {
                pageReload?.Invoke(this, new PageReloadEventArgs(page, size, focusedRowHandle));
                return;
            }

            // 根据焦点行是否改变触发焦点行改变或刷新列表事件
            if (first || focusedRow == currentRow)
                selectDataChanged?.Invoke(this, EventArgs.Empty);
            else
                focusedRowChanged?.Invoke(this, new RowHandleEventArgs(focusedRowHandle));
        }
    }
}