namespace Insight.Utils.Entity
{
    public class CategoryBses
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 父分类ID
        /// </summary>
        public string parentId { get; set; }

        /// <summary>
        /// 业务模块ID
        /// </summary>
        public string moduleId { get; set; }

        /// <summary>
        /// 排序索引
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string alias { get; set; }
    }
}
