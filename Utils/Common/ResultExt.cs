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
        /// <param name="option"></param>
        public static Result<T> Success<T>(this Result<T> result, T data = default(T), object option = null)
        {
            result.successful = true;
            result.code = "200";
            result.name = "OK";
            result.message = "接口调用成功";
            result.option = option;
            result.data = data;
            return result;
        }

        /// <summary>
        /// 资源创建成功（201）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data">承载的数据</param>
        public static Result<T> Created<T>(this Result<T> result, T data = default(T))
        {
            result.successful = true;
            result.code = "201";
            result.name = "Created";
            result.message = "资源创建成功";
            result.option = null;
            result.data = data;
            return result;
        }

        /// <summary>
        /// 无可用内容（204）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data">承载的数据</param>
        public static Result<T> NoContent<T>(this Result<T> result, T data)
        {
            result.successful = true;
            result.code = "204";
            result.name = "NoContent";
            result.message = "无可用内容";
            result.option = null;
            result.data = data;
            return result;
        }

        /// <summary>
        /// 无需刷新（205）
        /// </summary>
        /// <param name="result"></param>
        public static Result<T> WithoutRefresh<T>(this Result<T> result)
        {
            result.successful = true;
            result.code = "205";
            result.name = "WithoutRefresh";
            result.message = "尚未过期，无需刷新";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 请求参数错误（400）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message">错误详细信息（可选）</param>
        public static Result<T> BadRequest<T>(this Result<T> result, string message = null) where T : new()
        {
            result.successful = false;
            result.code = "400";
            result.name = "BadRequest";
            result.message = $"请求参数错误！{message}";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 获取AccessToken失败（401）
        /// </summary>
        /// <param name="result"></param>
        public static Result<T> InvalidToken<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "401";
            result.name = "InvalidAuthenticationInfo";
            result.message = "提供的身份验证信息不正确";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 调用接口过于频繁（402）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="option">剩余秒数</param>
        public static Result<T> TooFrequent<T>(this Result<T> result, object option)
        {
            result.successful = false;
            result.code = "402";
            result.name = "CallInterfaceTooFrequent";
            result.message = "调用接口过于频繁";
            result.option = option;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 用户未取得授权（403）
        /// </summary>
        /// <param name="result"></param>
        public static Result<T> Forbidden<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "403";
            result.name = "Forbidden";
            result.message = "当前用户未取得授权";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 指定的资源不存在（404）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="msg"></param>
        public static Result<T> NotFound<T>(this Result<T> result, string msg = null)
        {
            result.successful = false;
            result.code = "404";
            result.name = "ResourceNotFound";
            result.message = msg ?? "指定的资源不存在";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// AccessToken已过期（405）
        /// </summary>
        /// <param name="result"></param>
        public static Result<T> Expired<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "405";
            result.name = "AccessTokenExpired";
            result.message = "AccessToken已过期";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// AccessToken已失效（406）
        /// </summary>
        public static Result<T> Failured<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "406";
            result.name = "AccessTokenFailured";
            result.message = "AccessToken已失效";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 身份验证失败（407）
        /// </summary>
        /// <param name="result"></param>
        public static Result<T> InvalidAuth<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "407";
            result.name = "InvalidAuthenticationInfo";
            result.message = "账号或密码错误";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 获取Code失败（408）
        /// </summary>
        public static Result<T> GetCodeFailured<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "408";
            result.name = "GetCodeFailured";
            result.message = "获取Code失败";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 用户已存在（409）
        /// </summary>
        public static Result<T> AccountExists<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "409";
            result.name = "AccountAlreadyExists";
            result.message = "用户已存在";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 用户被禁止登录（410）
        /// </summary>
        public static Result<T> Disabled<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "410";
            result.name = "AccountIsDisabled";
            result.message = "当前用户被禁止登录";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 用户已被锁定（411）
        /// </summary>
        public static Result<T> Locked<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "411";
            result.name = "UserLocked";
            result.message = "用户已被锁定，请10分钟后再试";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 租户已过期（412）
        /// </summary>
        public static Result<T> TenantIsExpiry<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "412";
            result.name = "TenantIsExpiry";
            result.message = "租户已过期！请在续租后重新登录系统";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 不存在指定的用户（413）
        /// </summary>
        public static Result<T> NotExists<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "413";
            result.name = "UserNotExists";
            result.message = "不存在指定的用户";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 转换为Guid失败（420）
        /// </summary>
        public static Result<T> InvalidGuid<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "420";
            result.name = "InvalidGUID";
            result.message = "错误的GUID数据";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 转换为DateTime失败（421）
        /// </summary>
        public static Result<T> InvalidDateTime<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "421";
            result.name = "InvalidDateTime";
            result.message = "错误的DateTime数据";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 数值转换失败（422）
        /// </summary>
        public static Result<T> InvalidValue<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "422";
            result.name = "InvalidValue";
            result.message = "错误的数据";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 未找到指定的文件（430）
        /// </summary>
        public static Result<T> FileNotExists<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "430";
            result.name = "FileNotExists";
            result.message = "未找到指定的文件";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 未找到指定的Sheet（431）
        /// </summary>
        public static Result<T> SheetNotExists<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "431";
            result.name = "SheetNotExists";
            result.message = "未找到指定的Sheet";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 未从文件中读取任何数据行（432）
        /// </summary>
        public static Result<T> NoRowsRead<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "432";
            result.name = "NoRowsRead";
            result.message = "未从文件中读取任何数据行";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// Excel格式不正确（433）
        /// </summary>
        public static Result<T> IncorrectExcelFormat<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "433";
            result.name = "IncorrectExcelFormat";
            result.message = "Excel格式不正确";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 服务器错误（500）
        /// </summary>
        public static Result<T> ServerError<T>(this Result<T> result, string message)
        {
            result.successful = false;
            result.code = "500";
            result.name = "ServerError";
            result.message = message;
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 写数据库失败（501）
        /// </summary>
        public static Result<T> DataBaseError<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "501";
            result.name = "DataBaseError";
            result.message = "写数据库失败";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 数据已存在（502）
        /// </summary>
        public static Result<T> DataAlreadyExists<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "502";
            result.name = "DataAlreadyExists";
            result.message = "数据已存在";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 当前服务不可用（503）
        /// </summary>
        public static Result<T> ServiceUnavailable<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "503";
            result.name = "ServiceUnavailable";
            result.message = "当前服务不可用";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 未更新任何数据（504）
        /// </summary>
        public static Result<T> NotUpdate<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "504";
            result.name = "DataNotUpdate";
            result.message = "未更新任何数据";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 不允许修改和删除的数据（505）
        /// </summary>
        public static Result<T> NotBeModified<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "505";
            result.name = "NotBeModified";
            result.message = "不允许修改和删除的数据";
            result.option = null;
            result.data = default(T);
            return result;
        }

        /// <summary>
        /// 不允许删除的数据（506）
        /// </summary>
        public static Result<T> NotBeDeleted<T>(this Result<T> result)
        {
            result.successful = false;
            result.code = "506";
            result.name = "NotBeDeleted";
            result.message = "不允许删除的数据";
            result.option = null;
            result.data = default(T);
            return result;
        }
    }
}
