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
        /// <param name="data">承载的数据（可选）</param>
        public void Success(string data = null)
        {
            Successful = true;
            Code = "200";
            Name = "OK";
            Message = "接口调用成功";
            Data = data;
        }

        /// <summary>
        /// 接口调用成功（200）
        /// </summary>
        /// <param name="data">承载的数据</param>
        public void Success(object data)
        {
            Successful = true;
            Code = "200";
            Name = "OK";
            Message = "接口调用成功";
            Data = Util.Serialize(data);
        }

        /// <summary>
        /// 资源创建成功（201）
        /// </summary>
        /// <param name="data">承载的数据（可选）</param>
        public void Created(string data = null)
        {
            Successful = true;
            Code = "201";
            Name = "Created";
            Message = "资源创建成功";
            Data = data;
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
        }

        /// <summary>
        /// 请求参数错误（400）
        /// </summary>
        /// <param name="data">错误详细信息（可选）</param>
        public void BadRequest(string data = null)
        {
            Successful = false;
            Code = "400";
            Name = "BadRequest";
            Message = "请求参数错误";
            Data = data;
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
        public void TooFrequent(string data)
        {
            Successful = false;
            Code = "402";
            Name = "CallInterfaceTooFrequent";
            Message = "调用接口过于频繁";
            Data = data;
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
        /// 版本不兼容（407）
        /// </summary>
        public void Incompatible()
        {
            Successful = false;
            Code = "405";
            Name = "IncompatibleVersions";
            Message = "客户端版本不兼容";
        }

        /// <summary>
        /// 转换为Guid失败（419）
        /// </summary>
        public void InvalidGuid()
        {
            Successful = false;
            Code = "406";
            Name = "InvalidGUID";
            Message = "转换为Guid失败";
        }

        /// <summary>
        /// 未更新任何数据（420）
        /// </summary>
        public void NotUpdate()
        {
            Successful = false;
            Code = "407";
            Name = "DataNotUpdate";
            Message = "未更新任何数据";
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
