using System;
using Insight.Utils.ExcelHelper.Attribute;
using Insight.Utils.ExcelHelper.Enum;

namespace Insight.Utils.ExcelHelper
{
    public class Test
    {
        [ColumnName(Policy.Ignorable)]
        public string id { get; set; }

        [ColumnName("名称")]
        public string name { get; set; }

        [ColumnName("更新时间", dateFormat = "yyyy-MM-dd hh:mm:ss", policy = Policy.Required)]
        public DateTime updateTime { get; set; }
    }
}
