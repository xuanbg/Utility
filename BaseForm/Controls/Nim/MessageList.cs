using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Common;

namespace Insight.Utils.Controls.Nim
{
    public partial class MessageList : XtraUserControl
    {
        private DateTime messageTime;
        private int height;

        /// <summary>
        /// 己方头像
        /// </summary>
        public Image me { private get; set; }

        /// <summary>
        /// 对方头像
        /// </summary>
        public Image target { private get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public MessageList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造并添加消息控件到消息窗口
        /// </summary>
        /// <param name="message">云信IM点对点消息</param>
        public void addMessage(NimMessage message)
        {
            var time = Util.getDateTime(message.timetag);
            if (messageTime == DateTime.MinValue)
            {
                messageTime = time;
                addTime(time);
            }

            var ts = time - messageTime;
            if (ts.TotalMinutes > 15) addTime(time);

            var head = message.direction == 0 ? me : target;
            var control = new MessageBox
            {
                width = pceList.Width,
                message = message,
                headImage = head,
                Name = message.id,
                Location = new Point(0, height),
                Padding = new Padding(0),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            pceList.Controls.Add(control);

            height = height + control.Size.Height;
            pceList.Height = height;
            sceMessage.ScrollControlIntoView(control);
        }

        /// <summary>
        /// 构造并添加时间控件到消息窗口
        /// </summary>
        /// <param name="time"></param>
        private void addTime(DateTime time)
        {
            var timeControl = new TimeLabel
            {
                time = time,
                Name = Util.newId("N"),
                Size = new Size(pceList.Width, 20),
                Location = new Point(0, height),
                Padding = new Padding(0),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            pceList.Controls.Add(timeControl);

            height = height + timeControl.Size.Height;
            pceList.Height = height;
            messageTime = time;
        }
    }
}
