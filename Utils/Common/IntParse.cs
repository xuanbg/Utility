using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public class IntParse
    {
        /// <summary>
        /// 是否转换成功
        /// </summary>
        public Result Result = new Result();

        /// <summary>
        /// 转换后的整数值
        /// </summary>
        public int Value;

        /// <summary>
        /// 将一个字符串转换为整数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        public IntParse(string str)
        {
            Result.BadRequest();
            if (!int.TryParse(str, out Value)) return;

            Result.Success();
        }
    }
}
