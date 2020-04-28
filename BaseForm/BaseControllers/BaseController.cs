using System.Windows.Forms;
using Insight.Utils.Common;

namespace Insight.Base.BaseForm.BaseControllers
{
    public class BaseController
    {
        /// <summary>
        /// 按钮点击事件路由
        /// </summary>
        /// <param name="methodName">回调方法名称</param>
        /// <param name="param">回调参数</param>
        protected void buttonClick(string methodName, object[] param = null)
        {
            var method = GetType().GetMethod(methodName);
            if (method == null)
                Messages.showError("对不起，该功能尚未实现！");
            else
                method.Invoke(this, param);
        }

        /// <summary>
        /// MDI窗体是否已存在
        /// </summary>
        /// <param name="name">窗体名称</param>
        /// <returns>是否已存在</returns>
        protected static bool existForm(string name)
        {
            var form = Application.OpenForms[name];
            if (form == null) return false;

            form.Activate();
            return true;
        }
    }
}
