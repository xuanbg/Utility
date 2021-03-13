namespace Insight.Base.BaseForm.Entities
{

    /// <summary>
    /// LookUp列表对象
    /// </summary>
    public class LookUpMember
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }
    }

    /// <summary>
    /// TreeLookUp列表对象
    /// </summary>
    public class TreeLookUpMember
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string parentId { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public int type { get; set; }

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
        /// 描述
        /// </summary>
        public string remark { get; set; }
    }
}
