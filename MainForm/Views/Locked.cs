using System.Windows.Forms;
using Insight.Utils.BaseForms;

namespace Insight.Utils.MainForm.Views
{
    public partial class Locked : BaseDialog
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Locked()
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
