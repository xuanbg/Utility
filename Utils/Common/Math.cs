using System;

namespace Insight.Utils.Common
{
    public static class MathTool
    {
        /// <summary>
        /// 向下取整
        /// </summary>
        /// <param name="val">输入值</param>
        /// <param name="post">小数位数</param>
        /// <returns>返回值</returns>
        public static decimal floor(decimal val, int post)
        {
            var pow = (decimal) Math.Pow(10, post);
            return Math.Floor(val * pow) / pow;
        }

        /// <summary>
        /// 向上取整
        /// </summary>
        /// <param name="val">输入值</param>
        /// <param name="post">小数位数</param>
        /// <returns>返回值</returns>
        public static decimal ceiling(decimal val, int post)
        {
            var pow = (decimal) Math.Pow(10, post);
            return Math.Ceiling(val * pow) / pow;
        }

    }
}
