namespace Insight.Utils.Entity
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
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string alias { get; set; }
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
        public int nodeType { get; set; }

        /// <summary>
        /// 排序索引
        /// </summary>
        public int index { get; set; }

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
    /// ComboBox列表对象
    /// </summary>
    public class ComboBoxItem
    {

        private readonly int index;
        private readonly string name;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        public ComboBoxItem(int index, string name)
        {
            this.index = index;
            this.name = name;
        }

        /// <summary>
        /// 重写ToString()方法，返回Name值
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// 重写GetHashCode()，返回Index值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return index;
        }
    }
}
