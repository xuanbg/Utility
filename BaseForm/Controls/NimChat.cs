using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Common;
using Insight.Utils.Controls.Nim;
using NIM;
using NIM.Messagelog;
using NIM.Session;
using NIM.User;

namespace Insight.Utils.Controls
{
    public partial class NimChat : XtraUserControl
    {
        private string messageId;

        /// <summary>
        /// 发送者云信ID
        /// </summary>
        public string myId { private get; set; }

        /// <summary>
        /// 发送者头像
        /// </summary>
        public Image myHead { private get; set; }

        /// <summary>
        /// 接收者云信ID
        /// </summary>
        public string targetId { private get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public NimChat()
        {
            InitializeComponent();

            sbeFile.Click += (sender, args) => openFile(0);
            sbeImage.Click += (sender, args) => openFile(1);
            sbeSend.Click += (sender, args) => sendTextMessage(mmeInput.Text);
            mmeInput.KeyDown += (sender, args) =>
            {
                if (!args.Control || args.KeyCode != Keys.Enter) return;

                sendTextMessage(mmeInput.Text);
            };
            mmeInput.KeyUp += (sender, args) =>
            {
                if (!args.Control || args.KeyCode != Keys.Enter) return;

                mmeInput.EditValue = null;
            };

            TalkAPI.OnSendMessageCompleted += sendMessageResultHandler;
            TalkAPI.OnReceiveMessageHandler += receiveMessage;

            mmeInput.Focus();
        }

        /// <summary>
        /// 初始聊天窗口
        /// </summary>
        public void init()
        {
            UserAPI.GetUserNameCard(new List<string> { targetId }, ret =>
            {
                if (ret == null || !ret.Any()) return;

                var headUrl = ret[0].IconUrl;
                if (!string.IsNullOrEmpty(headUrl))
                {
                    mlcMessage.target = NimUtil.getImage(headUrl);
                }
            });

            if (myHead == null)
            {
                UserAPI.GetUserNameCard(new List<string> { myId }, ret =>
                {
                    if (ret == null || !ret.Any()) return;

                    var headUrl = ret[0].IconUrl;
                    if (string.IsNullOrEmpty(headUrl)) return;

                    myHead = NimUtil.getImage(headUrl);
                    mlcMessage.me = myHead;
                });
            }
            else
            {
                mlcMessage.me = myHead;
            }

            getHistory();
        }

        /// <summary>
        /// 获取历史消息
        /// </summary>
        private void getHistory()
        {
            MessagelogAPI.QueryMsglogLocally(targetId, NIMSessionType.kNIMSessionTypeP2P, 20, 0, (code, accountId, sType, result) =>
            {
                foreach (var msg in result.MsglogCollection.OrderBy(i => i.TimeStamp))
                {
                    addMessage(msg);
                }
            });
        }

        /// <summary>
        /// 检查消息发送结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void sendMessageResultHandler(object sender, MessageArcEventArgs args)
        {
            if (args.ArcInfo.TalkId != targetId) return;

            void action()
            {
                if (args.ArcInfo.Response != ResponseCode.kNIMResSuccess)
                {
                    Messages.showError("发送失败");
                    return;
                }

                mmeInput.EditValue = null;
                mmeInput.Focus();
            }

            if (IsDisposed || !(Parent?.IsHandleCreated ?? false)) return;

            Invoke((Action) action);
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
        public void addMessage(NIMIMMessage msg)
        {
            var message = new NimMessage
            {
                id = msg.TalkID,
                msgid = msg.ServerMsgId,
                from = msg.SenderID,
                to = msg.ReceiverID,
                type = msg.MessageType.GetHashCode(),
                body = NimUtil.getMsg(msg),
                direction = msg.SenderID == myId ? 0 : 1,
                timetag = msg.TimeStamp / 1000
            };

            void action() => mlcMessage.addMessage(message);

            if (IsDisposed || !(Parent?.IsHandleCreated ?? false)) return;

            Invoke((Action)action);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="type">文件类型</param>
        private void openFile(int type)
        {
            ofdMessage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            ofdMessage.Filter = type == 1 ? "图片|*.jpg;*.jpeg;*.png" : "All files|*.*";
            if (ofdMessage.ShowDialog() != DialogResult.OK) return;

            var fileName = ofdMessage.FileName;
            if (type == 0)
            {
                sendFile(fileName);
            }
            else
            {
                sendPicture(fileName);
            }
        }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="msg">文字内容</param>
        private void sendTextMessage(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;

            var body = new TextMessage { msg = msg };
            sendMessage(0, body);

            var message = new NIMTextMessage
            {
                SessionType = NIMSessionType.kNIMSessionTypeP2P,
                ReceiverID = targetId,
                TextContent = msg,
            };
            TalkAPI.SendMessage(message);
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="fileName"></param>
        private void sendFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            var ext = fileName.Substring(fileName.LastIndexOf('.') + 1);
            var message = new NIMFileMessage
            {
                SessionType = NIMSessionType.kNIMSessionTypeP2P,
                ReceiverID = targetId,
                LocalFilePath = fileName,
                FileAttachment = new NIMMessageAttachment {DisplayName = fileName, FileExtension = ext}
            };

            var body = new FileMessage {attach = Util.serialize(new Attach {name = fileName})};
            var id = sendMessage(6, body);
            TalkAPI.SendMessage(message, (uploaded, total, obj) =>
            {
                void action() => mlcMessage.setPosition(id, (int) (uploaded / total));

                if (IsDisposed || !(Parent?.IsHandleCreated ?? false)) return;

                Invoke((Action)action);
            });
        }

        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="fileName"></param>
        private void sendPicture(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            var ext = fileName.Substring(fileName.LastIndexOf('.') + 1);
            var message = new NIMImageMessage
            {
                SessionType = NIMSessionType.kNIMSessionTypeP2P,
                ReceiverID = targetId,
                ImageAttachment = new NIMImageAttachment { DisplayName = fileName, FileExtension = ext },
                LocalFilePath = fileName,
            };
            var body = new FileMessage{image = Util.getImage(fileName)};
            message.ImageAttachment.Height = body.image.Height;
            message.ImageAttachment.Width = body.image.Width;

            sendMessage(1, body);
            TalkAPI.SendMessage(message);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="body">消息体</param>
        private string sendMessage(int type, object body)
        {
            messageId = Util.newId("N");
            var message = new NimMessage
            {
                id = messageId,
                from = myId,
                to = targetId,
                type = type,
                direction = 0,
                body = body,
                timetag = Util.getTimeStamp(DateTime.Now)
            };

            mlcMessage.addMessage(message);
            return messageId;
        }
    }
}