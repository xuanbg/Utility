using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Insight.Base.BaseForm.BaseForms;
using Insight.Utils.Common;
using NIM.Messagelog;
using NIM.Session;

namespace Insight.Utils.NetEaseIM.Controls
{
    public partial class HistoryDialog : BaseDialog
    {
        private long endTime = Util.getTimeStamp(DateTime.Now) * 1000;
        private readonly string targetId;
        private readonly Image targetHead;
        private DateTime messageTime;
        private int height;
        private string playingId;
        private bool playing;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="user">对方云信名片</param>
        public HistoryDialog(NimUser user)
        {
            InitializeComponent();

            sbeNext.Click += (sender, args) => getHistory();
            Closed += (sender, args) => Dispose();
            close.Click += (sender, args) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            targetId = user.accid;
            targetHead = Util.getImageFromUrl(user.icon);
            getHistory();
        }

        /// <summary>
        /// 查询历史消息
        /// </summary>
        private void getHistory()
        {
            MessagelogAPI.QueryMsglogOnline(targetId, NIMSessionType.kNIMSessionTypeP2P, 6, 0, endTime, 0, false, true, true, (code, accountId, sType, result) =>
            {
                void action()
                {
                    var list = result.MsglogCollection;
                    if (list == null || list.Length == 0) return;

                    foreach (var msg in list.OrderByDescending(i => i.TimeStamp))
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

                        addMessage(message);
                    }

                    if (list.Length < 6)
                    {
                        sbeNext.Enabled = false;
                        sbeNext.Text = "已到达终点";
                        return;
                    }

                    endTime = list.Last().TimeStamp;
                }

                Invoke((Action)action);
            });
        }

        /// <summary>
        /// 构造并添加消息控件到消息窗口
        /// </summary>
        /// <param name="message">云信IM点对点消息</param>
        public void addMessage(NimMessage message)
        {
            var control = new MessageBox
            {
                width = pceHistory.Width,
                message = message,
                targetHead = targetHead,
                Dock = DockStyle.Top
            };
            control.play += (sender, args) => play(args.id);
            control.stop += (sender, args) => playing = false;
            pceHistory.Controls.Add(control);
            height = height + control.Size.Height;

            var time = Util.getDateTime(message.timetag);
            if (messageTime == DateTime.MinValue)
            {
                messageTime = time;
                addTime(time);
            }

            var ts = messageTime - time;
            if (ts.TotalMinutes > 15) addTime(time);

            pceHistory.Height = height;
        }

        /// <summary>
        /// 构造并添加时间控件到消息窗口
        /// </summary>
        /// <param name="time"></param>
        private void addTime(DateTime time)
        {
            messageTime = time;
            var control = new TimeLabel
            {
                time = time,
                Name = Util.newId("N"),
                Dock = DockStyle.Top
            };

            pceHistory.Controls.Add(control);

            height = height + control.Size.Height;
            pceHistory.Height = height;
        }

        /// <summary>
        /// 停止全部语音播放
        /// </summary>
        /// <param name="id">消息ID</param>
        private void play(string id)
        {
            var playingBox = pceHistory.Controls[playingId] as MessageBox;
            playingBox?.stopAudio();

            var playBox = (MessageBox) pceHistory.Controls[id];
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
