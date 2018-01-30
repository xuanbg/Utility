using System;
using Insight.Utils.Npoi.Attribute;
using static Insight.Utils.Npoi.Enum.Policy;

namespace Insight.Utils.Npoi
{
    public class Test
    {
        [ColumnName(Ignorable)]
        public string id { get; set; }

        [ColumnName("名称")]
        public string name { get; set; }

        [ColumnName("更新时间", dateFormat = "yyyy-MM-dd hh:mm:ss", policy = Required)]
        public DateTime updateTime { get; set; }
    }
}
