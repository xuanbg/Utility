using Insight.Utils.Common;

namespace Insight.Utils.BaseControllers
{
    public class BaseController
    {
        /// <summary>
        /// 按钮点击事件路由
        /// </summary>
        /// <param name="action">功能操作</param>
        protected void buttonClick(string action)
        {
            var method = GetType().GetMethod(action);
            if (method == null)
            {
                Messages.showError("对不起，该功能尚未实现！");
            }
            else
            {
                method.Invoke(this, null);
            }
        }
    }
}
