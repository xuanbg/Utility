using System;
using Insight.Utils.Entity;

namespace Insight.Utils.Server
{
    public class ServiceBase
    {
        protected CallManage CallManage;
        protected string VerifyUrl;

        public Result<object> Result = new Result<object>();
        public string UserName;
        public Guid UserId;
        public Guid? DeptId;

        /// <summary>
        /// 身份验证方法
        /// </summary>
        /// <param name="action">操作权限代码，默认为空，即不进行鉴权</param>
        /// <param name="limit"></param>
        /// <returns>bool 身份是否通过验证</returns>
        public bool Verify(string action = null, int limit = 0)
        {
            var verify = new Verify(CallManage, VerifyUrl, action, limit);
            UserName = verify.Token.userName;
            UserId = verify.Token.userId;
            DeptId = verify.Token.deptId;
            Result = verify.Result;

            return Result.successful;
        }
    }
}