using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Insight.Utils.Controls
{
    public partial class PageControl : XtraUserControl
    {
        private bool _Reload;
        private int _Rows;
        private int _Index;
        private int _TotalPages = 1;
        private static Collection<string> _SelectItems = new Collection<string> {"20", "40", "60", "80", "100"};

        /// <summary>
        /// 每页行数下拉列表选项
        /// </summary>
        public Collection<string> RowsSelectItems
        {
            get { return _SelectItems; }
            set
            {
                _SelectItems = value;
                cbeRows.Properties.Items.AddRange(value);
                cbeRows.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRows
        {
            set { SetTotalRows(value); }
        }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int RowsPerPage { get; private set; } = int.Parse(_SelectItems[0]);

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; private set; } = 1;

        /// <summary>
        /// 当前选中行Index(全局)
        /// </summary>
        public int FocusedRowIndex => _Index;

        /// <summary>
        /// 当前选中行Handle(当前页)
        /// </summary>
        public int FocusedRowHandle => _Index - RowsPerPage*(CurrentPage - 1);

        /// <summary>
        /// 当前页删除行后是否需要重新加载列表
        /// </summary>
        public bool NeedReload => _Reload;

        /// <summary>
        /// 构造方法
        /// </summary>
        public PageControl()
        {
            InitializeComponent();

            cbeRows.Properties.Items.AddRange(_SelectItems);
            cbeRows.SelectedIndex = 0;
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
                    _Index = RowsPerPage * (CurrentPage - 1) + index;
                    break;

                case 2:
                    _Index = index;
                    break;

                case 3:
                    _Index = index > 0 ? index : _Rows - 1;
                    break;
            }
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        public new void Refresh()
        {
            btnFirst.Enabled = CurrentPage > 1;
            btnPrev.Enabled = CurrentPage > 1;
            btnNext.Enabled = CurrentPage < _TotalPages;
            btnLast.Enabled = CurrentPage < _TotalPages;
            btnJump.Enabled = _TotalPages > 1;

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
            _Reload = _Rows > RowsPerPage;
            RowsPerPage = int.Parse(cbeRows.Text);
            CurrentPage = (int) Math.Ceiling((decimal) (_Index + 1)/RowsPerPage);
            _Reload = _Reload || _Rows > RowsPerPage;
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
            _Reload = true;
            CurrentPage = 1;
            CurrentPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 转到上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            _Reload = true;
            CurrentPage -= 1;
            CurrentPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 转到下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            _Reload = true;
            CurrentPage += 1;
            CurrentPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 转到末页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            _Reload = true;
            CurrentPage = _TotalPages;
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
            if (page < 1 || page > _TotalPages || page == CurrentPage)
            {
                txtPage.EditValue = null;
                return;
            }

            txtPage.Visible = false;
            _Reload = true;
            CurrentPage = page;
            CurrentPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 输入总行数
        /// </summary>
        /// <param name="rows">总行数</param>
        private void SetTotalRows(int rows)
        {
            _Rows = rows;
            _TotalPages = (int) Math.Ceiling((decimal) rows/RowsPerPage);
            CurrentPage = (int) Math.Ceiling((decimal) (_Index + 1)/RowsPerPage);

            var total = _TotalPages == 0 ? 1 : _TotalPages;
            labRows.Text = $" 行/页 | 共 {rows} 行 | 分 {total} 页";
            labRows.Refresh();

            Refresh();
        }
    }
}
