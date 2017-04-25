using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public static class ResultExt
    {
        /// <summary>
        /// 接口调用成功（200）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data">承载的数据</param>
        public static Result Success(this Result result, object data = null)
        {
            result.successful = true;
            result.code = "200";
            result.name = "OK";
            result.message = "接口调用成功";
            result.data = Util.Serialize(data ?? "NoContent");
            return result;
        }

        /// <summary>
        /// 接口调用成功（200）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data">承载的数据</param>
        public static Result Success(this Result result, string data)
        {
            result.successful = true;
            result.code = "200";
            result.name = "OK";
            result.message = "接口调用成功";
            result.data = data;
            return result;
        }

        /// <summary>
        /// 资源创建成功（201）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data">承载的数据</param>
        public static Result Created(this Result result, object data = null)
        {
            result.successful = true;
            result.code = "201";
            result.name = "Created";
            result.message = "资源创建成功";
            result.data = Util.Serialize(data ?? "NoContent");
            return result;
        }

        /// <summary>
        /// 资源创建成功（201）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data">承载的数据</param>
        public static Result Created(this Result result, string data)
        {
            result.successful = true;
            result.code = "201";
            result.name = "Created";
            result.message = "资源创建成功";
            result.data = data;
            return result;
        }

        /// <summary>
        /// 无可用内容（204）
        /// </summary>
        /// <param name="result"></param>
        public static Result NoContent(this Result result)
        {
            result.successful = true;
            result.code = "204";
            result.name = "NoContent";
            result.message = "无可用内容";
            result.data = "[]";
            return result;
        }

        /// <summary>
        /// 无需刷新（205）
        /// </summary>
        /// <param name="result"></param>
        public static Result WithoutRefresh(this Result result)
        {
            result.successful = true;
            result.code = "205";
            result.name = "WithoutRefresh";
            result.message = "尚未过期，无需刷新";
            return result;
        }

        /// <summary>
        /// 请求参数错误（400）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data">错误详细信息（可选）</param>
        public static Result BadRequest(this Result result, object data = null)
        {
            result.successful = false;
            result.code = "400";
            result.name = "BadRequest";
            result.message = $"请求参数错误！{data}";
            return result;
        }

        /// <summary>
        /// 身份验证失败（401）
        /// </summary>
        /// <param name="result"></param>
        public static Result InvalidAuth(this Result result)
        {
            result.successful = false;
            result.code = "401";
            result.name = "InvalidAuthenticationInfo";
            result.message = "提供的身份验证信息不正确";
            return result;
        }

        /// <summary>
        /// 调用接口过于频繁（402）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data"></param>
        public static Result TooFrequent(this Result result, string data)
        {
            result.successful = false;
            result.code = "402";
            result.name = "CallInterfaceTooFrequent";
            result.message = "调用接口过于频繁";
            result.data = data;
            return result;
        }

        /// <summary>
        /// 用户未取得授权（403）
        /// </summary>
        /// <param name="result"></param>
        public static Result Forbidden(this Result result)
        {
            result.successful = false;
            result.code = "403";
            result.name = "Forbidden";
            result.message = "当前用户未取得授权";
            return result;
        }

        /// <summary>
        /// 指定的资源不存在（404）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="msg"></param>
        public static Result NotFound(this Result result, string msg = null)
        {
            result.successful = false;
            result.code = "404";
            result.name = "ResourceNotFound";
            result.message = msg ?? "指定的资源不存在";
            return result;
        }

        /// <summary>
        /// AccessToken已过期（405）
        /// </summary>
        /// <param name="result"></param>
        public static Result Expired(this Result result)
        {
            result.successful = false;
            result.code = "405";
            result.name = "AccessTokenExpired";
            result.message = "AccessToken已过期";
            return result;
        }

        /// <summary>
        /// AccessToken已失效（406）
        /// </summary>
        public static Result Failured(this Result result)
        {
            result.successful = false;
            result.code = "406";
            result.name = "AccessTokenFailured";
            result.message = "AccessToken已失效";
            return result;
        }

        /// <summary>
        /// 获取AccessToken失败（407）
        /// </summary>
        public static Result GetTokenFailured(this Result result)
        {
            result.successful = false;
            result.code = "407";
            result.name = "GetTokenFailured";
            result.message = "用户名或密码错误";
            return result;
        }

        /// <summary>
        /// 获取Code失败（408）
        /// </summary>
        public static Result GetCodeFailured(this Result result)
        {
            result.successful = false;
            result.code = "408";
            result.name = "GetCodeFailured";
            result.message = "获取Code失败";
            return result;
        }

        /// <summary>
        /// 用户已存在（409）
        /// </summary>
        public static Result AccountExists(this Result result)
        {
            result.successful = false;
            result.code = "409";
            result.name = "AccountAlreadyExists";
            result.message = "用户已存在";
            return result;
        }

        /// <summary>
        /// 用户被禁止登录（410）
        /// </summary>
        public static Result Disabled(this Result result)
        {
            result.successful = false;
            result.code = "410";
            result.name = "AccountIsDisabled";
            result.message = "当前用户被禁止登录";
            return result;
        }

        /// <summary>
        /// 账号已锁定（411）
        /// </summary>
        public static Result AccountIsBlocked(this Result result)
        {
            result.successful = false;
            result.code = "411";
            result.name = "AccountIsBlocked";
            result.message = "账号已锁定";
            return result;
        }

        /// <summary>
        /// 转换为Guid失败（420）
        /// </summary>
        public static Result InvalidGuid(this Result result)
        {
            result.successful = false;
            result.code = "420";
            result.name = "InvalidGUID";
            result.message = "错误的GUID数据";
            return result;
        }

        /// <summary>
        /// 转换为DateTime失败（421）
        /// </summary>
        public static Result InvalidDateTime(this Result result)
        {
            result.successful = false;
            result.code = "421";
            result.name = "InvalidDateTime";
            result.message = "错误的DateTime数据";
            return result;
        }

        /// <summary>
        /// 数值转换失败（422）
        /// </summary>
        public static Result InvalidValue(this Result result)
        {
            result.successful = false;
            result.code = "422";
            result.name = "InvalidValue";
            result.message = "错误的数据";
            return result;
        }
        
        /// <summary>
        /// 写数据库失败（501）
        /// </summary>
        public static Result DataBaseError(this Result result)
        {
            result.successful = false;
            result.code = "501";
            result.name = "DataBaseError";
            result.message = "写数据库失败";
            return result;
        }

        /// <summary>
        /// 数据已存在（502）
        /// </summary>
        public static Result DataAlreadyExists(this Result result)
        {
            result.successful = false;
            result.code = "502";
            result.name = "DataAlreadyExists";
            result.message = "数据已存在";
            return result;
        }

        /// <summary>
        /// 当前服务不可用（503）
        /// </summary>
        public static Result ServiceUnavailable(this Result result)
        {
            result.successful = false;
            result.code = "503";
            result.name = "ServiceUnavailable";
            result.message = "当前服务不可用";
            return result;
        }

        /// <summary>
        /// 未更新任何数据（504）
        /// </summary>
        public static Result NotUpdate(this Result result)
        {
            result.successful = false;
            result.code = "504";
            result.name = "DataNotUpdate";
            result.message = "未更新任何数据";
            return result;
        }

        /// <summary>
        /// 不允许修改和删除的数据（505）
        /// </summary>
        public static Result NotBeModified(this Result result)
        {
            result.successful = false;
            result.code = "505";
            result.name = "NotBeModified";
            result.message = "不允许修改和删除的数据";
            return result;
        }

        /// <summary>
        /// 不允许删除的数据（506）
        /// </summary>
        public static Result NotBeDeleted(this Result result)
        {
            result.successful = false;
            result.code = "506";
            result.name = "NotBeDeleted";
            result.message = "不允许删除的数据";
            return result;
        }
    }
}
