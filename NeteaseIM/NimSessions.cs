using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Common;
using Insight.Utils.NetEaseIM.Controls;
using NIM.Messagelog;
using NIM.Session;

namespace Insight.Utils.NetEaseIM
{
    public partial class NimSessions : XtraUserControl
    {
        private readonly List<NimSessionInfo> sessions = new List<NimSessionInfo>();
        private string msgId;
        private string id;

        /// <summary>
        /// 表示将处理当前点击会话的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SessionClickHandle(object sender, SessionEventArgs e);

        /// <summary>
        /// 表示将处理当前点击会话的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SessionDoubleClickHandle(object sender, SessionEventArgs e);

        /// <summary>  
        /// 当点击会话后，通知处理
        /// </summary>  
        public event SessionClickHandle sessionClick;

        /// <summary>  
        /// 当点击会话后，通知处理
        /// </summary>  
        public event SessionDoubleClickHandle sessionDoubleClick;

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
            ppcSession.reloadPage += (sender, args) => refresh(args.page - 1);
        }

        /// <summary>
        /// 获取会话列表
        /// </summary>
        public void getSessions()
        {
            sessions.Clear();
            SessionAPI.QueryAllRecentSession((count, data) =>
            {
                var list = data?.SessionList;
                if (list == null || !list.Any()) return;

                var index = 0;
                var tasks = new Task[list.Count];
                foreach (var session in list)
                {
                    tasks[index] = Task.Run(() => addSession(session));
                    index++;
                }

                Task.WaitAll(tasks);

                void action()
                {
                    ppcSession.totalRows = sessions.Count;
                    refresh();
                    click();
                    sessionClick?.Invoke(this, new SessionEventArgs(id, true));
                }

                while (!(Parent?.IsHandleCreated ?? false)) Thread.Sleep(100);

                Invoke((Action) action);
            });
        }

        /// <summary>
        /// 处理会话更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sessionChanged(object sender, SessionChangedEventArgs e)
        {
            if (msgId == e.Info.MsgId) return;

            msgId = e.Info.MsgId;
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

            void action() => ppcSession.totalRows = sessions.Count;

            while (!(Parent?.IsHandleCreated ?? false)) Thread.Sleep(100);

            Invoke((Action)action);
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
                head = Util.getImageFromUrl(user.icon),
                message = NimUtil.readMsg(info),
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
            if (session == null)
            {
                addSession(info);
                return;
            }

            session.message = NimUtil.readMsg(info);
            session.time = info.Timetag / 1000;
            session.unRead = info.Id == info.Sender;
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
        /// <param name="page">页码</param>
        private void refresh(int page = 0)
        {
            var show = sceMain.Controls[0];
            var hide = sceMain.Controls[1];
            var height = 0;
            sessions.OrderByDescending(i => i.time).Skip(page * ppcSession.size).Take(ppcSession.size).OrderBy(i => i.time).ToList().ForEach(i =>
            {
                var control = new SessionBox
                {
                    headImage = i.head,
                    name = i.name,
                    message = i.message,
                    time = i.time,
                    unRead = i.unRead,
                    Name = i.id,
                    Dock = DockStyle.Top,
                };
                if (i.id == id) control.BackColor = Color.White;

                control.click += (sender, args) =>
                {
                    sessionClick?.Invoke(sender, new SessionEventArgs(i.id, i.unRead));
                    click(control);
                };
                control.doubleClick += (sender, args) =>
                {
                    sessionDoubleClick?.Invoke(sender, new SessionEventArgs(i.id, i.unRead));
                    click(control);
                };

                height = height + control.Height;
                hide.Controls.Add(control);
            });

            hide.Dock = height > Height ? DockStyle.Top : DockStyle.Fill;
            hide.Height = height;
            hide.Visible = true;
            show.Visible = false;

            show.SendToBack();
            foreach (Control control in show.Controls)
            {
                NimUtil.clearEvent(control, "click");
                NimUtil.clearEvent(control, "doubleClick");
                control.Dispose();
            }

            show.Controls.Clear();
        }

        /// <summary>
        /// 点击会话
        /// </summary>
        /// <param name="box">会话控件</param>
        private void click(SessionBox box = null)
        {
            if (id == null) id = sessions.OrderBy(i => i.time).Last().id;

            var control = (SessionBox) sceMain.Controls[0].Controls[id];
            if (control == null) return;

            control.BackColor = sceMain.Controls[0].BackColor;
            control.Refresh();
            if (box == null) box = control;

            box.unRead = false;
            box.BackColor = Color.White;
            box.Refresh();

            id = box.Name;
            var session = sessions.Find(i => i.id == id);
            session.unRead = false;

            SessionAPI.SetUnreadCountZero(NIMSessionType.kNIMSessionTypeP2P, id, (a, b, c) => { });
            MessagelogAPI.MarkMessagesStatusRead(id, NIMSessionType.kNIMSessionTypeP2P, (a, b, c) => { });
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

    public class SessionEventArgs : EventArgs
    {
        /// <summary>
        /// 云信ID
        /// </summary>
        public string id;

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool unread;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="unread">是否未读</param>
        public SessionEventArgs(string id, bool unread)
        {
            this.id = id;
            this.unread = unread;
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
