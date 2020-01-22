using System;
using System.Collections.Generic;
using Insight.Utils.Common;

namespace Insight.Utils.Entity
{
    /// <summary>
    /// 分类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Catalog<T> : CatalogBase
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 焦点行
        /// </summary>
        public int handle { get; set; }

        /// <summary>
        /// 成员总数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 是否预置：0、自定；1、预置
        /// </summary>
        public bool isBuiltin { get; set; }

        /// <summary>
        /// 是否可见：0、不可见；1、可见
        /// </summary>
        public bool isVisible { get; set; }

        /// <summary>
        /// 分类成员集合
        /// </summary>
        public List<T> members { get; set; }

        /// <summary>
        /// 创建部门ID
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createdTime { get; set; }
    }

    /// <summary>
    /// 分类基类
    /// </summary>
    public class CatalogBase
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
        [InputCheck(true, "分类名称不能为空！请输入正确的名称。")]
        public string name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string alias { get; set; }
    }
}
