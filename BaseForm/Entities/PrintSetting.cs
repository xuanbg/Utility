using System.Collections.Generic;
using FastReport;

namespace Insight.Base.BaseForm.Entities
{
    public class PrintSetting<T>
    {
        /// <summary>
        /// 使用的打印机
        /// </summary>
        public string printer { get; set; }

        /// <summary>
        /// 打印份数
        /// </summary>
        public int copies { get; set; } = 1;

        /// <summary>
        /// 打印模式
        /// </summary>
        public PrintMode printMode { get; set; } = PrintMode.Default;

        /// <summary>
        /// 合并模式
        /// </summary>
        public PagesOnSheet pagesOnSheet { get; set; } = PagesOnSheet.One;

        /// <summary>
        /// 报表模板
        /// </summary>
        public string template { get; set; }

        /// <summary>
        /// 报表数据集名称
        /// </summary>
        public string dataName { get; set; }

        /// <summary>
        /// 报表数据集
        /// </summary>
        public List<T> data { get; set; }

        /// <summary>
        /// 报表参数
        /// </summary>
        public Dictionary<string, object> parameter { get; set; }
    }
}
