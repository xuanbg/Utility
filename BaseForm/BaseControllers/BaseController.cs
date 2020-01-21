﻿using Insight.Utils.Common;

namespace Insight.Utils.BaseControllers
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
            {
                Messages.showError("对不起，该功能尚未实现！");
            }
            else
            {
                method.Invoke(this, param);
            }
        }
    }
}