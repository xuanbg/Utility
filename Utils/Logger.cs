using System.Collections.Generic;
using System.Diagnostics;
using Insight.Utils.Entity;

namespace Insight.Utils
{
    public class Logger
    {
        private readonly string LogServer;
        private readonly string Token;
        private readonly string EventSource;
        private readonly EventLogEntryType EventType;

        /// <summary>
        /// 构造函数，写日志到系统日志
        /// </summary>
        /// <param name="source">事件源</param>
        /// <param name="type">事件类型</param>
        public Logger(string source, EventLogEntryType type = EventLogEntryType.Error)
        {
            EventSource = source;
            EventType = type;
        }

        /// <summary>
        /// 构造函数，写日志到日志服务器
        /// </summary>
        /// <param name="server">日志服务地址</param>
        /// <param name="token">日志服务AccessToken</param>
        public Logger(string server, string token)
        {
            LogServer = server;
            Token = token;
        }

        /// <summary>
        /// 将事件消息写到日志服务器
        /// </summary>
        /// <param name="code">事件代码</param>
        /// <param name="message">事件消息</param>
        /// <param name="source">事件来源</param>
        /// <param name="action">操作名称</param>
        /// <param name="userid">源用户ID</param>
        /// <param name="key">查询关键字段</param>
        public Result LogToServer(string code, string message = null, string source = null, string action = null, string userid = null, string key = null)
        {
            var dict = new Dictionary<string, string>
            {
                {"code", code},
                {"message", message},
                {"source", source },
                {"action", action },
                {"userid", userid },
                {"key", key }
            };
            var data = Util.Serialize(dict);
            return new HttpRequest(LogServer, "POST", Token, data).Result;
        }

        /// <summary>
        /// 将事件消息写入系统日志
        /// </summary>
        /// <param name="msg">事件消息</param>
        public void LogToEvent(string msg)
        {
            if (!EventLog.SourceExists(EventSource)) EventLog.CreateEventSource(EventSource, "应用程序");

            EventLog.WriteEntry(EventSource, msg, EventType);
        }
    }
}
