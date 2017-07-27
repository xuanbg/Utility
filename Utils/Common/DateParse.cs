using System;
using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public class DateParse
    {
        /// <summary>
        /// 是否转换成功
        /// </summary>
        public Result<string> Result = new Result<string>();

        /// <summary>
        /// 转换成功后的结果
        /// </summary>
        public DateTime? DateTime;

        /// <summary>
        /// 转换后的DateTime值
        /// </summary>
        public DateTime Value;

        /// <summary>
        /// 将一个字符串转换为DateTime
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="allowNull">是否允许为空（默认不允许）</param>
        public DateParse(string str, bool allowNull = false)
        {
            if (allowNull) Result.Success();
            else Result.InvalidGuid();

            if (string.IsNullOrEmpty(str)) return;

            if (System.DateTime.TryParse(str, out Value))
            {
                Result.Success();
                DateTime = Value;
                return;
            }

            Result.InvalidDateTime();
        }
    }
}