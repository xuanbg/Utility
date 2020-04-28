using System;

namespace Insight.Base.BaseForm.Entities
{
    /// <summary>
    /// 回调事件参数类
    /// </summary>
    public class CallbackEventArgs : EventArgs
    {
        /// <summary>
        /// 回调方法名称
        /// </summary>
        public string methodName { get; }

        /// <summary>
        /// 
        /// </summary>
        public object[] param { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="methodName">回调方法名称</param>
        /// <param name="param">回调参数</param>
        public CallbackEventArgs(string methodName, object[] param)
        {
            this.methodName = methodName;
            this.param = param;
        }
    }
}