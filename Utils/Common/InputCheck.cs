using System.Collections.Generic;
using System.Windows.Forms;

namespace Insight.Utils.Common
{
    public class InputCheck
    {
        /// <summary>
        /// 检查结果
        /// </summary>
        public bool result = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">要检查的对象集合</param>
        public InputCheck(IEnumerable<InputItem> items)
        {
            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.key)) continue;

                Messages.ShowWarning(item.message);
                item.control.Focus();

                result = false;
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
        /// 窗体控件
        /// </summary>
        public Control control { get; set; }

        /// <summary>
        /// 检查内容
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string message { get; set; }
    }
}