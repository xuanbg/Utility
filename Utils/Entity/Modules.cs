using System;
using System.Collections.Generic;

namespace Insight.Utils.Entity
{
    /// <summary>
    /// 导航栏数据
    /// </summary>
    public class Navigation
    {
        /// <summary>
        /// ID，唯一标识
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 上级导航ID
        /// </summary>
        public string parentId { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 导航名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string alias { get; set; }

        /// <summary>
        /// 模块url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 图标url
        /// </summary>
        public string iconurl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public byte[] icon { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 是否默认启动：0、否；1、是
        /// </summary>
        public bool isDefault { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 功能集合
        /// </summary>
        public List<Function> funcs { get; set; }
    }

    /// <summary>
    /// 模块工具栏按钮
    /// </summary>
    public class Function
    {
        /// <summary>
        /// ID，唯一标识
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 导航ID
        /// </summary>
        public string navigatorId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string alias { get; set; }

        /// <summary>
        /// 接口路由
        /// </summary>
        public string routes { get; set; }

        /// <summary>
        /// 功能url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 图标url
        /// </summary>
        public string iconurl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public byte[] icon { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 是否开始分组：0、否；1、是
        /// </summary>
        public bool isBegin { get; set; }

        /// <summary>
        /// 是否显示文字：0、隐藏；1、显示
        /// </summary>
        public bool isShowText { get; set; }

        /// <summary>
        /// 是否可见：0、不可见；1、可见
        /// </summary>
        public bool isVisible { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 是否启用状态
        /// </summary>
        public bool permit { get; set; }
    }

    /// <summary>
    /// 业务模块选项
    /// </summary>
    public class ModuleParam
    {
        /// <summary>
        /// ID，唯一标识
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string tenantId { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>
        public string moduleId { get; set; }

        /// <summary>
        /// 选项ID
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 选项参数值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 生效机构ID
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
    }
}
