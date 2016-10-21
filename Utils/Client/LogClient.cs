using System;
using System.Collections.Generic;
using System.Diagnostics;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class LogClient
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public Result Result = new Result();

        /// <summary>
        /// 日志信息
        /// </summary>
        private readonly LogInfo _Info;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info"></param>
        public LogClient(LogInfo info)
        {
            var toserver = !string.IsNullOrEmpty(info.Interface);
            var toevent = !string.IsNullOrEmpty(info.EventSource);

            if (!toevent && !toserver)
            {
                Result.BadRequest("未知的日志存储目标");
                return;
            }

            _Info = info;
            if (toserver) LogToServer();

            if (toevent) LogToEvent();
        }

        /// <summary>
        /// 将事件消息写到日志服务器
        /// </summary>
        public void LogToServer()
        {
            var dict = new Dictionary<string, object>
            {
                {"code", _Info.Code},
                {"message", _Info.Message},
                {"source", _Info.Source},
                {"action", _Info.Action},
                {"key", _Info.Key},
                {"userid", _Info.CreatorUserId}
            };
            var data = Util.Serialize(dict);
            Result = new HttpClient(_Info.Interface, "POST", data).Request(_Info.Token);
        }

        /// <summary>
        /// 将事件消息写入系统日志
        /// </summary>
        public void LogToEvent()
        {
            try
            {
                if (!EventLog.SourceExists(_Info.EventSource))
                {
                    EventLog.CreateEventSource(_Info.EventSource, "应用程序");
                }

                EventLog.WriteEntry(_Info.EventSource, _Info.Message, _Info.EventType);
                Result.Success();
            }
            catch (ArgumentException ex)
            {
                Result.BadRequest(ex);
            }
        }
    }
}
