using System;
using System.Drawing;
using DevExpress.XtraEditors;
using Insight.Utils.Common;

namespace Insight.Utils.Controls.Nim
{
    public partial class SessionBox : XtraUserControl
    {
        private DateTime _time;

        public new delegate void Click(object sender, EventArgs args);

        public event Click click;

        /// <summary>
        /// 显示头像
        /// </summary>
        public Image headImage
        {
            set
            {
                if (value == null) return;

                picTarget.Image = value;
            }
        }

        /// <summary>
        /// 显示昵称
        /// </summary>
        public string name
        {
            set => labName.Text = value;
        }

        /// <summary>
        /// 显示消息内容
        /// </summary>
        public string message
        {
            set => labMessage.Text = value;
        }

        /// <summary>
        /// 显示最后消息发送时间
        /// </summary>
        public long time
        {
            set
            {
                _time = Util.getDateTime(value);
                refreshTime();
            }
        }

        /// <summary>
        /// 设置未读
        /// </summary>
        public bool unRead
        {
            set => peeUnread.Visible = value;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public SessionBox()
        {
            InitializeComponent();
            pceSession.Click += (sender, args) => click?.Invoke(sender, args);
            picTarget.Click += (sender, args) => click?.Invoke(sender, args);
            labName.Click += (sender, args) => click?.Invoke(sender, args);
            labTime.Click += (sender, args) => click?.Invoke(sender, args);
            labMessage.Click += (sender, args) => click?.Invoke(sender, args);
            peeUnread.Click += (sender, args) => click?.Invoke(sender, args);
        }

        /// <summary>
        /// 刷新显示时间
        /// </summary>
        public void refreshTime()
        {
            var ts = DateTime.Now - _time;
            labTime.Text = _time.ToString(ts.TotalHours > 12 ? "yy-MM-dd" : "hh:mm:ss");
        }
    }
}
