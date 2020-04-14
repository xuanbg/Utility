using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Controls.Nim;
using NIM;
using NIM.Session;

namespace Insight.Utils.Controls
{
    public partial class NimSessions : XtraUserControl
    {
        private int height;

        /// <summary>  
        /// 当消息发送后，通知处理消息
        /// </summary>  
        public event SessionClickHandle sessionClick;

        /// <summary>
        /// 表示将处理当前消息事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SessionClickHandle(object sender, SessionEventArgs e);

        /// <summary>
        /// 用户云信ID
        /// </summary>
        public string myId { private get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public NimSessions()
        {
            InitializeComponent();

            var timer = new System.Timers.Timer {Enabled = true, Interval = 1000 * 60 * 5};
            timer.Start();

            timer.Elapsed += refreshTime;
            SessionAPI.RecentSessionChangedHandler += sessionChanged;
        }

        /// <summary>
        /// 获取会话列表
        /// </summary>
        public void getSessions()
        {
            SessionAPI.QueryAllRecentSession((count, data) =>
            {
                if (data?.SessionList == null) return;

                foreach (var info in data.SessionList)
                {
                    addSession(info);
                }

                Refresh();
            });
        }

        /// <summary>
        /// 处理会话更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sessionChanged(object sender, SessionChangedEventArgs e)
        {
            switch (e.Info.Command)
            {
                case NIMSessionCommand.kNIMSessionCommandAdd:
                    addSession(e.Info);
                    Refresh();

                    break;
                case NIMSessionCommand.kNIMSessionCommandUpdate:
                    updateControl(e.Info);

                    break;
                case NIMSessionCommand.kNIMSessionCommandRemove:
                    removeControl(e.Info.Id);

                    break;
            }
        }

        /// <summary>
        /// 构造会话控件并加入列表
        /// </summary>
        /// <param name="info"></param>
        private void addSession(SessionInfo info)
        {
            var userId = info.Sender == myId ? info.ExtendString : info.Sender;
            var user = NimUtil.getUser(userId);
            var session = new NimSessionInfo
            {
                id = info.Id,
                targetId = userId,
                targetName = user.name,
                target = NimUtil.getHeadImage(user.icon),
                time = info.Timetag / 1000,
                unRead = info.UnreadCount > 0
            };

            switch (info.MsgType)
            {
                case NIMMessageType.kNIMMessageTypeText:
                    session.message = info.Content;

                    break;
                case NIMMessageType.kNIMMessageTypeImage:
                    session.message = "[图片]";

                    break;
                case NIMMessageType.kNIMMessageTypeFile:
                    session.message = "[文件]";

                    break;
                default:
                    session.message = "[未知]";

                    break;
            }

            addControl(session);
        }

        /// <summary>
        /// 构造并添加会话控件到会话列表
        /// </summary>
        /// <param name="session">云信会话信息</param>
        private void addControl(NimSessionInfo session)
        {
            void action()
            {
                var control = new SessionBox
                {
                    headImage = session.target,
                    name = session.targetName,
                    message = session.message,
                    time = session.time,
                    unRead = session.unRead,
                    Name = session.id,
                    Location = new Point(0, height),
                    Dock = DockStyle.Top,
                };
                control.click += (sender, args) => sessionClick?.Invoke(this, new SessionEventArgs(session.targetId));
                pceMain.Controls.Add(control);

                height = height + control.Size.Height;
                if (height > Height) pceMain.Dock = DockStyle.Top;

                pceMain.Height = height;
                sceMain.ScrollControlIntoView(control);
            }

            Invoke((Action)action);
        }

        /// <summary>
        /// 更新会话记录
        /// </summary>
        /// <param name="info"></param>
        private void updateControl(SessionInfo info)
        {
            void action()
            {
                var control = (SessionBox) pceMain.Controls[info.Id];
                control.unRead = true;
                switch (info.MsgType)
                {
                    case NIMMessageType.kNIMMessageTypeText:
                        control.message = info.Content;

                        break;
                    case NIMMessageType.kNIMMessageTypeImage:
                        control.message = "[图片]";

                        break;
                    case NIMMessageType.kNIMMessageTypeFile:
                        control.message = "[文件]";

                        break;
                    default:
                        control.message = "[未知]";

                        break;
                }

                control.time = info.Timetag / 1000;
            }

            Invoke((Action) action);
        }

        /// <summary>
        /// 移除控件
        /// </summary>
        /// <param name="id"></param>
        private void removeControl(string id)
        {
            void action()
            {
                var control = pceMain.Controls[id];
                pceMain.Controls.Remove(control);
                height = height + control.Size.Height;
                if (height <= Height) pceMain.Dock = DockStyle.Fill;

                pceMain.Height = height;
            }

            Invoke((Action) action);
        }

        /// <summary>
        /// 刷新会话时间
        /// </summary>
        private void refreshTime(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (SessionBox control in pceMain.Controls)
            {
                control.refreshTime();
            }
        }
    }

    /// <summary>
    /// 消息事件参数类
    /// </summary>
    public class SessionEventArgs : EventArgs
    {
        /// <summary>
        /// 云信用户ID
        /// </summary>
        public string targetId { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="targetId">云信用户ID</param>
        public SessionEventArgs(string targetId)
        {
            this.targetId = targetId;
        }
    }

    public class NimSessionInfo
    {
        /// <summary>
        /// 会话ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 对方云信ID
        /// </summary>
        public string targetId { get; set; }

        /// <summary>
        /// 对方昵称
        /// </summary>
        public string targetName { get; set; }

        /// <summary>
        /// 对方头像
        /// </summary>
        public Image target { get; set; }

        /// <summary>
        /// 最后一条消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 最后一条消息发送时间戳
        /// </summary>
        public long time { get; set; }

        /// <summary>
        /// 是否未读
        /// </summary>
        public bool unRead { get; set; }
    }
}
