using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraNavBar;
using FastReport.Utils;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Controller;
using Insight.Utils.Entity;
using Insight.Utils.MainForm.Models;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm
{
    public class Controller : BaseController
    {
        public readonly MainModel mainModel;
        public readonly MainWindow mainWindow;
        public readonly List<NavBarItemLink> links = new List<NavBarItemLink>();
        public readonly List<string> needOpens = new List<string>();

        private List<Navigation> navItems;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Controller()
        {
            // 构造主窗体并显示
            mainModel = new MainModel();
            mainWindow = new MainWindow
            {
                Text = Setting.appName,
                Icon = new Icon("logo.ico")
            };

            // 初始化界面
            Res.LoadLocale("Components\\Chinese (Simplified).frl");
            mainWindow.MyFeel.LookAndFeel.SkinName = Setting.lookAndFeel;
            mainWindow.StbTime.Caption = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mainWindow.StbServer.Caption = Setting.gateway;
            mainWindow.WindowState = SystemInformation.WorkingArea.Height > 755 ? FormWindowState.Normal : FormWindowState.Maximized;

            // 订阅主窗体菜单事件
            mainWindow.MubChangPassWord.ItemClick += (sender, args) => changPassword();
            mainWindow.MubLock.ItemClick += (sender, args) => lockWindow();
            mainWindow.MubLogout.ItemClick += (sender, args) => logout();
            mainWindow.MubExit.ItemClick += (sender, args) => mainWindow.Close();
            mainWindow.MubPrintSet.ItemClick += (sender, args) => printSet();
            mainWindow.MubUpdate.ItemClick += (sender, args) => update();
            mainWindow.MubAbout.ItemClick += (sender, args) => about();
            mainWindow.Closing += (sender, args) => args.Cancel = mainModel.logout();
            mainWindow.Closed += (sender, args) => exit();

            login();
            needOpens.ForEach(addPageMdi);
            if (Setting.needChangePw) changPassword(true);
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
                show();

                waiting.view.Close();
            };

            // 显示登录界面
            view.Show();
            view.Refresh();

            login.initUserName();
        }

        /// <summary>
        /// 主窗体初始化
        /// </summary>
        public void show()
        {
            mainWindow.StbDept.Caption = Setting.deptName;
            mainWindow.StbDept.Visibility = string.IsNullOrEmpty(Setting.deptName) ? BarItemVisibility.Never : BarItemVisibility.Always;
            mainWindow.StbUser.Caption = Setting.userName;

            initNavBar();
            links.ForEach(i => i.Item.LinkClicked += (sender, args) => addPageMdi(args.Link.Item.Tag.ToString()));

            mainWindow.Show();
        }

        /// <summary>
        /// 打开MDI子窗体
        /// </summary>
        /// <param name="name"></param>
        public void addPageMdi(string name)
        {
            var form = Application.OpenForms[name];
            if (form != null)
            {
                form.Activate();
                return;
            }

            var mod = navItems.Single(m => m.moduleInfo.module == name);
            var path = $"{Application.StartupPath}\\{mod.moduleInfo.file}";
            if (!File.Exists(path))
            {
                var msg = $"对不起，{mod.name}模块无法加载！\r\n未能发现{path}文件。";
                Messages.showError(msg);
                return;
            }

            mainWindow.Loading.ShowWaitForm();
            var asm = Assembly.LoadFrom(path);
            var type = asm.GetTypes().SingleOrDefault(i => i.FullName != null && i.FullName.EndsWith($"{mod.moduleInfo.module}.Controller"));
            if (type == null || string.IsNullOrEmpty(type.FullName))
            {
                mainWindow.Loading.CloseWaitForm();
                var msg = $"对不起，{mod.name}模块无法加载！\r\n您的应用程序中缺少相应组件。";
                Messages.showError(msg);

                return;
            }

            asm.CreateInstance(type.FullName, false, BindingFlags.Default, null, new object[] { mod }, CultureInfo.CurrentCulture, null);
            mainWindow.Loading.CloseWaitForm();
        }

        /// <summary>
        /// 初始化导航栏
        /// </summary>
        private void initNavBar()
        {
            var navigators = mainModel.getNavigators();
            navItems = navigators.Where(i => i.parentId != null).ToList();
            var groups = navigators.Where(i => i.parentId == null).ToList();
            var height = mainWindow.NavMain.Height;
            foreach (var g in groups)
            {
                var expand = false;
                var items = new List<NavBarItemLink>();
                foreach (var item in navItems.Where(i => i.parentId == g.id))
                {
                    if (item.moduleInfo.autoLoad ?? false)
                    {
                        expand = true;
                        needOpens.Add(item.moduleInfo.module);
                    }

                    var icon = Util.getImage(item.moduleInfo.iconUrl);
                    var navBarItem = new NavBarItem(item.name) { Tag = item.moduleInfo.module, SmallImage = icon };
                    items.Add(new NavBarItemLink(navBarItem));
                }

                var group = new NavBarGroup
                {
                    Caption = g.name,
                    Name = g.name,
                    SmallImage = Util.getImage(g.moduleInfo.iconUrl)
                };
                var count = links.Count + items.Count;
                group.Expanded = groups.Count * 55 + count * 32 < height || expand;
                group.ItemLinks.AddRange(items.ToArray());

                mainWindow.NavMain.Groups.Add(group);
                links.AddRange(items);
            }
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
            MainModel.saveLookAndFeel(mainWindow.MyFeel.LookAndFeel.SkinName);
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
