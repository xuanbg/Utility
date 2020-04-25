using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraNavBar;
using FastReport.Utils;
using Insight.Utils.BaseViewModels;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.ViewModels
{
    public class MainModel : BaseModel<object, MainWindow>
    {
        private readonly List<string> opens = new List<string>();

        /// <summary>
        /// 模块信息集合
        /// </summary>
        public List<ModuleDto> navigators;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">窗体标题</param>
        public MainModel(string title) : base(title)
        {
            // 初始化界面
            Res.LoadLocale("Components\\Chinese (Simplified).frl");

            view.Icon = new Icon("logo.ico");
            view.WindowState = SystemInformation.WorkingArea.Height > 755 ? FormWindowState.Normal : FormWindowState.Maximized;
            view.MyFeel.LookAndFeel.SkinName = Setting.lookAndFeel;
            view.StbTime.Caption = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            view.StbServer.Caption = Setting.gateway;

            // 订阅主窗体菜单事件
            view.MubChangPassWord.ItemClick += (sender, args) => callback("changPassword", new object[]{null});
            view.MubLock.ItemClick += (sender, args) => callback("lockWindow");
            view.MubLogout.ItemClick += (sender, args) => logout();
            view.MubExit.ItemClick += (sender, args) => view.Close();
            view.MubPrintSet.ItemClick += (sender, args) => callback("printSet");
            view.MubUpdate.ItemClick += (sender, args) => callback("update", new object[]{false});
            view.MubAbout.ItemClick += (sender, args) => callback("about");
            view.Closing += (sender, args) => exit(args);
        }

        /// <summary>
        /// 显示主窗体
        /// </summary>
        /// <param name="navigators">模块信息集合</param>
        public void showMainWindow(List<ModuleDto> navigators)
        {
            this.navigators = navigators;
            initNavBar();

            view.StbDept.Caption = Setting.deptName;
            view.StbDept.Visibility = string.IsNullOrEmpty(Setting.deptName) ? BarItemVisibility.Never : BarItemVisibility.Always;
            view.StbUser.Caption = Setting.userName;

            view.Show();
            view.Refresh();

            opens.ForEach(i => callback("openMdiWindow", new object[] { i }));
            if (Setting.needChangePw) callback("changPassword", new object[] { "123456" });
        }

        /// <summary>
        /// 初始化导航栏
        /// </summary>
        private void initNavBar()
        {
            var links = new List<NavBarItemLink>();
            var groups = navigators.Where(i => i.parentId == null).ToList();
            var height = view.NavMain.Height;
            foreach (var g in groups)
            {
                var expand = false;
                var barItem = new List<NavBarItemLink>();
                foreach (var module in navigators.Where(i => i.parentId == g.id))
                {
                    if (module.moduleInfo.autoLoad ?? false)
                    {
                        expand = true;
                        opens.Add(module.moduleInfo.module);
                    }

                    var icon = Util.getImageFromFile(module.moduleInfo.iconUrl);
                    var navBarItem = new NavBarItem(module.name) { Tag = module.moduleInfo.module, SmallImage = icon };
                    barItem.Add(new NavBarItemLink(navBarItem));
                }

                var group = new NavBarGroup
                {
                    Caption = g.name,
                    Name = g.name,
                    SmallImage = Util.getImageFromFile(g.moduleInfo.iconUrl)
                };
                var count = links.Count + barItem.Count;
                group.Expanded = groups.Count * 55 + count * 32 < height || expand;
                group.ItemLinks.AddRange(barItem.ToArray());

                view.NavMain.Groups.Add(group);
                links.AddRange(barItem);
            }

            links.ForEach(i => i.Item.LinkClicked += (sender, args) =>
            {
                callback("openMdiWindow", new object[] { args.Link.Item.Tag.ToString() });
            });
        }
        
        /// <summary>
        /// 点击菜单项：注销，弹出询问对话框，确认注销后重启应用程序
        /// </summary>
        private static void logout()
        {
            const string msg = "注销用户将导致当前未完成的输入内容丢失！\r\n您确定要注销吗？";
            if (!Messages.showConfirm(msg)) return;

            Setting.tokenHelper.deleteToken();
            Application.Restart();
        }

        /// <summary>
        /// 退出系统前保存当前应用的皮肤
        /// </summary>
        /// <param name="args">取消事件参数</param>
        private void exit(CancelEventArgs args)
        {
            const string msg = "退出应用程序将导致当前未完成的输入内容丢失！\r\n您确定要退出吗？";
            if (!Messages.showConfirm(msg))
            {
                args.Cancel = true;
                return;
            }

            Setting.saveLookAndFeel(view.MyFeel.LookAndFeel.SkinName);
            Setting.tokenHelper.deleteToken();
            Application.Exit();
        }
    }
}
