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
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 当前登录部门ID
        /// </summary>
        public Guid? DeptId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 登录用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 随机密码
        /// </summary>
        public string Secret { get; set; }
    }
}