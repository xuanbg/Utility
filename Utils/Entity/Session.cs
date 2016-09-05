using System;
using Insight.Utils.Common;

namespace Insight.Utils.Entity
{
    /// <summary>
    /// 用户会话信息
    /// </summary>
    public class Session:AccessToken
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 登录用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 登录部门ID
        /// </summary>
        public Guid? DeptId { get; set; }

        /// <summary>
        /// 用户签名，用户名（大写）+ 密码MD5值的结果的MD5值
        /// 仅用于获取Token
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 刷新密码
        /// </summary>
        public string RefreshKey { get; set; }

        /// <summary>
        /// 绑定的手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public bool Validity { get; set; }

        /// <summary>
        /// 连续失败次数
        /// </summary>
        public int FailureCount { get; set; }

        /// <summary>
        /// 上次连接时间
        /// </summary>
        public DateTime LastConnect { get; set; }

        /// <summary>
        /// Secret过期时间
        /// </summary>
        public DateTime Expired { get; set; }

        /// <summary>
        /// Secret失效时间
        /// </summary>
        public DateTime FailureTime { get; set; }

        /// <summary>
        /// 用户在线状态
        /// </summary>
        public bool OnlineStatus { get; set; }

        /// <summary>
        /// 设置Secret及过期时间
        /// </summary>
        /// <param name="expired">超时小时数</param>
        public void InitSecret(int expired)
        {
            var now = DateTime.Now;
            Secret = Util.Hash(Guid.NewGuid() + Signature + now);
            Expired = now.AddHours(UserType == 0 ? 2 : 24);
            RefreshKey = Util.Hash(Guid.NewGuid() + Secret);
            FailureTime = now.AddHours(UserType == 0 ? 24 : expired);
        }

        /// <summary>
        /// 刷新Secret过期时间
        /// </summary>
        public void Refresh()
        {
            var exten = UserType == 0 ? 1 : 24;
            Expired = Expired.AddHours(exten);
        }

        /// <summary>
        /// 使Session在线
        /// </summary>
        public void Online()
        {
            OnlineStatus = true;
            FailureCount = 0;
        }

        /// <summary>
        /// 注销Session
        /// </summary>
        public void SignOut()
        {
            Secret = Guid.NewGuid().ToString();
            OnlineStatus = false;
        }
    }
}
