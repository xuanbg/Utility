using System;
using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public class GuidParse
    {
        /// <summary>
        /// 转换返回结果
        /// </summary>
        public Result Result = new Result();

        /// <summary>
        /// 转换后的值
        /// </summary>
        public Guid Value;

        /// <summary>
        /// 转换后的GUID
        /// </summary>
        public Guid? Guid;

        /// <summary>
        /// 转换字符串为其它类型的值
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="allowNull">是否允许为空（默认不允许）</param>
        public GuidParse(string str, bool allowNull = false)
        {
            if (string.IsNullOrEmpty(str))
            {
                if (allowNull) Result.Success();
                else Result.InvalidGuid();

                return;
            }

            if (System.Guid.TryParse(str, out Value))
            {
                Result.Success();
                Guid = Value;
                return;
            }

            Result.InvalidGuid();
        }
    }
}
