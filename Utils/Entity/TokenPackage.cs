namespace Insight.Utils.Entity
{

    public class TokenPackage
    {
        /// <summary>
        /// AccessToken字符串
        /// </summary>
        public string accessToken { get; set; }

        /// <summary>
        /// RefresToken字符串
        /// </summary>
        public string refreshToken { get; set; }

        /// <summary>
        /// Secret过期时间
        /// </summary>
        public int expiryTime { get; set; }

        /// <summary>
        /// Secret失效时间
        /// </summary>
        public int failureTime { get; set; }
    }

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
        /// 随机密码
        /// </summary>
        public string secret { get; set; }
    }

    public class Session
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public string tenantId { get; set; }

        /// <summary>
        /// 登录部门ID
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 用户E-mail
        /// </summary>
        public string email { get; set; }
    }
}
