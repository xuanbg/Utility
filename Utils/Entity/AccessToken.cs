using System;

namespace Insight.Utils.Entity
{
    /// <summary>
    /// 用户会话信息
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 用户OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户签名，用户名（大写）+ 密码MD5值的结果的MD5值
        /// 仅用于获取Token
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 登录用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录部门ID
        /// </summary>
        public Guid? DeptId { get; set; }

        /// <summary>
        /// 登录部门全称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 用户机器码
        /// </summary>
        public string MachineId { get; set; }

        /// <summary>
        /// 随机密码
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Secret失效时间
        /// </summary>
        public DateTime FailureTime { get; set; }
    }
}
