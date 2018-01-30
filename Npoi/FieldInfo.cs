using Insight.Utils.Npoi.Enum;

namespace Insight.Utils.Npoi
{
    public class FieldInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string fieldName { get; set; }

        /// <summary>
        /// 字段类型名称
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// 列名称
        /// </summary>
        public string columnName { get; set; }

        /// <summary>
        /// 列日期格式
        /// </summary>
        public string dateFormat { get; set; }

        /// <summary>
        /// 列策略
        /// </summary>
        public Policy columnPolicy { get; set; }
    }
}
