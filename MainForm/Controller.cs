using System.Diagnostics;
using System.Windows.Forms;
using Insight.Utils.Common;
using Insight.Utils.Controller;
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
            // 构造主窗体并显示
            manage = new MainModel();
            var view = manage.view;
            view.Show();
            view.Refresh();

            // 订阅主窗体菜单事件
            view.MubChangPassWord.ItemClick += (sender, args) => ChangPassword();
            view.MubLock.ItemClick += (sender, args) => Lock();
            view.MubLogout.ItemClick += (sender, args) => Logout();
            view.MubExit.ItemClick += (sender, args) => view.Close();
            view.MubPrintSet.ItemClick += (sender, args) => PrintSet();
            view.MubUpdate.ItemClick += (sender, args) => Update();
            view.MubAbout.ItemClick += (sender, args) => About();

            // 订阅主窗体事件
            view.Shown += (sender, args) =>
            {
                manage.needOpens.ForEach(manage.AddPageMdi);
            };
            view.Closing += (sender, args) => args.Cancel = manage.Logout();
            view.Closed += (sender, args) => Exit();

            // 订阅导航栏点击事件
            manage.links.ForEach(i => i.Item.LinkClicked += (sender, args) => manage.AddPageMdi(args.Link.Item.Tag));
        }

        /// <summary>
        /// 点击菜单项：修改密码，弹出修改密码对话框
        /// </summary>
        private void ChangPassword()
        {
            var changPw = new ChangPwModel();
            var view = changPw.view;

            SubCloseEvent(view);
            view.Confirm.Click += (sender, args) =>
            {
                if (!changPw.Save()) return;

                CloseDialog(view);
            };

            changPw.Init();
            view.ShowDialog();
        }

        /// <summary>
        /// 点击菜单项：锁定，弹出解锁对话框
        /// </summary>
        private void Lock()
        {
            var model = new LockModel();
            var view = model.view;

            view.Confirm.Click += (sender, args) =>
            {
                if (!model.Unlock()) return;

                CloseDialog(view);
            };

            model.Init();
            view.ShowDialog();
        }

        /// <summary>
        /// 点击菜单项：注销，弹出询问对话框，确认注销后重启应用程序
        /// </summary>
        private void Logout()
        {
            const string msg = "注销用户将导致当前未完成的输入内容丢失！\r\n您确定要注销吗？";
            if (!Messages.ShowConfirm(msg)) return;

            Application.Restart();
        }

        /// <summary>
        /// 退出系统前保存当前应用的皮肤
        /// </summary>
        private void Exit()
        {
            manage.SaveLookAndFeel();
            Application.Exit();
        }

        /// <summary>
        /// 点击菜单项：打印机设置，打开打印机设置对话框
        /// </summary>
        private void PrintSet()
        {
            var model = new PrintModel();
            var view = model.view;

            SubCloseEvent(view);
            view.Confirm.Click += (sender, args) =>
            {
                model.Save();
                CloseDialog(view);
            };

            view.ShowDialog();
        }


        /// <summary>
        /// 点击菜单项：检查更新，如有更新，提示是否更新
        /// </summary>
        private void Update(bool confirm = true)
        {
            var model = new UpdateModel();
            var view = model.view;

            view.Confirm.Click += (sender, args) =>
            {
                CloseDialog(view);
                if (!model.restart) return;

                // 运行restart.bat重启应用程序
                Process.Start(model.CreateBat());
                Application.Exit();
            };

            // 检查更新
            var count = model.CheckUpdate();
            if (count == 0)
            {
                if (confirm) Messages.ShowMessage("当前无可用更新！");
                return;
            }

            var msg = $"当前有 {count} 个文件需要更新，是否立即更新？";
            if (confirm && !Messages.ShowConfirm(msg)) return;

            view.ShowDialog();
        }

        /// <summary>
        /// 点击菜单项：关于，打开关于对话框
        /// </summary>
        private void About()
        {
            var model = new AboutModel();
            var view = model.view;

            SubCloseEvent(view, true);
            view.ShowDialog();
        }
    }
}
