using System;

namespace Insight.Utils.Entity
{
    public class ClientFile
    {
        /// <summary>
        /// ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 子目录
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string fullPath { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updateTime { get; set; }
    }
}