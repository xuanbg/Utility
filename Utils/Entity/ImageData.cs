using System;

namespace Insight.Utils.Entity
{
    public class ImageData
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
        /// 影像类型
        /// </summary>
        public int imageType { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string expand { get; set; }

        /// <summary>
        /// 保密等级
        /// </summary>
        public string secrecyDegree { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int? pages { get; set; }

        /// <summary>
        /// 文件字节数
        /// </summary>
        public long? size { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 影像内容
        /// </summary>
        public byte[] image { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 是否失效
        /// </summary>
        public bool isInvalid { get; set; }

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
}
