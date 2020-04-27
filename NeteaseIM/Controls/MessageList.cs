using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Common;
using NIM;
using NIM.Messagelog;
using NIM.Session;

namespace Insight.Utils.NetEaseIM.Controls
{
    public partial class MessageList : XtraUserControl
    {
        private readonly List<NimMessage> messages = new List<NimMessage>();
        private string targetId;
        private Image targetHead;
        private string playingId;
        private bool playing;

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

            showHistory();
        }

        /// <summary>
        /// 加载历史消息
        /// </summary>
        public void showHistory()
        {
            MessagelogAPI.QueryMsglogLocally(targetId, NIMSessionType.kNIMSessionTypeP2P, 20, 0, (code, accountId, sType, result) =>
            {
                var list = result.MsglogCollection;
                if (list == null || list.Length == 0) return;

                messages.Clear();
                foreach (var msg in list.OrderBy(i => i.TimeStamp))
                {
                    addMessage(msg);
                }

                void action() => refresh();

                while (!(Parent?.IsHandleCreated ?? false)) Thread.Sleep(100);

                Invoke((Action)action);
            });
        }

        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="message">消息对象</param>
        public void addMessage(NimMessage message)
        {
            messages.Add(message);

            void action() => refresh();

            while (!(Parent?.IsHandleCreated ?? false)) Thread.Sleep(100);

            Invoke((Action)action);
        }

        /// <summary>
        /// 显示进度
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="position">进度</param>
        public void setPosition(string id, int position)
        {
            var control = (MessageBox)sceMessage.Controls[0].Controls[id];
            control.position = position;
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

            var message = args.Message.MessageContent;
            if (message == null) return;

            addMessage(message);

            void action() => refresh();

            while (!(Parent?.IsHandleCreated ?? false)) Thread.Sleep(100);

            Invoke((Action)action);
        }

        /// <summary>
        /// 将接收到的消息添加到消息列表
        /// </summary>
        /// <param name="msg">云信消息数据</param>
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
            messages.Add(message);
        }

        /// <summary>
        /// 刷新会话列表
        /// </summary>
        private void refresh()
        {
            var height = 0;
            var hide = sceMessage.Controls[1];
            var messageTime = DateTime.MinValue;
            MessageBox box = null;
            messages.OrderBy(i => i.timetag).ToList().ForEach(i =>
            {
                var time = Util.getDateTime(i.timetag);
                var ts = time - messageTime;
                if (ts.TotalMinutes > 15)
                {
                    messageTime = time;
                    var timeControl = new TimeLabel
                    {
                        time = time,
                        Name = Util.newId("N"),
                        Size = new Size(hide.Width, 20),
                        Location = new Point(0, height),
                        Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
                    };
                    hide.Controls.Add(timeControl);
                    height = height + timeControl.Height;
                }

                box = new MessageBox
                {
                    width = hide.Width,
                    message = i,
                    targetHead = targetHead,
                    Location = new Point(0, height),
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
                };
                box.play += (sender, args) => stopPlaying(args.id);
                box.stop += (sender, args) => playing = false;
                hide.Controls.Add(box);
                height = height + box.Height;
            });

            hide.Dock = height > Height ? DockStyle.Top : DockStyle.Fill;
            hide.Height = height;
            hide.Visible = true;
            sceMessage.ScrollControlIntoView(box);

            var show = sceMessage.Controls[0];
            show.Visible = false;
            show.SendToBack();
            show.Controls.Clear();
        }

        /// <summary>
        /// 停止全部语音播放
        /// </summary>
        /// <param name="id">消息ID</param>
        private void stopPlaying(string id)
        {
            var box = sceMessage.Controls[0].Controls[playingId] as MessageBox;
            box?.stopAudio();

            var playBox = (MessageBox)sceMessage.Controls[0].Controls[id];
            Task.Run(() =>
            {
                while (playing) Thread.Sleep(200);

                playingId = id;
                playing = true;
                playBox.playAudio();
            });
        }
    }
}
