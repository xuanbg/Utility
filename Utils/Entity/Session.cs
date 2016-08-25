using System;

namespace Insight.Utils.Entity
{
    /// <summary>
    /// 用户会话信息
    /// </summary>
    public class Session:AccessToken
    {
        /// <summary>
        /// 用户状态
        /// </summary>
        public bool Validity { get; set; }

        /// <summary>
        /// 连续失败次数
        /// </summary>
        public int FailureCount { get; set; }

        /// <summary>
        /// 上次连接时间
        /// </summary>
        public DateTime LastConnect { get; set; }

        /// <summary>
        /// 用户在线状态
        /// </summary>
        public bool OnlineStatus { get; set; }

        /// <summary>
        /// 用户登录结果
        /// </summary>
        public LoginResult LoginResult { get; set; }
    }

    /// <summary>
    /// 用户登录结果
    /// </summary>
    public enum LoginResult
    {
        Success,
        Multiple,
        Online,
        Failure,
        Banned,
        NotExist,
        Unauthorized
    }
}
