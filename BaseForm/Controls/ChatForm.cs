using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Common;

namespace Insight.Utils.Controls
{
    public partial class ChatForm : XtraUserControl
    {
        /// <summary>  
        /// 当前焦点行发生改变，通知修改焦点行
        /// </summary>  
        public event MessageSendHandle messageSend;

        /// <summary>
        /// 表示将处理当前焦点行发生改变事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MessageSendHandle(object sender, MessageEventArgs e);

        /// <summary>
        /// 发送者云信ID
        /// </summary>
        public string from { private get; set; }

        /// <summary>
        /// 接收者云信ID
        /// </summary>
        public string to { private get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ChatForm()
        {
            InitializeComponent();

            mmeInput.Focus();
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
        }

        /// <summary>
        /// 将接收到的消息添加到消息列表
        /// </summary>
        /// <param name="message"></param>
        public void addMessage(NimMessage message)
        {
            message.direction = 1;

            mlcMessage.addMessage(message);
        }

        /// <summary>
        /// 消息发送成功后更新数据
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="msgId">云信消息ID</param>
        /// <param name="timeTag">消息发送时间戳</param>
        public void messageSent(string id, long msgId, long timeTag)
        {
            mlcMessage.addMessage(id, msgId, timeTag);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg">文字内容</param>
        private void sendTextMessage(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;

            mmeInput.EditValue = null;
            mmeInput.Focus();

            var message = new NimMessage
            {
                id = Util.newId("N"),
                from = from,
                to = to,
                type = 0,
                direction = 0,
                body = new TextMessage {msg = msg}
            };
            mlcMessage.addMessage(message);

            messageSend?.Invoke(this, new MessageEventArgs(message));
        }
    }

    /// <summary>
    /// 焦点行改变事件参数类
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Row handle
        /// </summary>
        public NimMessage message { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">云信IM点对点消息</param>
        public MessageEventArgs(NimMessage message)
        {
            this.message = message;
        }
    }

    public class NimUser
    {
        /// <summary>
        /// 云信用户ID
        /// </summary>
        public string accid { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int gender { get; set; }
    }

}