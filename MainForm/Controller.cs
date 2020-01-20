﻿using System.Diagnostics;
using System.Windows.Forms;
using Insight.Utils.BaseControllers;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.MainForm.Dtos;
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
            mainModel.callbackEvent += (sender, args) => buttonClick(args.methodName, args.param);

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
                if (Setting.needChangePw) changPassword(login.view.PassWordInput.Text);
            };

            // 显示登录界面
            view.Show();
            view.Refresh();

            login.initUserName();
        }

        /// <summary>
        /// 点击菜单项：修改密码，弹出修改密码对话框
        /// </summary>
        /// <param name="password">原密码</param>
        public void changPassword(string password)
        {
            var dto = new PasswordDto {old = password};
            var model = new ChangPwModel("修改密码", dto);
            model.callbackEvent += (sender, args) =>
            {
                var data = (PasswordDto)args.param[0];
                if (!Model.changPassword(data)) return;

                model.close();
            };

            model.showDialog();
        }

        /// <summary>
        /// 点击菜单项：锁定，弹出解锁对话框
        /// </summary>
        public void lockWindow()
        {
            var model = new LockModel("解除屏幕锁定");
            model.showDialog();
        }

        /// <summary>
        /// 点击菜单项：打印机设置，打开打印机设置对话框
        /// </summary>
        public void printSet()
        {
            var model = new PrintModel("设置打印机");
            model.showDialog();
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
    }
}
