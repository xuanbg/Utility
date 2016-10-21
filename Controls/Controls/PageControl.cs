using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Insight.Utils.Controls
{
    public partial class PageControl : XtraUserControl
    {
        private int _Rows;

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRows
        {
            get { return _Rows; }
            set { SetTotalRows(value); }
        }
        
        /// <summary>
        /// 每页行数
        /// </summary>
        public int RowsPerPage { get; set; } = 20;

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; private set; } = 1;

        /// <summary>
        /// 当前选中行索引
        /// </summary>
        public int FocusedRowIndex { get; private set; }

        /// <summary>
        /// 当前选中行
        /// </summary>
        public int FocusedRowHandle => FocusedRowIndex - RowsPerPage*(CurrentPage - 1);

        /// <summary>
        /// 构造方法
        /// </summary>
        public PageControl()
        {
            InitializeComponent();
            cbeRows.EditValue = RowsPerPage;
        }

        /// <summary>
        /// 设置当前选中行索引
        /// </summary>
        /// <param name="type">RowIndex类型：1、RowHandle；2、RowIndex；3、TotalRows</param>
        /// <param name="index">Index值</param>
        public void SetRowIndex(int type, int index = 0)
        {
            switch (type)
            {
                case 1:
                    FocusedRowIndex = (CurrentPage - 1)*RowsPerPage + index;
                    break;

                case 2:
                    FocusedRowIndex = index;
                    break;

                case 3:
                    FocusedRowIndex = TotalRows - 1;
                    break;
            }
        }

        /// <summary>
        /// 设置当前页
        /// </summary>
        /// <param name="page"></param>
        public void SetPage(int page)
        {
            if (page < 1 || page > TotalPages) return;

            CurrentPage = page;

            Refresh();
            RowsPerPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        public override void Refresh()
        {
            btnFirst.Enabled = CurrentPage > 1;
            btnPrev.Enabled = CurrentPage > 1;
            btnNext.Enabled = CurrentPage < TotalPages;
            btnLast.Enabled = CurrentPage < TotalPages;
            btnJump.Enabled = TotalPages > 1;

            var width = (int)Math.Log10(CurrentPage) * 7 + 18;
            btnJump.Width = width;
            btnJump.Text = CurrentPage.ToString();
            labRows.Focus();
        }

        /// <summary>  
        /// 当前页发生改变
        /// </summary>  
        public event EventHandler CurrentPageChanged;

        /// <summary>  
        /// 每页行数发生改变
        /// </summary>  
        public event EventHandler RowsPerPageChanged;

        /// <summary>
        /// 切换每页行数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbeRows_EditValueChanged(object sender, EventArgs e)
        {
            RowsPerPage = int.Parse(cbeRows.Text);
            CurrentPage = (int)Math.Ceiling((decimal)(FocusedRowIndex + 1) / RowsPerPage);

            Refresh();
            RowsPerPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 转到首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
            Refresh();
            CurrentPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 转到上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            Refresh();
            CurrentPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 转到下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            Refresh();
            CurrentPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 转到末页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            CurrentPage = TotalPages;
            Refresh();
            CurrentPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 跳转到指定页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJump_Click(object sender, EventArgs e)
        {
            txtPage.Visible = true;
            txtPage.Focus();
        }

        /// <summary>
        /// 焦点离开输入框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPage_Leave(object sender, EventArgs e)
        {
            txtPage.EditValue = null;
            txtPage.Visible = false;
        }

        /// <summary>
        /// 输入页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                txtPage.EditValue = null;
                txtPage.Visible = false;
                return;
            }

            if (e.KeyChar != 13) return;

            var page = int.Parse(txtPage.Text);
            if (page < 1 || page > TotalPages || page == CurrentPage)
            {
                txtPage.EditValue = null;
                return;
            }

            CurrentPage = page;
            Refresh();
            CurrentPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 输入总行数
        /// </summary>
        /// <param name="rows">总行数</param>
        private void SetTotalRows(int rows)
        {
            _Rows = rows;
            TotalPages = (int) Math.Ceiling((decimal) rows/RowsPerPage);
            labRows.Text = $" 行/页 | 共 {rows} 行 | 分 {TotalPages} 页";
            labRows.Refresh();

            Refresh();
        }
    }
}
