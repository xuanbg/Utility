using System;

namespace Insight.Utils.Npoi
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AliasAttribute : Attribute
    {
        /// <summary>
        /// 属性别名
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="alias">别名</param>
        public AliasAttribute(string alias)
        {
            Alias = alias;
        }
    }
}
