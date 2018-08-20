using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Insight.Utils.Common
{
    public class RegexHelper
    {
        /// <summary>
        /// 清除包含'字符串
        /// </summary>
        public const string CleanString = @"[']";

        /// <summary>
        /// 验证字符串是否为字符begin-end之间
        /// </summary>
        public const string IsValidByte = @"^[A-Za-z0-9]{#0#,#1#}$";

        /// <summary>
        /// 验证字符串是否为年月日
        /// </summary>
        public const string IsValidDate = @"^2\d{3}-(?:0?[1-9]|1[0-2])-(?:0?[1-9]|[1-2]\d|3[0-1])(?:0?[1-9]|1\d|2[0-3]):(?:0?[1-9]|[1-5]\d):(?:0?[1-9]|[1-5]\d)$";

        /// <summary>
        /// 验证字符串是否为小数
        /// </summary>
        public const string IsValidDecimal = @"[0].\d{1,2}|[1]";

        /// <summary>
        /// 验证字符串是否为EMAIL
        /// </summary>
        public const string IsValidEmail = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        /// <summary>
        /// 验证字符串是否为IP
        /// </summary>
        public const string IsValidIp = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";

        /// <summary>
        /// 验证字符串是否为后缀名
        /// </summary>
        public const string IsValidPostfix = @"\.(?i:{0})$";

        /// <summary>
        /// 验证字符串是否为电话号码
        /// </summary>
        public const string IsValidTel = @"^\+?(\d{1,3}(-| ))?0?(\d{2,3}(-| ))?\d{2,4}(-| )?\d{3,4}(-\d{1,4})?$";

        /// <summary>
        /// 验证字符串是否为手机号码
        /// </summary>
        public const string IsValidMobile = @"^\+?(\d{1,3}(-| ))?1\d{2}(-| )?\d{4}(-| )?\d{4}$";

        /// <summary>
        /// 验证字符串是否为URL
        /// </summary>
        public const string IsValidUrl = @"^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$";

        #region 常用方法

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>替换后字符串</returns>
        public static string replaceInput(string input, string regex)
        {
            return Regex.Replace(input, regex, string.Empty);
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="replace">替换字符串</param>
        /// <returns>替换后字符串</returns>
        public static string replaceInput(string input, string regex, string replace)
        {
            return Regex.Replace(input, regex, replace);
        }

        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>是否验证通过</returns>
        public static bool checkInput(string input, string regex)
        {
            return Regex.IsMatch(input, regex);
        }

        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="begin">开始数字</param>
        /// <param name="end">结尾数字</param>
        /// <returns>是否验证通过</returns>
        public static bool validByte(string input, string regex, int begin, int end)
        {
            if (string.IsNullOrEmpty(regex)) return false;

            var rep = regex.Replace("#0#", begin.ToString(CultureInfo.InvariantCulture));
            rep = rep.Replace("#1#", end.ToString(CultureInfo.InvariantCulture));
            var ret = checkInput(input, rep);
            return ret;
        }

        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="fix">后缀名</param>
        /// <returns>是否验证通过</returns>
        public static bool validPostfix(string input, string regex, string fix)
        {
            var ret = string.Format(CultureInfo.InvariantCulture, regex, fix);
            return checkInput(input, ret);
        }

        #endregion

        #region CheckInput

        /// <summary>
        /// 验证18位身份证号码
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns>bool 是否合法</returns>
        public static bool checkIdCard18(string id)
        {
            if (long.TryParse(id.Remove(17), out var n) == false || n < Math.Pow(10, 16) || long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }

            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (!address.Contains(id.Remove(2)))
            {
                return false;//省份验证
            }

            var birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            if (DateTime.TryParse(birth, out _) == false)
            {
                return false;//生日验证
            }

            var arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            var wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            var ai = id.Remove(17).ToCharArray();
            var sum = 0;
            for (var i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }

            Math.DivRem(sum, 11, out var y);
            return arrVarifyCode[y] == id.Substring(17, 1).ToLower();
        }

        /// <summary>
        /// 验证15位身份证号码
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns>bool 是否合法</returns>
        public static bool checkIdCard15(string id)
        {
            if (long.TryParse(id, out long n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }

            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (!address.Contains(id.Remove(2)))
            {
                return false;//省份验证
            }

            var birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            return DateTime.TryParse(birth, out _);
        }

        /// <summary>
        /// 输入合法性验证
        /// </summary>
        /// <param name="type">输入类型</param>
        /// <param name="number">输入内容</param>
        /// <returns>bool 是否合法</returns>
        public static bool check(string type, string number)
        {
            if (string.IsNullOrEmpty(number)) return false;

            bool result;
            switch (type)
            {
                case "Tel":
                    result = checkInput(number, IsValidTel);
                    break;

                case "Mobile":
                    result = checkInput(number, IsValidMobile);
                    break;

                case "Fax":
                    result = checkInput(number, IsValidTel);
                    break;

                case "Email":
                    result = checkInput(number, IsValidEmail);
                    break;

                default:
                    result = true;
                    break;
            }
            return result;
        }

        #endregion

    }
}
