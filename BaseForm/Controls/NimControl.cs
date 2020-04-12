using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Common;
using Insight.Utils.Controls.Nim;
using NIM;
using NIM.User;

namespace Insight.Utils.Controls
{
    public partial class NimControl : XtraUserControl
    {
        private ClientAPI.LoginResultDelegate handleResult;
        private NimChatControl chat;

        /// <summary>  
        /// 登录成功后，通知更新登录状态
        /// </summary>  
        public event LoginHandle loginHandle;

        /// <summary>
        /// 表示将处理当前登录事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void LoginHandle(object sender, LoginEventArgs e);

        /// <summary>
        /// 云信AppKey
        /// </summary>
        public string appKey;

        /// <summary>
        /// 发送者云信ID
        /// </summary>
        public string myId;

        /// <summary>
        /// 发送者头像
        /// </summary>
        public Image myHead { private get; set; } = Util.getImage("icons/head.png");

        /// <summary>
        /// 构造方法
        /// </summary>
        public NimControl()
        {
            InitializeComponent();
        }

        public void initChat(string targetId)
        {
            // 在会话集合查找会话，如存在则激活该会话
            var control = pceChat.Controls[targetId];
            if (chat != null && control != null)
            {
                if (chat == control) return;

                control.Visible = true;
                chat.Visible = false;
                chat = (NimChatControl) control;

                return;
            }

            // 打开新会话
            chat = new NimChatControl
            {
                Name = targetId,
                TabIndex = 0,
                Location = new Point(0, 0),
                Dock = DockStyle.Fill,
                myId = myId,
                targetId = targetId,
                myHead = myHead
            };
            pceChat.Controls.Add(chat);
            chat.init();
        }

        /// <summary>
        /// 执行登录
        /// </summary>
        public void login()
        {
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
                        loginHandle?.Invoke(this, new LoginEventArgs(true));

                        Messages.showMessage("云信客户端登录成功！");
                        UserAPI.GetUserNameCard(new List<string> { myId }, ret =>
                        {
                            if (ret == null || !ret.Any()) return;

                            var headUrl = ret[0].IconUrl;
                            if (string.IsNullOrEmpty(headUrl)) return;

                            myHead = NimUtil.getHeadImage(headUrl);
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

    /// <summary>
    /// 登录事件参数类
    /// </summary>
    public class LoginEventArgs : EventArgs
    {
        /// <summary>
        /// 登录状态
        /// </summary>
        public bool status { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">登录状态</param>
        public LoginEventArgs(bool status)
        {
            this.status = status;
        }
    }

}
