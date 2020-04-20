using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Common;
using NIM;
using NIM.Messagelog;
using NIM.Session;

namespace Insight.Utils.Controls.Nim
{
    public partial class MessageList : XtraUserControl
    {
        private string targetId;
        private string messageId;
        private DateTime messageTime;
        private int height;
        private Image targetHead;

        /// <summary>
        /// 构造方法
        /// </summary>
        public MessageList()
        {
            InitializeComponent();

            TalkAPI.OnReceiveMessageHandler += receiveMessage;
        }

        /// <summary>
        /// 初始化消息列表
        /// </summary>
        /// <param name="user">对方云信名片</param>
        public void init(NimUser user)
        {
            targetId = user.accid;
            targetHead = Util.getImageFromUrl(user.icon);

            // 加载历史消息
            pceList.Controls.Clear();
            MessagelogAPI.QueryMsglogLocally(targetId, NIMSessionType.kNIMSessionTypeP2P, 20, 0, (code, accountId, sType, result) =>
            {
                foreach (var msg in result.MsglogCollection.OrderBy(i => i.TimeStamp))
                {
                    addMessage(msg);
                }
            });
        }

        /// <summary>
        /// 滚动窗口到最新消息
        /// </summary>
        public void scrollToView()
        {
            var control = pceList.Controls[messageId];
            sceMessage.ScrollControlIntoView(control);
        }

        /// <summary>
        /// 显示进度
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="position">进度</param>
        public void setPosition(string id, int position)
        {
            var control = (MessageBox)pceList.Controls[id];
            control.position = position;
        }

        /// <summary>
        /// 构造并添加消息控件到消息窗口
        /// </summary>
        /// <param name="message">云信IM点对点消息</param>
        public void addMessage(NimMessage message)
        {
            messageId = message.id;

            var time = Util.getDateTime(message.timetag);
            if (messageTime == DateTime.MinValue)
            {
                messageTime = time;
                addTime(time);
            }

            var ts = time - messageTime;
            if (ts.TotalMinutes > 15) addTime(time);

            var control = new MessageBox
            {
                width = pceList.Width,
                message = message,
                targetHead = targetHead,
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
        /// 接收消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void receiveMessage(object sender, NIMReceiveMessageEventArgs args)
        {
            var type = args.Message.MessageContent.SessionType;
            if (type == NIMSessionType.kNIMSessionTypeP2P && args.Message.MessageContent.SenderID != targetId) return;

            addMessage(args.Message.MessageContent);
        }

        /// <summary>
        /// 将接收到的消息添加到消息列表
        /// </summary>
        /// <param name="msg"></param>
        private void addMessage(NIMIMMessage msg)
        {
            var message = new NimMessage
            {
                id = msg.ClientMsgID,
                msgid = msg.ServerMsgId,
                from = msg.SenderID,
                to = msg.ReceiverID,
                type = msg.MessageType.GetHashCode(),
                body = NimUtil.getMsg(msg),
                direction = msg.SenderID == targetId ? 1 : 0,
                timetag = msg.TimeStamp / 1000
            };

            void action() => addMessage(message);

            while (!(Parent?.IsHandleCreated ?? false)) Thread.Sleep(100);

            Invoke((Action)action);
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
