using System;
using System.Drawing;
using DevExpress.XtraEditors;
using Insight.Utils.Common;

namespace Insight.Utils.Controls.Nim
{
    public partial class SessionBox : XtraUserControl
    {
        private DateTime _time;

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
        /// 显示消息时间
        /// </summary>
        public DateTime time
        {
            set
            {
                _time = value;
                refreshTime();
            }
        }

        /// <summary>
        /// 显示消息内容
        /// </summary>
        public NimMessage message
        {
            set
            {
                switch (value.type)
                {
                    case 0:
                        labMessage.Text = Util.convertTo<TextMessage>(value.body).msg;
                        break;
                    case 1:
                        labMessage.Text = "[图片]";
                        break;
                    case 6:
                        labMessage.Text = "[文件]";
                        break;
                    default:
                        labMessage.Text = "[未知]";
                        break;
                }
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
