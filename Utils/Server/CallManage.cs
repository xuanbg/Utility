using System;
using System.Collections.Generic;

namespace Insight.Utils.Server
{
    public class CallManage
    {
        // 接口调用时间记录
        private static readonly Dictionary<string, DateTime> _Requests = new Dictionary<string, DateTime>();

        /// <summary>
        /// 根据传入的时长返回当前调用的剩余限制时间（秒）
        /// </summary>
        /// <param name="seconds">限制访问时长（秒）</param>
        /// <returns>int 剩余限制时间（秒）</returns>
        public static int LimitCall(string key, int seconds)
        {
            if (seconds <= 0) return 0;

            if (!_Requests.ContainsKey(key))
            {
                _Requests.Add(key, DateTime.Now);
                return 0;
            }

            var span = _Requests[key].AddSeconds(seconds) - DateTime.Now;
            var surplus = (int)Math.Floor(span.TotalSeconds);
            if (seconds - surplus > 0 && seconds - surplus < 3)
            {
                _Requests[key] = DateTime.Now;
                return seconds;
            }

            if (surplus > 0) return surplus;

            _Requests[key] = DateTime.Now;
            return 0;
        }
    }
}
