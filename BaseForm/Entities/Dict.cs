using System.Collections.Generic;

namespace Insight.Base.BaseForm.Entities
{
    public class DictDto
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string appName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 键值集合
        /// </summary>
        public List<DictKeyDto> keys { get; set; } = new List<DictKeyDto>();
    }

    public class DictKeyDto
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 字典ID
        /// </summary>
        public string dictId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string tenantId { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 键值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public object extend { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}