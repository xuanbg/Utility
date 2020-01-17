using System.Collections.Generic;
using System.Windows.Forms;
using Insight.Utils.Common;

namespace Insight.Utils.BaseViewModels
{
    public class BaseDialogModel<T>
    {
        private readonly List<InputItem> checkItems = new List<InputItem>();

        /// <summary>
        /// 对话框视图
        /// </summary>
        public T view;

        /// <summary>
        /// 设置一个输入检查对象
        /// </summary>
        /// <param name="control"></param>
        /// <param name="key">输入对象的值</param>
        /// <param name="message">错误消息</param>
        /// <param name="clear">是否清除集合</param>
        protected void setCheckItem(Control control, string key, string message, bool clear = false)
        {
            if (clear) checkItems.Clear();

            var item = new InputItem{control = control, key = key, message = message};
            checkItems.Add(item);
        }

        /// <summary>
        /// 设置多个输入检查对象
        /// </summary>
        /// <param name="items">输入检查对象集合</param>
        protected void setCheckItems(IEnumerable<InputItem> items)
        {
            checkItems.AddRange(items);
        }

        /// <summary>
        /// 检查输入检查对象是否都有值
        /// </summary>
        /// <returns>bool 对象是否都有值</returns>
        protected bool inputExamine()
        {
            return new InputCheck(checkItems).result;
        }
    }
}