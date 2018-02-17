namespace Insight.Utils.Entity
{
    /// <summary>
    /// 用户会话信息
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 当前登录部门ID
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        /// 登录用户姓名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 随机密码
        /// </summary>
        public string secret { get; set; }
    }

    /// <summary>
    /// 用户会话信息
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 随机密码
        /// </summary>
        public string secret { get; set; }
    }
}