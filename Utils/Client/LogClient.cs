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
        private readonly LogInfo _info;

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

            _info = info;
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
                {"code", _info.Code},
                {"message", _info.Message},
                {"source", _info.Source},
                {"action", _info.Action},
                {"key", _info.Key},
                {"userid", _info.CreatorUserId}
            };
            var data = Util.Serialize(dict);
            Result = new HttpRequest(_info.Interface, "POST", _info.Token, data).Result;
        }

        /// <summary>
        /// 将事件消息写入系统日志
        /// </summary>
        public void LogToEvent()
        {
            try
            {
                if (!EventLog.SourceExists(_info.EventSource))
                {
                    EventLog.CreateEventSource(_info.EventSource, "应用程序");
                }

                EventLog.WriteEntry(_info.EventSource, _info.Message, _info.EventType);
                Result.Success();
            }
            catch (ArgumentException ex)
            {
                Result.BadRequest(ex);
            }
        }
    }
}
