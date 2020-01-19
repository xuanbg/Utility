using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public class MainModel : BaseModel<MainWindow>
    {
        private readonly List<string> opens = new List<string>();
        private List<ModuleDto> navItems;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">窗体标题</param>
        public MainModel(string title) : base(title)
        {
            // 初始化界面
            Res.LoadLocale("Components\\Chinese (Simplified).frl");
            view.Icon = new Icon("logo.ico");
            view.MyFeel.LookAndFeel.SkinName = Setting.lookAndFeel;
            view.StbTime.Caption = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            view.StbServer.Caption = Setting.gateway;
            view.WindowState = SystemInformation.WorkingArea.Height > 755 ? FormWindowState.Normal : FormWindowState.Maximized;

            // 订阅主窗体菜单事件
            view.MubChangPassWord.ItemClick += (sender, args) => callback("changPassword");
            view.MubLock.ItemClick += (sender, args) => callback("lockWindow");
            view.MubLogout.ItemClick += (sender, args) => callback("logout");
            view.MubExit.ItemClick += (sender, args) => callback("exit");
            view.MubPrintSet.ItemClick += (sender, args) => callback("printSet");
            view.MubUpdate.ItemClick += (sender, args) => callback("update");
            view.MubAbout.ItemClick += (sender, args) => callback("about");
            view.Closed += (sender, args) => callback("exit");
        }

        /// <summary>
        /// 主窗体初始化
        /// </summary>
        public void showMainWindow(List<ModuleDto> navigators)
        {
            view.StbDept.Caption = Setting.deptName;
            view.StbDept.Visibility = string.IsNullOrEmpty(Setting.deptName) ? BarItemVisibility.Never : BarItemVisibility.Always;
            view.StbUser.Caption = Setting.userName;

            initNavBar(navigators);
            view.Show();
        }

        /// <summary>
        /// 自动加载模块
        /// </summary>
        public void autoLoad()
        {
            opens.ForEach(addPageMdi);
        }

        /// <summary>
        /// 保存当前主题样式到配置文件
        /// </summary>
        public void saveLookAndFeel()
        {
            Setting.saveLookAndFeel(view.MyFeel.LookAndFeel.SkinName);
        }

        /// <summary>
        /// 初始化导航栏
        /// </summary>
        private void initNavBar(List<ModuleDto> navigators)
        {
            var links = new List<NavBarItemLink>();
            navItems = navigators.Where(i => i.parentId != null).ToList();
            var groups = navigators.Where(i => i.parentId == null).ToList();
            var height = view.NavMain.Height;
            foreach (var g in groups)
            {
                var expand = false;
                var items = new List<NavBarItemLink>();
                foreach (var item in navItems.Where(i => i.parentId == g.id))
                {
                    if (item.moduleInfo.autoLoad ?? false)
                    {
                        expand = true;
                        opens.Add(item.moduleInfo.module);
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

                view.NavMain.Groups.Add(group);
                links.AddRange(items);
            }

            links.ForEach(i => i.Item.LinkClicked += (sender, args) => addPageMdi(args.Link.Item.Tag.ToString()));
        }

        /// <summary>
        /// 打开MDI子窗体
        /// </summary>
        /// <param name="name"></param>
        private void addPageMdi(string name)
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

            var asm = Assembly.LoadFrom(path);
            var type = asm.GetTypes().SingleOrDefault(i => i.FullName != null && i.FullName.EndsWith($"{mod.moduleInfo.module}.Controller"));
            if (type == null || string.IsNullOrEmpty(type.FullName))
            {
                var msg = $"对不起，{mod.name}模块无法加载！\r\n您的应用程序中缺少相应组件。";
                Messages.showError(msg);

                return;
            }

            view.Loading.ShowWaitForm();
            asm.CreateInstance(type.FullName, false, BindingFlags.Default, null, new object[] { mod }, CultureInfo.CurrentCulture, null);
            view.Loading.CloseWaitForm();
        }
    }
}
