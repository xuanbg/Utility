using System;
using System.Diagnostics;
using Insight.Utils.Client;

namespace Insight.Utils.Entity
{
    public class LogInfo
    {
        /// <summary>
        /// 日志接口
        /// </summary>
        public string Interface { get; set; }

        /// <summary>
        /// AccessToken(Logger)
        /// </summary>
        public TokenHelper Token { get; set; }

        /// <summary>
        /// 系统日志事件源
        /// </summary>
        public string EventSource { get; set; }

        /// <summary>
        /// 系统日志事件类型
        /// </summary>
        public EventLogEntryType EventType { get; set; }

        /// <summary>
        /// 事件代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 事件消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 事件源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 事件名称
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 查询关键词
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 创建者用户ID
        /// </summary>
        public Guid? CreatorUserId { get; set; }
    }
}
