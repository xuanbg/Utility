
using Insight.Utils.Common;

namespace Insight.Utils.Entity
{
    /// <summary>
    /// Json接口返回值
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool Successful { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 错误名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据集总数
        /// </summary>
        public int? Total { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 初始化为未知错误（500）
        /// </summary>
        public Result()
        {
            Code = "500";
            Name = "UnknownError";
            Message = "未知错误";
        }

        #region 接口返回信息

        /// <summary>
        /// 接口调用成功（200）
        /// </summary>
        /// <param name="data">承载的数据</param>
        /// <param name="total">数据集总数</param>
        public void Success(object data = null, int? total = null)
        {
            Successful = true;
            Code = "200";
            Name = "OK";
            Message = "接口调用成功";
            Total = total;
            Data = Util.Serialize(data ?? true);
        }

        /// <summary>
        /// 资源创建成功（201）
        /// </summary>
        /// <param name="data">承载的数据</param>
        public void Created(object data)
        {
            Successful = true;
            Code = "201";
            Name = "Created";
            Message = "资源创建成功";
            Data = Util.Serialize(data);
        }

        /// <summary>
        /// 无可用内容（204）
        /// </summary>
        public void NoContent()
        {
            Successful = true;
            Code = "204";
            Name = "NoContent";
            Message = "无可用内容";
            Total = 0;
            Data = "[]";
        }

        /// <summary>
        /// 无需刷新（205）
        /// </summary>
        public void WithoutRefresh()
        {
            Successful = true;
            Code = "205";
            Name = "WithoutRefresh";
            Message = "尚未过期，无需刷新";
        }

        /// <summary>
        /// 请求参数错误（400）
        /// </summary>
        /// <param name="data">错误详细信息（可选）</param>
        public void BadRequest(object data = null)
        {
            Successful = false;
            Code = "400";
            Name = "BadRequest";
            Message = $"请求参数错误{"：" + data}";
        }

        /// <summary>
        /// 身份验证失败（401）
        /// </summary>
        public void InvalidAuth()
        {
            Successful = false;
            Code = "401";
            Name = "InvalidAuthenticationInfo";
            Message = "提供的身份验证信息不正确";
        }

        /// <summary>
        /// 调用接口过于频繁（402）
        /// </summary>
        /// <param name="data"></param>
        public void TooFrequent(object data)
        {
            Successful = false;
            Code = "402";
            Name = "CallInterfaceTooFrequent";
            Message = "调用接口过于频繁";
        }

        /// <summary>
        /// 用户未取得授权（403）
        /// </summary>
        public void Forbidden()
        {
            Successful = false;
            Code = "403";
            Name = "Forbidden";
            Message = "当前用户未取得授权";
        }

        /// <summary>
        /// 指定的资源不存在（404）
        /// </summary>
        public void NotFound()
        {
            Successful = false;
            Code = "404";
            Name = "ResourceNotFound";
            Message = "指定的资源不存在";
        }

        /// <summary>
        /// AccessToken已过期（405）
        /// </summary>
        public void Expired()
        {
            Successful = false;
            Code = "405";
            Name = "AccessTokenExpired";
            Message = "AccessToken已过期";
        }

        /// <summary>
        /// AccessToken已失效（406）
        /// </summary>
        public void Failured()
        {
            Successful = false;
            Code = "406";
            Name = "AccessTokenFailured";
            Message = "AccessToken已失效";
        }

        /// <summary>
        /// 许可证数量不足（407）
        /// </summary>
        public void InsufficientLicenses()
        {
            Successful = false;
            Code = "407";
            Name = "InsufficientNumberOfLicenses";
            Message = "许可证数量不足";
        }

        /// <summary>
        /// 账号已锁定（408）
        /// </summary>
        public void AccountIsBlocked()
        {
            Successful = false;
            Code = "408";
            Name = "AccountIsBlocked";
            Message = "账号已锁定";
        }

        /// <summary>
        /// 用户已存在（409）
        /// </summary>
        public void AccountExists()
        {
            Successful = false;
            Code = "409";
            Name = "AccountAlreadyExists";
            Message = "用户已存在";
        }

        /// <summary>
        /// 用户被禁止登录（410）
        /// </summary>
        public void Disabled()
        {
            Successful = false;
            Code = "410";
            Name = "AccountIsDisabled";
            Message = "当前用户被禁止登录";
        }

        /// <summary>
        /// 转换为Guid失败（420）
        /// </summary>
        public void InvalidGuid()
        {
            Successful = false;
            Code = "420";
            Name = "InvalidGUID";
            Message = "转换为Guid失败";
        }

        /// <summary>
        /// 未更新任何数据（421）
        /// </summary>
        public void NotUpdate()
        {
            Successful = false;
            Code = "421";
            Name = "DataNotUpdate";
            Message = "未更新任何数据";
        }

        /// <summary>
        /// 未找到指定的文件（430）
        /// </summary>
        public void FileNotExists()
        {
            Successful = false;
            Code = "430";
            Name = "FileNotExists";
            Message = "未找到指定的文件";
        }

        /// <summary>
        /// 未找到指定的Sheet（431）
        /// </summary>
        public void SheetNotExists()
        {
            Successful = false;
            Code = "431";
            Name = "SheetNotExists";
            Message = "未找到指定的Sheet";
        }

        /// <summary>
        /// 未从文件中读取任何数据行（432）
        /// </summary>
        public void NoRowsRead()
        {
            Successful = false;
            Code = "432";
            Name = "NoRowsRead";
            Message = "未从文件中读取任何数据行";
        }

        /// <summary>
        /// Excel格式不正确（433）
        /// </summary>
        public void IncorrectExcelFormat()
        {
            Successful = false;
            Code = "433";
            Name = "IncorrectExcelFormat";
            Message = "Excel格式不正确";
        }

        /// <summary>
        /// 写数据库失败（501）
        /// </summary>
        public void DataBaseError()
        {
            Successful = false;
            Code = "501";
            Name = "DataBaseError";
            Message = "写数据库失败";
        }

        /// <summary>
        /// 数据已存在（502）
        /// </summary>
        public void DataAlreadyExists()
        {
            Successful = false;
            Code = "502";
            Name = "DataAlreadyExists";
            Message = "数据已存在";
        }

        /// <summary>
        /// 当前服务不可用（503）
        /// </summary>
        public void ServiceUnavailable()
        {
            Successful = false;
            Code = "503";
            Name = "ServiceUnavailable";
            Message = "当前服务不可用";
        }

        #endregion

    }
}
