using System.Windows.Forms;
using Insight.Base.BaseForm.Forms;

namespace Insight.Base.MainForm.Views
{
    public partial class LockDialog : BaseDialog
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LockDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 拦截Alt-F4
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | 0x200;
                return cp;
            }
        }
    }
}
