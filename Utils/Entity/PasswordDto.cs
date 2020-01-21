using Insight.Utils.Common;

namespace Insight.Utils.Entity
{
    public class PasswordDto
    {
        /// <summary>
        /// 原密码
        /// </summary>
        [InputCheck(true, "原密码不能为空，请输入正确的原密码！")]
        public string old { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [InputCheck(true, "新密码不能为空，请输入您的新密码并牢记！")]
        public string password { get; set; }
    }
}
