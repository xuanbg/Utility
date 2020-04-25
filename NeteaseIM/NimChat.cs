using System;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Common;
using Insight.Utils.NetEaseIM.Controls;
using NIM;
using NIM.Session;

namespace Insight.Utils.NetEaseIM
{
    public partial class NimChat : XtraUserControl
    {
        private string myId;
        private NimUser target;

        /// <summary>
        /// 构造方法
        /// </summary>
        public NimChat()
        {
            InitializeComponent();

            sbeFile.Click += (sender, args) => openFile(0);
            sbeImage.Click += (sender, args) => openFile(1);
            sbeHistroy.Click += (sender, args) => showHistory();
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
        }

        /// <summary>
        /// 初始化消息列表
        /// </summary>
        /// <param name="myId">己方云信ID</param>
        /// <param name="user">对方云信名片</param>
        public void init(string myId, NimUser user)
        {
            this.myId = myId;
            target = user;

            mlcMessage.init(user);
            mmeInput.Select();
        }

        /// <summary>
        /// 显示最新消息
        /// </summary>
        public void showNewMessage()
        {
            mlcMessage.scrollToView();
        }

        /// <summary>
        /// 检查消息发送结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void sendMessageResultHandler(object sender, MessageArcEventArgs args)
        {
            if (args.ArcInfo.TalkId != target?.accid) return;

            void action()
            {
                if (args.ArcInfo.Response != ResponseCode.kNIMResSuccess)
                {
                    Messages.showError("发送失败");
                    return;
                }

                mmeInput.EditValue = null;
                mmeInput.Select();
            }

            while (!(Parent?.IsHandleCreated ?? false)) Thread.Sleep(100);

            Invoke((Action) action);
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
                ReceiverID = target.accid,
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
                ReceiverID = target.accid,
                LocalFilePath = fileName,
                FileAttachment = new NIMMessageAttachment {DisplayName = fileName, FileExtension = ext}
            };

            var body = new FileMessage {attach = Util.serialize(new Attach {name = fileName})};
            var id = sendMessage(6, body);
            TalkAPI.SendMessage(message, (uploaded, total, obj) =>
            {
                void action()
                {
                    mlcMessage.setPosition(id, (int) (100 * uploaded / total));
                    if (uploaded == total) Messages.showMessage("文件上传完成");
                }

                while (!(Parent?.IsHandleCreated ?? false)) Thread.Sleep(100);

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
                ReceiverID = target.accid,
                ImageAttachment = new NIMImageAttachment { DisplayName = fileName, FileExtension = ext },
                LocalFilePath = fileName,
            };
            var body = new FileMessage{image = Util.getImageFromFile(fileName)};
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
            var id = Util.newId("N");
            var message = new NimMessage
            {
                id = id,
                from = myId,
                to = target.accid,
                type = type,
                direction = 0,
                body = body,
                timetag = Util.getTimeStamp(DateTime.Now)
            };
            mlcMessage.addMessage(message);

            return id;
        }

        /// <summary>
        /// 查看历史消息
        /// </summary>
        private void showHistory()
        {
            var dialog = new HistoryDialog(target);
            dialog.ShowDialog();
        }
    }
}