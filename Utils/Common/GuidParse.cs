using System;
using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public class GuidParse
    {
        /// <summary>
        /// 是否转换成功
        /// </summary>
        public Result Result = new Result();

        /// <summary>
        /// 转换成功后的结果
        /// </summary>
        public Guid? Guid;

        /// <summary>
        /// 转换后的GUID值
        /// </summary>
        public Guid Value;

        /// <summary>
        /// 将一个字符串转换为GUID
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        public GuidParse(string str)
        {
            Result.Success();
            if (string.IsNullOrEmpty(str)) return;

            if (System.Guid.TryParse(str, out Value))
            {
                Guid = Value;
                return;
            }

            Result.InvalidGuid();
        }
    }
}
