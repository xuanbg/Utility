using System.Diagnostics;
using System.Windows.Forms;
using Insight.Utils.BaseControllers;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.MainForm.ViewModels;

namespace Insight.Utils.MainForm
{
    public class Controller : BaseController
    {
        public readonly MainModel mainModel;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Controller()
        {
            // 构造主窗体并显示
            mainModel = new MainModel(Setting.appName);
            mainModel.callbackEvent += (sender, args) => buttonClick(args.methodName);

            login();
        }

        /// <summary>
        /// 登录
        /// </summary>
        private void login()
        {
            // 构造登录Model并订阅登录窗体事件
            var login = new LoginModel();
            var view = login.view;
            view.CloseButton.Click += (sender, args) => Application.Exit();

            view.SetButton.Click += (sender, args) => 
            {
                var set = new SetModel("服务器设置");
                    login.initUserName();
                    set.save();
            };

            view.LoginButton.Click += (sender, args) =>
            {
                if (!login.login()) return;

                // 显示等待界面
                var waiting = new WaitingModel();
                waiting.view.Show();
                waiting.view.Refresh();
                login.view.Close();

                mainModel.showMainWindow(Model.getNavigators());
                waiting.view.Close();

                mainModel.autoLoad();
                if (Setting.needChangePw) changPassword(true);
            };

            // 显示登录界面
            view.Show();
            view.Refresh();

            login.initUserName();
        }

        /// <summary>
        /// 点击菜单项：修改密码，弹出修改密码对话框
        /// </summary>
        /// <param name="isFirst"></param>
        public void changPassword(bool isFirst = false)
        {
            var model = new ChangPwModel("修改密码");
            model.init(isFirst ? "123456" : null);
        }

        /// <summary>
        /// 点击菜单项：锁定，弹出解锁对话框
        /// </summary>
        public void lockWindow()
        {
            var model = new LockModel("屏幕解锁");

            model.init();
        }

        /// <summary>
        /// 点击菜单项：注销，弹出询问对话框，确认注销后重启应用程序
        /// </summary>
        public void logout()
        {
            const string msg = "注销用户将导致当前未完成的输入内容丢失！\r\n您确定要注销吗？";
            if (!logoutConfirm(msg)) return;

            Application.Restart();
        }

        /// <summary>
        /// 退出系统前保存当前应用的皮肤
        /// </summary>
        public void exit()
        {
            const string msg = "退出应用程序将导致当前未完成的输入内容丢失！\r\n您确定要退出吗？";
            if (!logoutConfirm(msg)) return;

            mainModel.saveLookAndFeel();
            Application.Exit();
        }

        /// <summary>
        /// 点击菜单项：打印机设置，打开打印机设置对话框
        /// </summary>
        public void printSet()
        {
            var model = new PrintModel();
            var view = model.view;

            view.Confirm.Click += (sender, args) =>
            {
                model.save();
            };

            view.ShowDialog();
        }

        /// <summary>
        /// 点击菜单项：检查更新，如有更新，提示是否更新
        /// </summary>
        public void update(bool confirm = true)
        {
            var model = new UpdateModel("检查更新");
                if (!model.restart) return;

                // 运行restart.bat重启应用程序
                Process.Start(model.createBat());
                Application.Exit();

            // 检查更新
            var count = model.checkUpdate();
            if (count == 0)
            {
                if (confirm) Messages.showMessage("当前无可用更新！");
                return;
            }

            var msg = $"当前有 {count} 个文件需要更新，是否立即更新？";
            if (confirm && !Messages.showConfirm(msg)) return;

        }

        /// <summary>
        /// 点击菜单项：关于，打开关于对话框
        /// </summary>
        public void about()
        {
            var model = new AboutModel("关于");

            model.showDialog();
        }

        /// <summary>
        /// 如注销用户失败，弹出询问对话框。
        /// </summary>
        /// <param name="msg">消息提示</param>
        private bool logoutConfirm(string msg)
        {
            if (!Messages.showConfirm(msg)) return false;

            Setting.tokenHelper.deleteToken();
            return true;
        }
    }
}
