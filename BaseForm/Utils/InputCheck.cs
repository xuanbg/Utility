using System;

namespace Insight.Base.BaseForm.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InputCheck : Attribute
    {
        /// <summary>
        /// 不能为空字符串
        /// </summary>
        public bool notEmpty { get; }

        /// <summary>
        /// 自定义错误消息
        /// </summary>
        public string message { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">自定义错误消息</param>
        public InputCheck(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="notEmpty">不能为空字符串</param>
        /// <param name="message">自定义错误消息</param>
        public InputCheck(bool notEmpty, string message)
        {
            this.notEmpty = notEmpty;
            this.message = message;
        }
    }
}