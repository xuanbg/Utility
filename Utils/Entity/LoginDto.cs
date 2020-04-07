namespace Insight.Utils.Entity
{
    public class LoginDto
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string tenantId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }

        /// <summary>
        /// 验证码/微信授权码(微信登录用)
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 微信appId(微信登录用)
        /// </summary>
        public string weChatAppId { get; set; }

        /// <summary>
        /// 微信用户唯一ID(微信登录用)
        /// </summary>
        public string unionId { get; set; }

        /// <summary>
        /// 是否替换用户的UnionId(微信登录用)
        /// </summary>
        public bool? isReplace { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string deviceId { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string deviceModel { get; set; }
    }
}
