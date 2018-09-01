using System;

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

    public class UserInfo
    {
        /// <summary>
        /// ID，唯一标识
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 姓名/昵称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 注册邮箱
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string tenantId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string tenantName { get; set; }

        /// <summary>
        /// 登录部门ID
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        /// 登录部门编码
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 登录部门名称
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 是否预置：0、自定；1、预置
        /// </summary>
        public bool isBuiltin { get; set; }

        /// <summary>
        /// 是否失效：0、有效；1、失效
        /// </summary>
        public bool isInvalid { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
    }
}
