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
        private int _Handle;
        private int _TotalPages = 1;
        private Collection<string> _SelectItems = new Collection<string> { "20", "40", "60", "80", "100" };

        /// <summary>  
        /// 当前页发生改变
        /// </summary>  
        public event EventHandler CurrentPageChanged;

        /// <summary>  
        /// 每页行数发生改变
        /// </summary>  
        public event EventHandler RowsPerPageChanged;

        /// <summary>  
        /// 每页行数发生改变
        /// </summary>  
        public event RowsChangedHandle TotalRowsChanged;

        /// <summary>
        /// 表示将处理总行数变化事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void RowsChangedHandle(object sender, RowsChangedEventArgs e);

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
            set
            {
                _Rows = value;
                Refresh();
            }
        }

        /// <summary>
        /// 当前选中行Handle
        /// </summary>
        public int FocusedRowHandle
        {
            set
            {
                _Index = RowsPerPage * (CurrentPage - 1) + value;
                if (_Index > 0) _Handle = _Index;
            }
        }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int RowsPerPage { get; private set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; private set; } = 1;

        /// <summary>
        /// 是否需要重新加载列表
        /// </summary>
        public bool NeedReload => _Reload;

        /// <summary>
        /// 构造方法
        /// </summary>
        public PageControl()
        {
            InitializeComponent();
            RowsPerPage = int.Parse(_SelectItems[0]);
        }

        /// <summary>
        /// 增加列表成员
        /// </summary>
        /// <param name="count">增加数量，默认1个</param>
        public void AddItems(int count = 1)
        {
            _Rows += count;
            Refresh();

            var index = _Rows - RowsPerPage*(CurrentPage - 1) - 1;
            TotalRowsChanged?.Invoke(this, new RowsChangedEventArgs(index));
        }

        /// <summary>
        /// 减少列表成员
        /// </summary>
        /// <param name="count">减少数量，默认1个</param>
        public void RemoveItems(int count = 1)
        {
            _Rows -= count;
            Refresh();

            if (_Handle >= _Rows) _Handle = _Rows - 1;

            var index = _Handle - RowsPerPage*(CurrentPage - 1);
            TotalRowsChanged?.Invoke(this, new RowsChangedEventArgs(index));
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        private new void Refresh()
        {
            _TotalPages = (int)Math.Ceiling((decimal)_Rows / RowsPerPage);
            CurrentPage = (int)Math.Ceiling((decimal)(_Index + 1) / RowsPerPage);

            var total = _TotalPages == 0 ? 1 : _TotalPages;
            labRows.Text = $" 行/页 | 共 {_Rows} 行 | 分 {total} 页";
            labRows.Refresh();

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
        /// 切换每页行数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageRowsChanged(object sender, EventArgs e)
        {
            _Reload = _Rows > RowsPerPage;
            RowsPerPage = int.Parse(cbeRows.Text);
            CurrentPage = (int)Math.Ceiling((decimal)(_Index + 1) / RowsPerPage);
            _Reload = _Reload || _Rows > RowsPerPage;
            Refresh();

            RowsPerPageChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 转到首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void First_Click(object sender, EventArgs e)
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
        private void Prev_Click(object sender, EventArgs e)
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
        private void Next_Click(object sender, EventArgs e)
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
        private void Last_Click(object sender, EventArgs e)
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
    }

    public class RowsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// RowHandle
        /// </summary>
        public int RowHandle { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="handel">RowHandle</param>
        public RowsChangedEventArgs(int handel)
        {
            RowHandle = handel;
        }
    }
}
