using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Insight.Utils.Controls
{
    public partial class PageControl : XtraUserControl
    {
        private bool _IsFirst = true;
        private int _Rows;
        private int _Index;
        private int _Current;
        private bool _PageChanged;

        /// <summary>
        /// 每页行数下拉列表选项
        /// </summary>
        public object[] RowsSelectItems { get; set; } = {"20", "40", "60", "80", "100"};

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
        public int RowsPerPage { get; private set; } = 20;

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get { return _Current + 1; }
            set
            {
                if (value < 1 || value > TotalPages || value == _Current + 1) return;

                _Current = value - 1;
                PageChanged = true;
                CurrentPageChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// 当前选中行索引
        /// </summary>
        public int FocusedRowIndex { get; private set; }

        /// <summary>
        /// 当前选中行Handle
        /// </summary>
        public int RowHandle => _Index - _Current*RowsPerPage;

        /// <summary>
        /// 当前选中行Handle
        /// </summary>
        public int FocusedRowHandle => FocusedRowIndex - _Current*RowsPerPage;

        /// <summary>
        /// 当前页已改变
        /// </summary>
        public bool PageChanged
        {
            get { return _PageChanged; }
            private set
            {
                Refresh();
                _PageChanged = value;
            }
        }

        /// <summary>
        /// 当前页删除行后是否需要重新加载列表
        /// </summary>
        public bool NeedReload => _Rows - _Current*RowsPerPage >= RowsPerPage;

        /// <summary>
        /// 焦点所在行是最后一行
        /// </summary>
        public bool IsLastRow => FocusedRowIndex == CurrentPage*RowsPerPage - 1;

        /// <summary>
        /// 构造方法
        /// </summary>
        public PageControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置当前选中行索引
        /// </summary>
        /// <param name="type">RowIndex类型：1、RowHandle；2、RowIndex；3、TotalRows</param>
        /// <param name="index">Index值</param>
        /// <param name="copy">是否在RowHandle属性保存副本，默认值为True</param>
        public void SetRowIndex(int type, int index = 0, bool copy = true)
        {
            switch (type)
            {
                case 1:
                    FocusedRowIndex = _Current*RowsPerPage + index;
                    break;

                case 2:
                    FocusedRowIndex = index;
                    break;

                case 3:
                    FocusedRowIndex = index > 0 ? index : _Rows - 1;
                    break;
            }

            if (FocusedRowIndex >= _Rows) FocusedRowIndex = _Rows - 1;

            if (copy) _Index = FocusedRowIndex;

            var page = (int) Math.Ceiling((decimal) (FocusedRowIndex + 1)/RowsPerPage) - 1;
            if (page == _Current) return;

            _Current = page;
            PageChanged = true;
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
        /// 控件加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageControl_Load(object sender, EventArgs e)
        {
            if (RowsSelectItems == null) return;

            cbeRows.Properties.Items.AddRange(RowsSelectItems);
            cbeRows.SelectedIndex = 0;
        }

        /// <summary>
        /// 切换每页行数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbeRows_EditValueChanged(object sender, EventArgs e)
        {
            RowsPerPage = int.Parse(cbeRows.Text);
            _Current = (int)Math.Ceiling((decimal)(FocusedRowIndex + 1) / RowsPerPage) - 1;

            Refresh();
            if (!_IsFirst) RowsPerPageChanged?.Invoke(this, null);

            _IsFirst = false;
        }

        /// <summary>
        /// 转到首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
        }

        /// <summary>
        /// 转到上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
        }

        /// <summary>
        /// 转到下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
        }

        /// <summary>
        /// 转到末页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            CurrentPage = TotalPages;
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

            txtPage.Visible = false;
            CurrentPage = page;
        }

        /// <summary>
        /// 输入总行数
        /// </summary>
        /// <param name="rows">总行数</param>
        private void SetTotalRows(int rows)
        {
            _Rows = rows;
            TotalPages = (int) Math.Ceiling((decimal) rows/RowsPerPage);
            PageChanged = false;
            labRows.Text = $" 行/页 | 共 {rows} 行 | 分 {TotalPages} 页";
            labRows.Refresh();

            Refresh();
        }
    }
}
