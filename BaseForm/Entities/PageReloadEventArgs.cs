using System;

namespace Insight.Base.BaseForm.Entities
{
    /// <summary>
    /// 页面重载事件参数类
    /// </summary>
    public class ReloadPageEventArgs : EventArgs
    {
        /// <summary>
        /// Current page
        /// </summary>
        public int page { get; }

        /// <summary>
        /// Page size
        /// </summary>
        public int size { get; }

        /// <summary>
        /// Row handle
        /// </summary>
        public int handle { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="size">Page size</param>
        /// <param name="handle">Row handle</param>
        public ReloadPageEventArgs(int page, int size, int handle)
        {
            this.page = page;
            this.size = size;
            this.handle = handle;
        }
    }
}