using System.Collections.Generic;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class LogClient
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public Result<object> result = new Result<object>();

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
            var request = new HttpRequest(info.token);
            request.Send(info.logUrl, RequestMethod.POST, dict);
        }

        /// <summary>
        /// 将事件消息写入系统日志
        /// </summary>
        public void LogToEvent()
        {
            var error = Util.LogToEvent(info.eventSource, info.message, info.eventType);
            if (error == null)
            {
                result.Success();
            }
            else
            {
                result.BadRequest(error);
            }
        }
    }
}
