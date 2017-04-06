using System.Collections.Generic;

namespace Insight.Utils.Common
{
    public class InputCheck
    {
        /// <summary>
        /// 检查结果
        /// </summary>
        public bool Result = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">要检查的对象集合</param>
        public InputCheck(IEnumerable<InputItem> items)
        {
            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.Key)) continue;

                Messages.ShowWarning(item.Message);
                Result = false;
                return;
            }
        }
    }

    /// <summary>
    /// 检查对象
    /// </summary>
    public class InputItem
    {
        /// <summary>
        /// 检查内容
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }
    }
}