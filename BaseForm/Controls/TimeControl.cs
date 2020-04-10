using System;
using DevExpress.XtraEditors;

namespace Insight.Utils.Controls
{
    public partial class TimeControl : XtraUserControl
    {
        public TimeControl()
        {
            InitializeComponent();

            labTime.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
