using System;
using DevExpress.XtraEditors;

namespace Insight.Utils.Controls
{
    public partial class TimeControl : XtraUserControl
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime time
        {
            set
            {
                var ts = DateTime.Now - value;
                if (ts.TotalDays > 365)
                {
                    labTime.Text = value.ToString("yyyy-MM-dd hh:mm:ss");
                }
                else if (ts.TotalDays > 30)
                {
                    labTime.Text = value.ToString("MM-dd hh:mm:ss");
                }
                else if (ts.TotalDays > 1)
                {
                    labTime.Text = value.ToString("dd hh:mm:ss");
                }
                else
                {
                    labTime.Text = value.ToString("hh:mm:ss");
                }
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public TimeControl()
        {
            InitializeComponent();
        }
    }
}
