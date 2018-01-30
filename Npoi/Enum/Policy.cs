namespace Insight.Utils.Npoi.Enum
{
    public enum Policy
    {
        /// <summary>
        /// 默认值
        /// </summary>
        None = 0,

        /// <summary>
        /// 必需的(Sheet中必须包含该列)
        /// </summary>
        Required = 1,

        /// <summary>
        /// 可忽略的(不会被导出)
        /// </summary>
        Ignorable = 2
    }
}