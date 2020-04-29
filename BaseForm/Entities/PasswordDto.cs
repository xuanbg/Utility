using Insight.Base.BaseForm.Utils;

namespace Insight.Base.BaseForm.Entities
{
    public class PasswordDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 验证参数,MD5(type + mobile + code)
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 新密码(MD5值)
        /// </summary>
        [InputCheck(true, "新密码不能为空，请输入您的新密码并牢记！")]
        public string password { get; set; }

        /// <summary>
        /// 原密码(MD5值)
        /// </summary>
        [InputCheck(true, "原密码不能为空，请输入正确的原密码！")]
        public string old { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string tenantId { get; set; }

        /// <summary>
        /// 登录部门ID
        /// </summary>
        public string deptId { get; set; }
    }
}
