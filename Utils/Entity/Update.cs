using System.Collections.Generic;

namespace Insight.Utils.Entity
{
    public class Update
    {
        /// <summary>
        /// 是否强制更新
        /// </summary>
        public bool update { get; set; }

        /// <summary>
        /// 更新提示
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 版本更新列表
        /// </summary>
        public List<FileVersion> data { get; set; }
    }

    public class FileVersion
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string file { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 本地相对路径
        /// </summary>
        public string localPath { get; set; }
    }
}