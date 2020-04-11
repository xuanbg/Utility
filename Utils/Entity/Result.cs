using System;

namespace Insight.Utils.Entity
{
    /// <summary>
    /// Json接口返回值
    /// </summary>
    public class Result<T>
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 可选参数
        /// </summary>
        public object option { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int total => Convert.ToInt32(option);

        /// <summary>
        /// 初始化为未知错误（500）
        /// </summary>
        public Result()
        {
            code = 500;
            message = "未知错误";
        }
    }
}
