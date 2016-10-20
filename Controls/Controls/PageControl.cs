using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Insight.Utils.Controls
{
    public partial class PageControl : XtraUserControl
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; private set; } = 1;

        /// <summary>
        /// 当前行
        /// </summary>
        public int RowIndex { get; set; } = 0;

        /// <summary>
        /// 每页行数
        /// </summary>
        public int RowCount { get; set; } = 20;

        /// <summary>
        /// 总行数
        /// </summary>
        public int Rows
        {
            get { return _Rows; }
            set { SetRows(value); }
        }
        
        /// <summary>  
        /// 自定义事件 当前页改变事件  
        /// </summary>  
        public event EventHandler CurrentPageChange;

        /// <summary>  
        /// 自定义事件
        /// </summary>  
        public event EventHandler PageRowsChange;

        private int _Rows;
        private int _Pages;

        /// <summary>
        /// 构造方法
        /// </summary>
        public PageControl()
        {
            InitializeComponent();
            cbeRows.EditValue = RowCount;
        }

        /// <summary>
        /// 切换每页行数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbeRows_EditValueChanged(object sender, EventArgs e)
        {
            RowCount = int.Parse(cbeRows.Text);

            float rows = _Rows == 0 ? 1 : _Rows;
            CurrentPage = (int)Math.Ceiling((RowIndex + 1) / rows);

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
            CurrentPage = _Pages;
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
            if (page < 1 || page > _Pages || page == CurrentPage)
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
            _Pages = (int) Math.Ceiling((decimal) rows/RowCount);
            labRows.Text = $" 行/页 | 共 {rows} 行 | 分 {_Pages} 页";
            labRows.Refresh();

            Refresh();
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        public override void Refresh()
        {
            btnFirst.Enabled = CurrentPage > 1;
            btnPrev.Enabled = CurrentPage > 1;
            btnNext.Enabled = CurrentPage < _Pages;
            btnLast.Enabled = CurrentPage < _Pages;
            btnJump.Enabled = _Pages > 1;

            var width = (int) Math.Log10(CurrentPage)*7 + 18;
            btnJump.Width = width;
            btnJump.Text = CurrentPage.ToString();
            labRows.Focus();
        }
    }
}
