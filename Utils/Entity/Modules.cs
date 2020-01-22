using System;
using System.Collections.Generic;

namespace Insight.Utils.Entity
{
    /// <summary>
    /// 模块信息对象实体
    /// </summary>
    public class ModuleDto
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
        /// 导航级别
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 导航名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 业务模块信息
        /// </summary>
        public ModuleInfo moduleInfo { get; set; }

        /// <summary>
        /// 功能集合
        /// </summary>
        public List<FunctionDto> functions { get; set; }
    }

    public class ModuleInfo
    {
        /// <summary>
        /// 图标url
        /// </summary>
        public string iconUrl { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string module { get; set; }

        /// <summary>
        /// 模块文件
        /// </summary>
        public string file { get; set; }

        /// <summary>
        /// 是否默认启动
        /// </summary>
        public bool? autoLoad { get; set; }

        /// <summary>
        /// 是否拥有选项
        /// </summary>
        public bool? hasParams { get; set; }
    }

    /// <summary>
    /// 模块工具栏按钮
    /// </summary>
    public class FunctionDto
    {
        /// <summary>
        /// ID，唯一标识
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 导航ID
        /// </summary>
        public string navId { get; set; }

        /// <summary>
        /// 功能类型
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        public string authCodes { get; set; }
        
        /// <summary>
        /// 功能图标信息
        /// </summary>
        public FuncInfo funcInfo { get; set; }

        /// <summary>
        /// 是否授权
        /// </summary>
        public bool permit { get; set; }
    }

    /// <summary>
    /// 图标信息
    /// </summary>
    public class FuncInfo
    {
        /// <summary>
        /// 方法名称
        /// </summary>
        public string method { get; set; }

        /// <summary>
        /// 图标url
        /// </summary>
        public string iconUrl { get; set; }

        /// <summary>
        /// 是否开始分组
        /// </summary>
        public bool beginGroup { get; set; }

        /// <summary>
        /// 是否隐藏文字
        /// </summary>
        public bool hideText { get; set; }
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
}
