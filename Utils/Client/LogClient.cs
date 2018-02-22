using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Script.Serialization;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class LogClient
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public Result<string> result = new Result<string>();

        /// <summary>
        /// 日志信息
        /// </summary>
        private readonly LogInfo info;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info"></param>
        public LogClient(LogInfo info)
        {
            this.info = info;
            if (string.IsNullOrEmpty(info.eventSource) && string.IsNullOrEmpty(info.logUrl))
            {
                result.BadRequest("未知的日志存储目标");
            }
        }

        /// <summary>
        /// 将事件消息写到日志服务器
        /// </summary>
        public void LogToServer()
        {
            var dict = new Dictionary<string, object>
            {
                {"code", info.code},
                {"message", info.message},
                {"source", info.source},
                {"action", info.action},
                {"key", info.key},
                {"userid", info.userId}
            };
            var body = new JavaScriptSerializer().Serialize(dict);
            var request = new HttpRequest(info.token);
            request.Send(info.logUrl, body, RequestMethod.POST);
        }

        /// <summary>
        /// 将事件消息写入系统日志
        /// </summary>
        public void LogToEvent()
        {
            try
            {
                if (!EventLog.SourceExists(info.eventSource))
                {
                    EventLog.CreateEventSource(info.eventSource, "应用程序");
                }

                EventLog.WriteEntry(info.eventSource, info.message, info.eventType);
                result.Success();
            }
            catch (ArgumentException ex)
            {
                result.BadRequest(ex.Message);
            }
        }
    }
}
