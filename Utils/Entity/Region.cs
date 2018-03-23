namespace Insight.Utils.Entity
{
    /// <summary>
    /// 行政区划基础数据实体类
    /// </summary>
    public class Region
    {
        /// <summary>
        /// 行政区划唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string parentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
    }
}
