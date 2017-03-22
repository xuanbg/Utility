using System.Collections.Generic;

namespace Insight.Utils.Entity
{
    public class TabList<T>
    {
        /// <summary>
        /// 列表总数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 列表对象集合
        /// </summary>
        public List<T> items { get; set; }
    }
}