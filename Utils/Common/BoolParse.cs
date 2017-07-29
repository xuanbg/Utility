using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public class BoolParse
    {
        /// <summary>
        /// 转换返回结果
        /// </summary>
        public Result<string> Result = new Result<string>();

        /// <summary>
        /// 转换后的值
        /// </summary>
        public bool Value;

        /// <summary>
        /// 转换后的GUID
        /// </summary>
        public bool? Bool;

        /// <summary>
        /// 转换字符串为其它类型的值
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="allowNull">是否允许为空（默认不允许）</param>
        public BoolParse(string str, bool allowNull = false)
        {
            if (string.IsNullOrEmpty(str))
            {
                if (allowNull) Result.Success();
                else Result.InvalidValue();

                return;
            }

            if (bool.TryParse(str, out Value))
            {
                Result.Success();
                Bool = Value;
                return;
            }

            Result.InvalidValue();
        }
    }
}
