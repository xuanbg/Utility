using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Controller;
using Insight.Utils.MainForm.Login.Models;
using Insight.Utils.MainForm.Models;

namespace Insight.Utils.MainForm
{
    public class Controller : BaseController<MainModel>
    {         
        /// <summary>
        /// 构造函数
        /// </summary>
        public Controller()
        {
            login();

            // 构造主窗体并显示
            manage = new MainModel();
            var view = manage.view;

            // 订阅主窗体菜单事件
            view.MubChangPassWord.ItemClick += (sender, args) => changPassword();
            view.MubLock.ItemClick += (sender, args) => lockWindow();
            view.MubLogout.ItemClick += (sender, args) => logout();
            view.MubExit.ItemClick += (sender, args) => view.Close();
            view.MubPrintSet.ItemClick += (sender, args) => printSet();
            view.MubUpdate.ItemClick += (sender, args) => update();
            view.MubAbout.ItemClick += (sender, args) => about();

            // 订阅主窗体事件
            view.Shown += (sender, args) =>
            {
                if (Setting.needChangePw) changPassword(true);

                manage.needOpens.ForEach(manage.addPageMdi);
            };
            view.Closing += (sender, args) => args.Cancel = manage.logout();
            view.Closed += (sender, args) => exit();
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
                var set = new SetModel();
                subCloseEvent(set.view);
                set.view.Confirm.Click += (s, a) =>
                {
                    login.initUserName();
                    set.save();

                    closeDialog(set.view);
                };

                set.view.ShowDialog();
            };

            view.LoginButton.Click += (sender, args) =>
            {
                if (!login.login()) return;

                // 显示等待界面
                var waiting = new WaitingModel();
                waiting.view.Show();
                waiting.view.Refresh();

                Thread.Sleep(800);
                login.view.Close();
                manage.show();

                waiting.view.Close();
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
        private void changPassword(bool isFirst = false)
        {
            var changPw = new ChangPwModel();
            var view = changPw.view;

            subCloseEvent(view);
            view.Confirm.Click += (sender, args) =>
            {
                if (!changPw.save()) return;

                closeDialog(view);
            };

            changPw.init(isFirst ? "123456" : null);
            view.ShowDialog();
        }

        /// <summary>
        /// 点击菜单项：锁定，弹出解锁对话框
        /// </summary>
        private void lockWindow()
        {
            var model = new LockModel();
            var view = model.view;

            view.Confirm.Click += (sender, args) =>
            {
                if (!model.unlock()) return;

                closeDialog(view);
            };

            model.init();
            view.ShowDialog();
        }

        /// <summary>
        /// 点击菜单项：注销，弹出询问对话框，确认注销后重启应用程序
        /// </summary>
        private static void logout()
        {
            const string msg = "注销用户将导致当前未完成的输入内容丢失！\r\n您确定要注销吗？";
            if (!Messages.showConfirm(msg)) return;

            Application.Restart();
        }

        /// <summary>
        /// 退出系统前保存当前应用的皮肤
        /// </summary>
        private void exit()
        {
            manage.saveLookAndFeel();
            Application.Exit();
        }

        /// <summary>
        /// 点击菜单项：打印机设置，打开打印机设置对话框
        /// </summary>
        private void printSet()
        {
            var model = new PrintModel();
            var view = model.view;

            subCloseEvent(view);
            view.Confirm.Click += (sender, args) =>
            {
                model.save();
                closeDialog(view);
            };

            view.ShowDialog();
        }


        /// <summary>
        /// 点击菜单项：检查更新，如有更新，提示是否更新
        /// </summary>
        private void update(bool confirm = true)
        {
            var model = new UpdateModel();
            var view = model.view;

            view.Confirm.Click += (sender, args) =>
            {
                closeDialog(view);
                if (!model.restart) return;

                // 运行restart.bat重启应用程序
                Process.Start(model.createBat());
                Application.Exit();
            };

            // 检查更新
            var count = model.checkUpdate();
            if (count == 0)
            {
                if (confirm) Messages.showMessage("当前无可用更新！");
                return;
            }

            var msg = $"当前有 {count} 个文件需要更新，是否立即更新？";
            if (confirm && !Messages.showConfirm(msg)) return;

            view.ShowDialog();
        }

        /// <summary>
        /// 点击菜单项：关于，打开关于对话框
        /// </summary>
        private void about()
        {
            var model = new AboutModel();
            var view = model.view;

            subCloseEvent(view, true);
            view.ShowDialog();
        }
    }
}
