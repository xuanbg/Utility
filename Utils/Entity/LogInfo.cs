using System.Diagnostics;

namespace Insight.Utils.Entity
{
    public class LogInfo
    {
        /// <summary>
        /// 日志接口
        /// </summary>
        public string logUrl { get; set; }

        /// <summary>
        /// AccessToken(Logger)
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 系统日志事件源
        /// </summary>
        public string eventSource { get; set; }

        /// <summary>
        /// 系统日志事件类型
        /// </summary>
        public EventLogEntryType eventType { get; set; }

        /// <summary>
        /// 操作代码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 事件来源
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        public string action { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 来源用户ID
        /// </summary>
        public string userId { get; set; }
    }
}
