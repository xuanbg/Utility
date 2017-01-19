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
            result.Successful = true;
            result.Code = "200";
            result.Name = "OK";
            result.Message = "接口调用成功";
            result.Data = Util.Serialize(data ?? true);
            return result;
        }

        /// <summary>
        /// 资源创建成功（201）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data">承载的数据</param>
        public static Result Created(this Result result, object data = null)
        {
            result.Successful = true;
            result.Code = "201";
            result.Name = "Created";
            result.Message = "资源创建成功";
            result.Data = Util.Serialize(data ?? true);
            return result;
        }

        /// <summary>
        /// 无可用内容（204）
        /// </summary>
        /// <param name="result"></param>
        public static Result NoContent(this Result result)
        {
            result.Successful = true;
            result.Code = "204";
            result.Name = "NoContent";
            result.Message = "无可用内容";
            result.Data = "[]";
            return result;
        }

        /// <summary>
        /// 无需刷新（205）
        /// </summary>
        /// <param name="result"></param>
        public static Result WithoutRefresh(this Result result)
        {
            result.Successful = true;
            result.Code = "205";
            result.Name = "WithoutRefresh";
            result.Message = "尚未过期，无需刷新";
            return result;
        }

        /// <summary>
        /// 请求参数错误（400）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data">错误详细信息（可选）</param>
        public static Result BadRequest(this Result result, object data = null)
        {
            result.Successful = false;
            result.Code = "400";
            result.Name = "BadRequest";
            result.Message = $"请求参数错误！{data}";
            return result;
        }

        /// <summary>
        /// 身份验证失败（401）
        /// </summary>
        /// <param name="result"></param>
        public static Result InvalidAuth(this Result result)
        {
            result.Successful = false;
            result.Code = "401";
            result.Name = "InvalidAuthenticationInfo";
            result.Message = "提供的身份验证信息不正确";
            return result;
        }

        /// <summary>
        /// 调用接口过于频繁（402）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data"></param>
        public static Result TooFrequent(this Result result, object data)
        {
            result.Successful = false;
            result.Code = "402";
            result.Name = "CallInterfaceTooFrequent";
            result.Message = "调用接口过于频繁";
            return result;
        }

        /// <summary>
        /// 用户未取得授权（403）
        /// </summary>
        /// <param name="result"></param>
        public static Result Forbidden(this Result result)
        {
            result.Successful = false;
            result.Code = "403";
            result.Name = "Forbidden";
            result.Message = "当前用户未取得授权";
            return result;
        }

        /// <summary>
        /// 指定的资源不存在（404）
        /// </summary>
        /// <param name="result"></param>
        public static Result NotFound(this Result result)
        {
            result.Successful = false;
            result.Code = "404";
            result.Name = "ResourceNotFound";
            result.Message = "指定的资源不存在";
            return result;
        }

        /// <summary>
        /// AccessToken已过期（405）
        /// </summary>
        /// <param name="result"></param>
        public static Result Expired(this Result result)
        {
            result.Successful = false;
            result.Code = "405";
            result.Name = "AccessTokenExpired";
            result.Message = "AccessToken已过期";
            return result;
        }

        /// <summary>
        /// AccessToken已失效（406）
        /// </summary>
        public static Result Failured(this Result result)
        {
            result.Successful = false;
            result.Code = "406";
            result.Name = "AccessTokenFailured";
            result.Message = "AccessToken已失效";
            return result;
        }

        /// <summary>
        /// 许可证数量不足（407）
        /// </summary>
        public static Result InsufficientLicenses(this Result result)
        {
            result.Successful = false;
            result.Code = "407";
            result.Name = "InsufficientNumberOfLicenses";
            result.Message = "许可证数量不足";
            return result;
        }

        /// <summary>
        /// 账号已锁定（408）
        /// </summary>
        public static Result AccountIsBlocked(this Result result)
        {
            result.Successful = false;
            result.Code = "408";
            result.Name = "AccountIsBlocked";
            result.Message = "账号已锁定";
            return result;
        }

        /// <summary>
        /// 用户已存在（409）
        /// </summary>
        public static Result AccountExists(this Result result)
        {
            result.Successful = false;
            result.Code = "409";
            result.Name = "AccountAlreadyExists";
            result.Message = "用户已存在";
            return result;
        }

        /// <summary>
        /// 用户被禁止登录（410）
        /// </summary>
        public static Result Disabled(this Result result)
        {
            result.Successful = false;
            result.Code = "410";
            result.Name = "AccountIsDisabled";
            result.Message = "当前用户被禁止登录";
            return result;
        }

        /// <summary>
        /// 转换为Guid失败（420）
        /// </summary>
        public static Result InvalidGuid(this Result result)
        {
            result.Successful = false;
            result.Code = "420";
            result.Name = "InvalidGUID";
            result.Message = "错误的GUID数据";
            return result;
        }

        /// <summary>
        /// 转换为DateTime失败（421）
        /// </summary>
        public static Result InvalidDateTime(this Result result)
        {
            result.Successful = false;
            result.Code = "421";
            result.Name = "InvalidDateTime";
            result.Message = "错误的DateTime数据";
            return result;
        }

        /// <summary>
        /// 数值转换失败（422）
        /// </summary>
        public static Result InvalidValue(this Result result)
        {
            result.Successful = false;
            result.Code = "422";
            result.Name = "InvalidValue";
            result.Message = "错误的数据";
            return result;
        }

        /// <summary>
        /// 未找到指定的文件（430）
        /// </summary>
        public static Result FileNotExists(this Result result)
        {
            result.Successful = false;
            result.Code = "430";
            result.Name = "FileNotExists";
            result.Message = "未找到指定的文件";
            return result;
        }

        /// <summary>
        /// 未找到指定的Sheet（431）
        /// </summary>
        public static Result SheetNotExists(this Result result)
        {
            result.Successful = false;
            result.Code = "431";
            result.Name = "SheetNotExists";
            result.Message = "未找到指定的Sheet";
            return result;
        }

        /// <summary>
        /// 未从文件中读取任何数据行（432）
        /// </summary>
        public static Result NoRowsRead(this Result result)
        {
            result.Successful = false;
            result.Code = "432";
            result.Name = "NoRowsRead";
            result.Message = "未从文件中读取任何数据行";
            return result;
        }

        /// <summary>
        /// Excel格式不正确（433）
        /// </summary>
        public static Result IncorrectExcelFormat(this Result result)
        {
            result.Successful = false;
            result.Code = "433";
            result.Name = "IncorrectExcelFormat";
            result.Message = "Excel格式不正确";
            return result;
        }

        /// <summary>
        /// 写数据库失败（501）
        /// </summary>
        public static Result DataBaseError(this Result result)
        {
            result.Successful = false;
            result.Code = "501";
            result.Name = "result.DataBaseError";
            result.Message = "写数据库失败";
            return result;
        }

        /// <summary>
        /// 数据已存在（502）
        /// </summary>
        public static Result DataAlreadyExists(this Result result)
        {
            result.Successful = false;
            result.Code = "502";
            result.Name = "result.DataAlreadyExists";
            result.Message = "数据已存在";
            return result;
        }

        /// <summary>
        /// 当前服务不可用（503）
        /// </summary>
        public static Result ServiceUnavailable(this Result result)
        {
            result.Successful = false;
            result.Code = "503";
            result.Name = "ServiceUnavailable";
            result.Message = "当前服务不可用";
            return result;
        }

        /// <summary>
        /// 未更新任何数据（504）
        /// </summary>
        public static Result NotUpdate(this Result result)
        {
            result.Successful = false;
            result.Code = "504";
            result.Name = "result.DataNotUpdate";
            result.Message = "未更新任何数据";
            return result;
        }
    }
}
