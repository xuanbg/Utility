using System;

namespace Insight.Base.BaseForm.Entities
{
    /// <summary>
    /// 焦点行改变事件参数类
    /// </summary>
    public class RowHandleEventArgs : EventArgs
    {
        /// <summary>
        /// Row handle
        /// </summary>
        public int rowHandle { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="handel">RowsPerPage</param>
        public RowHandleEventArgs(int handel)
        {
            rowHandle = handel;
        }
    }
}