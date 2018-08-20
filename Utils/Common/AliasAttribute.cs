using System;

namespace Insight.Utils.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AliasAttribute : Attribute
    {
        /// <summary>
        /// 属性别名
        /// </summary>
        public string alias { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="alias">别名</param>
        public AliasAttribute(string alias)
        {
            this.alias = alias;
        }
    }
}
