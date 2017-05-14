using System;
using StackExchange.Redis;

namespace Insight.Utils.Server
{
    public class CallManage
    {
        private readonly ConnectionMultiplexer _Redis;

        /// <summary>
        /// 设置Redis链接参数
        /// </summary>
        /// <param name="redis">Redis链接对象</param>
        public CallManage(ConnectionMultiplexer redis)
        {
            _Redis = redis;
        }

        /// <summary>
        /// 根据传入的时长返回当前调用的剩余限制时间（秒）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds">限制访问时长（秒）</param>
        /// <returns>int 剩余限制时间（秒）</returns>
        public int LimitCall(string key, int seconds)
        {
            var now = DateTime.Now;
            var val = now.ToString("O");
            var ts = new TimeSpan(0, 0, seconds);
            var db = _Redis.GetDatabase();
            var value = db.StringGet(key);

            // 初次访问或正常频次访问
            if (value.IsNullOrEmpty)
            {
                db.StringSet(key, val, ts);
                return 0;
            }

            // 如离上次访问时间不到1秒，则更新访问时间，返回等待时间为设定时间
            var span = (now - DateTime.Parse(value)).TotalSeconds;
            if (span < 1)
            {
                db.StringSet(key, val, ts);
                return seconds;
            }

            // 计算剩余时间，如剩余时间大于0，返回等待时间为剩余时间
            var surplus = seconds - (int) span;
            if (surplus > 0) return surplus;

            // Redis记录访问记录，返回等待时间为0
            db.StringSet(key, val, ts);
            return 0;
        }
    }
}