using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public class DateParse
    {
        /// <summary>
        /// 是否转换成功
        /// </summary>
        public Result Result = new Result();

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
        public DateParse(string str)
        {
            Result.Success();
            if (string.IsNullOrEmpty(str)) return;

            if (System.DateTime.TryParse(str, out Value))
            {
                DateTime = Value;
                return;
            }

            Result.InvalidDateTime();
        }
    }
}