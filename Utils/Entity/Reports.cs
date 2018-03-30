using System;
using System.Collections.Generic;

namespace Insight.Utils.Entity
{
    /// <summary>
    /// 报表
    /// </summary>
    public class ReportDef
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public string categoryId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string templateId { get; set; }

        /// <summary>
        /// 分期模式
        /// </summary>
        public int mode { get; set; }

        /// <summary>
        /// 延时(小时)
        /// </summary>
        public int delay { get; set; }

        /// <summary>
        /// 报表类型
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public string dataSource { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 创建部门ID
        /// </summary>
        public string creatorDeptId { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 报表分期
        /// </summary>
        public List<Period> periods { get; set; }

        /// <summary>
        /// 会计主体
        /// </summary>
        public List<Entity> entities { get; set; }

        /// <summary>
        /// 报表成员（角色）
        /// </summary>
        public List<Subscriber> members { get; set; }
    }

    /// <summary>
    /// 报表模板
    /// </summary>
    public class Template
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public string categoryId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 创建部门ID
        /// </summary>
        public string creatorDeptId { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
    }

    /// <summary>
    /// 分期规则
    /// </summary>
    public class Rule
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
        /// 周期类型
        /// </summary>
        public int cycleType { get; set; }

        /// <summary>
        /// 周期数
        /// </summary>
        public int? cycle { get; set; }

        /// <summary>
        /// 周期类型
        /// </summary>
        public string cycleInfo
        {
            get
            {
                var c = "";
                switch (cycleType)
                {
                    case 1:
                        c = "年";
                        break;
                    case 2:
                        c = "月";
                        break;
                    case 3:
                        c = "周";
                        break;
                    case 4:
                        c = "日";
                        break;
                }
                return cycle.HasValue || cycle > 0 ? $"{cycle} {c}" : null;
            }
        }

        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime? startTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 是否预置：0、自定；1、预置
        /// </summary>
        public bool isBuiltin { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 创建部门ID
        /// </summary>
        public string creatorDeptId { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
    }

    /// <summary>
    /// 报表实例
    /// </summary>
    public class Instance
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 报表ID
        /// </summary>
        public string reportId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public byte[] content { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
    }

    /// <summary>
    /// 报表分期
    /// </summary>
    public class Period
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 分期规则ID
        /// </summary>
        public string ruleId { get; set; }

        /// <summary>
        /// 分期规则名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 周期类型
        /// </summary>
        public string cycleInfo { get; set; }
    }

    /// <summary>
    /// 会计主体
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string orgId { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 报表订阅者
    /// </summary>
    public class Subscriber
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 会计主体ID
        /// </summary>
        public string entityId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string roleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }
    }
}