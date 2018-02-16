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
}
