using System;

namespace Insight.Utils.Entity
{

    public class TokenResult
    {
        /// <summary>
        /// AccessToken字符串
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// RefresToken字符串
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Secret过期时间
        /// </summary>
        public DateTime ExpiryTime { get; set; }

        /// <summary>
        /// Secret失效时间
        /// </summary>
        public DateTime FailureTime { get; set; }
    }
}
