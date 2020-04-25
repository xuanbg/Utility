using System;
using System.Drawing;
using DevExpress.XtraEditors;
using Insight.Utils.Common;

namespace Insight.Utils.NetEaseIM.Controls
{
    public partial class SessionBox : XtraUserControl
    {
        private DateTime _time;

        /// <summary>
        /// 控件点击事件的委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public new delegate void Click(object sender, EventArgs args);

        /// <summary>
        /// 控件双击事件的委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public new delegate void DoubleClick(object sender, EventArgs args);

        /// <summary>
        /// 控件点击事件
        /// </summary>
        public event Click click;

        /// <summary>
        /// 控件双击事件
        /// </summary>
        public event DoubleClick doubleClick;

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

            // 控件点击
            pceSession.Click += onClick;
            picTarget.Click += onClick;
            labName.Click += onClick;
            labTime.Click += onClick;
            labMessage.Click += onClick;
            peeUnread.Click += onClick;

            // 控件双击
            pceSession.DoubleClick += onDoubleClick;
            picTarget.DoubleClick += onDoubleClick;
            labName.DoubleClick += onDoubleClick;
            labTime.DoubleClick += onDoubleClick;
            labMessage.DoubleClick += onDoubleClick;
            peeUnread.DoubleClick += onDoubleClick;
        }

        /// <summary>
        /// 刷新显示时间
        /// </summary>
        public void refreshTime()
        {
            var ts = DateTime.Now - _time;
            labTime.Text = _time.ToString(ts.TotalHours > 12 ? "yy-MM-dd" : "HH:mm:ss");
        }

        /// <summary>
        /// 单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void onClick(object sender, EventArgs args)
        {
            click?.Invoke(sender, args);
        }

        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void onDoubleClick(object sender, EventArgs args)
        {
            doubleClick?.Invoke(sender, args);
        }
    }
}
