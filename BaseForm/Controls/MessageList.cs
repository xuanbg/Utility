using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Common;

namespace Insight.Utils.Controls
{
    public partial class MessageList : XtraUserControl
    {
        private static readonly Image defaultImage = Util.getImage("icons/head.png");
        private List<NimMessage> messages = new List<NimMessage>();
        private DateTime messageTime;
        private int height;

        /// <summary>
        /// 己方头像
        /// </summary>
        public Image me { private get; set; } = defaultImage;

        /// <summary>
        /// 对方头像
        /// </summary>
        public Image target { private get; set; } = defaultImage;

        /// <summary>
        /// 构造方法
        /// </summary>
        public MessageList()
        {
            InitializeComponent();

            messageTime = DateTime.Now;
        }

        /// <summary>
        /// 初始化历史消息
        /// </summary>
        /// <param name="messages">消息列表</param>
        public void initHistory(List<NimMessage> messages)
        {
            this.messages = messages;
            foreach (var message in messages)
            {
                addControl(message);
            }

            Refresh();
        }

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns>云信IM点对点消息</returns>
        public NimMessage getMessage(string id)
        {
            return messages.Find(i => i.id == id);
        }

        /// <summary>
        /// 往消息列表中增加一条消息
        /// </summary>
        /// <param name="message">云信IM点对点消息</param>
        public void addMessage(NimMessage message)
        {
            messages.Add(message);
            if (message.id != null) return;

            addControl(message);
        }

        /// <summary>
        /// 往消息列表中增加一条消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="msgId">云信消息ID</param>
        /// <param name="timeTag">消息发送时间戳</param>
        public void addMessage(string id, long msgId, long timeTag)
        {
            var message = getMessage(id);
            message.msgid = msgId;
            message.timetag = timeTag;

            addControl(message);
        }

        /// <summary>
        /// 构造并添加消息控件到消息窗口
        /// </summary>
        /// <param name="message">云信IM点对点消息</param>
        private void addControl(NimMessage message)
        {
            var ts = DateTime.Now - messageTime;
            if (ts.Minutes > 10)
            {
                messageTime = DateTime.Now;
            }

            var head = message.direction == 0 ? me : target;
            var control = new MessageControl
            {
                Name = message.id,
                width = Width - 20,
                message = message,
                headImage = head,
                Location = new Point(0, height),
                Padding = new Padding(0),
                MinimumSize = new Size(Width - 20, 70),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pceList.Controls.Add(control);

            height = height + control.Size.Height;
            pceList.Height = height;
            sceMessage.VerticalScroll.Value = height;

            Refresh();
        }
    }
}
