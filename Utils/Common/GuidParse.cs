using System;

namespace Insight.Utils.Common
{
    public class GuidParse
    {
        /// <summary>
        /// 是否转换成功
        /// </summary>
        public bool Successful = true;

        /// <summary>
        /// 转换成功后的结果
        /// </summary>
        public Guid? Result;

        /// <summary>
        /// 将一个字符串转换为GUID
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        public GuidParse(string str)
        {
            if (string.IsNullOrEmpty(str)) return;

            Guid guid;
            Successful = Guid.TryParse(str, out guid);
            if (!Successful) return;

            Result = guid;
        }
    }
}
