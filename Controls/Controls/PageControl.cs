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
        public int Rows
        {
            get { return _Rows; }
            set { SetRows(value); }
        }
        
        /// <summary>
        /// 每页行数
        /// </summary>
        public int RowCount { get; set; } = 20;

        /// <summary>
        /// 总页数
        /// </summary>
        public int Pages { get; private set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; private set; } = 1;

        /// <summary>
        /// 当前选中行索引
        /// </summary>
        public int RowIndex { get; private set; }

        /// <summary>
        /// 当前选中行
        /// </summary>
        public int FocusedRowHandle => RowIndex - RowCount*(CurrentPage - 1);

        /// <summary>
        /// 构造方法
        /// </summary>
        public PageControl()
        {
            InitializeComponent();
            cbeRows.EditValue = RowCount;
        }

        /// <summary>
        /// 设置当前选中行索引
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        public void SetRowIndex(IndexType type, int index = 0)
        {
            switch (type)
            {
                case IndexType.Handle:
                    RowIndex = (CurrentPage - 1)*RowCount + index;
                    break;

                case IndexType.Index:
                    RowIndex = index;
                    break;

                case IndexType.New:
                    RowIndex = Rows - 1;
                    break;
            }
        }

        /// <summary>
        /// 设置当前页
        /// </summary>
        /// <param name="page"></param>
        public void SetPage(int page)
        {
            if (page < 1 || page > Pages) return;

            CurrentPage = page;

            Refresh();
            PageRowsChange?.Invoke(this, null);
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        public override void Refresh()
        {
            btnFirst.Enabled = CurrentPage > 1;
            btnPrev.Enabled = CurrentPage > 1;
            btnNext.Enabled = CurrentPage < Pages;
            btnLast.Enabled = CurrentPage < Pages;
            btnJump.Enabled = Pages > 1;

            var width = (int)Math.Log10(CurrentPage) * 7 + 18;
            btnJump.Width = width;
            btnJump.Text = CurrentPage.ToString();
            labRows.Focus();
        }

        /// <summary>  
        /// 自定义事件 当前页改变事件  
        /// </summary>  
        public event EventHandler CurrentPageChange;

        /// <summary>  
        /// 自定义事件
        /// </summary>  
        public event EventHandler PageRowsChange;

        /// <summary>
        /// 切换每页行数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbeRows_EditValueChanged(object sender, EventArgs e)
        {
            RowCount = int.Parse(cbeRows.Text);
            CurrentPage = (int)Math.Ceiling((decimal)(RowIndex + 1) / RowCount);

            Refresh();
            PageRowsChange?.Invoke(this, null);
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
            CurrentPageChange?.Invoke(this, null);
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
            CurrentPageChange?.Invoke(this, null);
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
            CurrentPageChange?.Invoke(this, null);
        }

        /// <summary>
        /// 转到末页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            CurrentPage = Pages;
            Refresh();
            CurrentPageChange?.Invoke(this, null);
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
            if (page < 1 || page > Pages || page == CurrentPage)
            {
                txtPage.EditValue = null;
                return;
            }

            CurrentPage = page;
            Refresh();
            CurrentPageChange?.Invoke(this, null);
        }

        /// <summary>
        /// 输入总行数
        /// </summary>
        /// <param name="rows">总行数</param>
        private void SetRows(int rows)
        {
            _Rows = rows;
            Pages = (int) Math.Ceiling((decimal) rows/RowCount);
            labRows.Text = $" 行/页 | 共 {rows} 行 | 分 {Pages} 页";
            labRows.Refresh();

            Refresh();
        }

        /// <summary>
        /// 索引类型
        /// </summary>
        public enum IndexType
        {
            Index,
            Handle,
            New
        }
    }
}
