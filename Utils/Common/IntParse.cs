using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public class IntParse
    {
        /// <summary>
        /// 是否转换成功
        /// </summary>
        public Result<object> Result = new Result<object>();

        /// <summary>
        /// 转换成功后的结果
        /// </summary>
        public int? Int;

        /// <summary>
        /// 转换后的整数值
        /// </summary>
        public int Value;

        /// <summary>
        /// 将一个字符串转换为整数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="allowNull">是否允许为空（默认不允许）</param>
        public IntParse(string str, bool allowNull = false)
        {
            if (allowNull) Result.Success();
            else Result.InvalidGuid();

            if (int.TryParse(str, out Value))
            {
                Result.Success();
                Int = Value;
                return;
            }

            Result.InvalidValue();
        }
    }
}
