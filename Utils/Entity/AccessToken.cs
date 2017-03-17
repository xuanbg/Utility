using System;

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
        public Guid id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid userId { get; set; }

        /// <summary>
        /// 当前登录部门ID
        /// </summary>
        public Guid? deptId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 登录用户姓名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 随机密码
        /// </summary>
        public string secret { get; set; }
    }
}