namespace Insight.Utils.ExcelHelper.Enum
{
    public enum Policy
    {
        /// <summary>
        /// 默认值
        /// </summary>
        NONE = 0,

        /// <summary>
        /// 必需的(Sheet中必须包含该列)
        /// </summary>
        REQUIRED = 1,

        /// <summary>
        /// 可忽略的(不会被导出)
        /// </summary>
        IGNORABLE = 2
    }
}