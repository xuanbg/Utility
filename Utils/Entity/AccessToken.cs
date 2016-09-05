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
        /// 用户账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 登录用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户特征码
        /// </summary>
        public string Stamp { get; set; }

        /// <summary>
        /// 随机密码
        /// </summary>
        public string Secret { get; set; }
    }
}
