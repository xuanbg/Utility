using System;
using DevExpress.XtraEditors;

namespace Insight.Utils.Controls.Nim
{
    public partial class TimeLabel : XtraUserControl
    {
        /// <summary>
        /// 显示消息时间
        /// </summary>
        public DateTime time
        {
            set
            {
                var ts = DateTime.Now - value;
                if (ts.TotalDays > 365)
                {
                    labTime.Text = value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else if (ts.TotalDays > 30)
                {
                    labTime.Text = value.ToString("MM-dd HH:mm:ss");
                }
                else
                {
                    labTime.Text = value.ToString("dd HH:mm:ss");
                }
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public TimeLabel()
        {
            InitializeComponent();
        }
    }
}
