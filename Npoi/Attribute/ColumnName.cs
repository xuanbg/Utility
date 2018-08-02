using System;
using Insight.Utils.ExcelHelper.Enum;

namespace Insight.Utils.ExcelHelper.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnName : System.Attribute
    {
        /// <summary>
        /// 属性/字段名称
        /// </summary>
        public string name { get; }

        /// <summary>
        /// 时间/日期格式(默认为"yyyy-MM-dd")
        /// </summary>
        public string dateFormat { get; set; } = "yyyy-MM-dd";

        /// <summary>
        /// 导入/导出策略
        /// </summary>
        public Policy policy { get; set; } = Policy.None;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">属性/字段名称</param>
        public ColumnName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="policy">导入/导出策略</param>
        public ColumnName(Policy policy)
        {
            this.policy = policy;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">属性/字段名称</param>
        /// <param name="dateFormat">时间/日期格式</param>
        public ColumnName(string name, string dateFormat)
        {
            this.name = name;
            this.dateFormat = dateFormat;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">属性/字段名称</param>
        /// <param name="policy">导入/导出策略</param>
        public ColumnName(string name, Policy policy)
        {
            this.name = name;
            this.policy = policy;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">属性/字段名称</param>
        /// <param name="dateFormat">时间/日期格式</param>
        /// <param name="policy">导入/导出策略</param>
        public ColumnName(string name, string dateFormat, Policy policy)
        {
            this.name = name;
            this.policy = policy;
            this.dateFormat = dateFormat;
        }
    }
}