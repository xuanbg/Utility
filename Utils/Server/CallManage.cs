using System;
using System.Collections.Generic;
using System.Threading;

namespace Insight.Utils.Server
{
    public class CallManage
    {
        // 进程同步基元
        private static readonly Mutex _Mutex = new Mutex();

        // 接口调用时间记录
        private static readonly Dictionary<string, DateTime> _Requests = new Dictionary<string, DateTime>();

        /// <summary>
        /// 根据传入的时长返回当前调用的剩余限制时间（秒）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds">限制访问时长（秒）</param>
        /// <returns>int 剩余限制时间（秒）</returns>
        public static int LimitCall(string key, int seconds)
        {
            if (seconds <= 0) return 0;

            // 非首次访问
            if (_Requests.ContainsKey(key))
            {
                // 如离上次访问时间不到1秒，则更新访问时间，返回等待时间为设定时间
                var span = (DateTime.Now - _Requests[key]).TotalSeconds;
                if (span < 1)
                {
                    _Requests[key] = DateTime.Now;
                    return seconds;
                }

                // 计算剩余时间，如剩余时间大于0，返回等待时间为剩余时间
                var surplus = seconds - (int) Math.Floor(span);
                if (surplus > 0) return surplus;

                // 更新访问时间，返回等待时间为0
                _Requests[key] = DateTime.Now;
                return 0;
            }


            // 开启线程锁
            _Mutex.WaitOne();

            // 发生并发访问，返回等待时间为设定时间
            if (_Requests.ContainsKey(key))
            {
                _Mutex.ReleaseMutex();
                return seconds;
            }

            // 首次访问，登记访问时间，返回等待时间为0
            _Requests.Add(key, DateTime.Now);
            _Mutex.ReleaseMutex();
            return 0;
        }
    }
}