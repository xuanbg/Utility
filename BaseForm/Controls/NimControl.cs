using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Controls.Nim;
using NIM;
using NIM.User;

namespace Insight.Utils.Controls
{
    public partial class NimControl : XtraUserControl
    {
        private ClientAPI.LoginResultDelegate handleResult;
        private readonly string myId = Setting.userId.Replace("-", "");
        private string targetId = "12";

        /// <summary>
        /// 构造方法
        /// </summary>
        public NimControl()
        {
            InitializeComponent();
        }

        public void initChat()
        {
            nccChat.myId = myId;
            nccChat.targetId = targetId;
        }

        /// <summary>
        /// 执行登录
        /// </summary>
        public void login()
        {
            var appKey = Setting.appId.Replace("-", "");
            var password = Util.hash(myId);
            handleResult = handleLoginResult;

            if (!ClientAPI.Init(appKey, Application.StartupPath, null, null))
            {
                Messages.showError("云信客户端初始化失败");
                return;
            }

            ClientAPI.Login(appKey, myId, password, handleResult);
        }

        /// <summary>
        /// 登录结果处理
        /// </summary>
        /// <param name="result">登录结果</param>
        private void handleLoginResult(NIMLoginResult result)
        {
            void action()
            {
                if (result.LoginStep == NIMLoginStep.kNIMLoginStepLogin)
                {
                    if (result.Code == ResponseCode.kNIMResSuccess)
                    {
                        Messages.showMessage("云信客户端登录成功！");

                        UserAPI.GetUserNameCard(new List<string> { myId }, ret =>
                        {
                            if (ret == null || !ret.Any()) return;

                            var headUrl = ret[0].IconUrl;
                            if (string.IsNullOrEmpty(headUrl)) return;

                            nccChat.myHead = NimUtil.getHeadImage(headUrl);
                            nccChat.init();
                        });
                    }
                    else
                    {
                        ClientAPI.Logout(NIMLogoutType.kNIMLogoutChangeAccout, handleLogoutResult);
                    }
                }
            }

            Invoke((Action)action);
        }

        /// <summary>
        /// 登出结果处理
        /// </summary>
        /// <param name="result">登录结果</param>
        private void handleLogoutResult(NIMLogoutResult result)
        {
            Messages.showError($"云信客户端登录失败：{result.Code}");
        }
    }
}
