using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insight.Utils.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyAlias:Attribute
    {
        /// <summary>
        /// 属性别名
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="alias">别名</param>
        public PropertyAlias(string alias)
        {
            Alias = alias;
        }
    }
}
