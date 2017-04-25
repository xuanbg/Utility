using System;
using System.Threading;
using StackExchange.Redis;

namespace Insight.Utils.Server
{
    public class CallManage
    {
        // 进程同步基元
        private static readonly Mutex _Mutex = new Mutex();

        // Redis连结字符串（默认本地）
        public static string RedisConn = "localhost:6379";

        /// <summary>
        /// 根据传入的时长返回当前调用的剩余限制时间（秒）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds">限制访问时长（秒）</param>
        /// <returns>int 剩余限制时间（秒）</returns>
        public static int LimitCall(string key, int seconds)
        {
            if (seconds <= 0) return 0;

            using (var client = ConnectionMultiplexer.Connect(RedisConn))
            {
                var value = client.GetDatabase().StringGet(key);
                var now = DateTime.Now;
                if (value.HasValue)
                {
                    // 如离上次访问时间不到1秒，则更新访问时间，返回等待时间为设定时间
                    var span = (now - DateTime.Parse(value)).TotalSeconds;
                    if (span < 1)
                    {
                        client.GetDatabase().StringSet(key, now.ToString("O"));
                        return seconds;
                    }

                    // 计算剩余时间，如剩余时间大于0，返回等待时间为剩余时间
                    var surplus = seconds - (int) Math.Floor(span);
                    if (surplus > 0) return surplus;

                    // 更新访问时间，返回等待时间为0
                    client.GetDatabase().StringSet(key, now.ToString("O"));
                    return 0;
                }

                // 开启线程锁
                _Mutex.WaitOne();

                // 发生并发访问，返回等待时间为设定时间
                value = client.GetDatabase().StringGet(key);
                if (value.HasValue)
                {
                    _Mutex.ReleaseMutex();
                    return seconds;
                }

                // 首次访问，登记访问时间，返回等待时间为0
                client.GetDatabase().StringSet(key, now.ToString("O"));
                _Mutex.ReleaseMutex();
                return 0;
            }
        }
    }
}