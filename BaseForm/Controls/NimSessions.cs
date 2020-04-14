using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Controls.Nim;
using NIM;
using NIM.Messagelog;
using NIM.Session;

namespace Insight.Utils.Controls
{
    public partial class NimSessions : XtraUserControl
    {
        private readonly List<NimSessionInfo> sessions = new List<NimSessionInfo>();
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
        public delegate void SessionClickHandle(object sender, EventArgs e);

        /// <summary>
        /// 当前会话
        /// </summary>
        public NimSessionInfo info;

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

                var index = 0;
                var tasks = new Task[data.Count];
                foreach (var session in data.SessionList)
                {
                    tasks[index] = Task.Run(() => addSession(session));
                    index++;
                }

                Task.WaitAll(tasks);
                refresh();

                info = sessions.OrderBy(i => i.time).Last();
                void action() => sessionClick?.Invoke(this, EventArgs.Empty);

                Invoke((Action)action);
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
                    break;
                case NIMSessionCommand.kNIMSessionCommandUpdate:
                    updateSession(e.Info);
                    break;
                case NIMSessionCommand.kNIMSessionCommandRemove:
                    removeSession(e.Info);
                    break;
            }

            refresh();
        }

        /// <summary>
        /// 新增会话记录
        /// </summary>
        /// <param name="info">会话信息</param>
        private void addSession(SessionInfo info)
        {
            var user = NimUtil.getUser(info.Id);
            var session = new NimSessionInfo
            {
                id = info.Id,
                name = user.name,
                head = NimUtil.getHeadImage(user.icon),
                message = readMsg(info),
                time = info.Timetag / 1000,
                unRead = info.UnreadCount > 0
            };
            sessions.Add(session);
        }

        /// <summary>
        /// 更新会话记录
        /// </summary>
        /// <param name="info">会话信息</param>
        private void updateSession(SessionInfo info)
        {
            var session = sessions.Find(i => i.id == info.Id);
            session.message = readMsg(info);
            session.time = info.Timetag / 1000;
            session.unRead = true;
        }

        /// <summary>
        /// 删除会话记录
        /// </summary>
        /// <param name="info">会话信息</param>
        private void removeSession(SessionInfo info)
        {
            var session = sessions.Find(i => i.id == info.Id);
            sessions.Remove(session);
        }

        /// <summary>
        /// 刷新会话列表
        /// </summary>
        private void refresh()
        {
            void action()
            {
                var hide = sceMain.Controls[0];
                var show = sceMain.Controls[1];
                sessions.OrderBy(i => i.time).ToList().ForEach(i =>
                {
                    var control = new SessionBox
                    {
                        headImage = i.head,
                        name = i.name,
                        message = i.message,
                        time = i.time,
                        unRead = i.unRead,
                        Name = i.id,
                        Location = new Point(0, height),
                        Dock = DockStyle.Top,
                    };
                    control.click += (sender, args) =>
                    {
                        info = i;
                        info.unRead = false;
                        resetUnread(info.id);
                        sessionClick?.Invoke(sender, args);
                    };
                    height = height + control.Size.Height;

                    hide.Controls.Add(control);
                    hide.Dock = height > Height ? DockStyle.Top : DockStyle.Fill;
                    hide.Height = height;
                });
                hide.Visible = true;
                show.Visible = false;
                Refresh();
                show.SendToBack();
            }

            Invoke((Action)action);
        }

        /// <summary>
        /// 读取消息内容
        /// </summary>
        /// <param name="info">会话信息</param>
        /// <returns>消息内容</returns>
        private string readMsg(SessionInfo info)
        {
            switch (info.MsgType)
            {
                case NIMMessageType.kNIMMessageTypeText:
                    return info.Content;
                case NIMMessageType.kNIMMessageTypeImage:
                    return "[图片]";
                case NIMMessageType.kNIMMessageTypeFile:
                    return "[文件]";
                default:
                    return "[未知]";
            }
        }

        /// <summary>
        /// 会话未读数清零
        /// </summary>
        /// <param name="id">会话ID</param>
        private void resetUnread(string id)
        {
            SessionAPI.SetUnreadCountZero(NIMSessionType.kNIMSessionTypeP2P, id, (a, b, c) =>
            {
            });

            MessagelogAPI.MarkMessagesStatusRead(id, NIMSessionType.kNIMSessionTypeP2P, (a, b, c) =>
            {
            });
        }

        /// <summary>
        /// 刷新会话时间
        /// </summary>
        private void refreshTime(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (SessionBox control in pceMain0.Controls)
            {
                control.refreshTime();
            }
        }
    }

    public class NimSessionInfo
    {
        /// <summary>
        /// 会话ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 对方昵称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 对方头像
        /// </summary>
        public Image head { get; set; }

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
