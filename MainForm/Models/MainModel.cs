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
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;
using Insight.Utils.MainForm.Views;
using Insight.Utils.Models;

namespace Insight.Utils.MainForm.Models
{
    public class MainModel : BaseModel
    {
        public MainWindow view;
        public List<NavBarItemLink> links = new List<NavBarItemLink>();
        public List<string> needOpens = new List<string>();

        private List<Navigation> navItems;

        /// <summary>
        /// 构造函数，初始化视图
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        public MainModel()
        {
            view = new MainWindow {Text = Setting.appName};

            // 加载模块数据并初始化导航栏
            InitNavBar();

            // 初始化界面
            Res.LoadLocale("Components\\Chinese (Simplified).frl");
            view.MyFeel.LookAndFeel.SkinName = Setting.lookAndFeel;

            view.StbTime.Caption = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            view.StbDept.Caption = Setting.deptName;
            view.StbDept.Visibility = string.IsNullOrEmpty(Setting.deptName) ? BarItemVisibility.Never : BarItemVisibility.Always;
            view.StbUser.Caption = Setting.userName;
            view.StbServer.Caption = Setting.appServer ?? Setting.baseServer;
            if (SystemInformation.WorkingArea.Height > 755) return;

            view.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// 加载默认启动模块
        /// </summary>
        public void LoadDefault()
        {
            needOpens.ForEach(AddPageMdi);
        }

        /// <summary>
        /// 打开MDI子窗体
        /// </summary>
        /// <param name="name"></param>
        public void AddPageMdi(string name)
        {
            var form = Application.OpenForms[name];
            if (form != null)
            {
                form.Activate();
                return;
            }

            var mod = navItems.Single(m => m.alias == name);
            var path = $"{Application.StartupPath}\\{mod.url}";
            if (!File.Exists(path))
            {
                var msg = $"对不起，{mod.name}模块无法加载！\r\n未能发现{path}文件。";
                Messages.ShowError(msg);
                return;
            }

            view.Loading.ShowWaitForm();
            var asm = Assembly.LoadFrom(path);
            var type = asm.GetTypes().SingleOrDefault(i => i.FullName != null && i.FullName.EndsWith($"{mod.alias}.Controller"));
            if (type == null)
            {
                view.Loading.CloseWaitForm();
                var msg = $"对不起，{mod.name}模块无法加载！\r\n您的应用程序中缺少相应组件。";
                Messages.ShowError(msg);

                return;
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            asm.CreateInstance(type.FullName, false, BindingFlags.Default, null, new object[] { mod }, CultureInfo.CurrentCulture, null);
            view.Loading.CloseWaitForm();
        }

        /// <summary>
        /// 如注销用户失败，弹出询问对话框。
        /// </summary>
        public bool Logout()
        {
            const string msg = "退出应用程序将导致当前未完成的输入内容丢失！\r\n您确定要退出吗？";
            if (!Messages.ShowConfirm(msg)) return true;

            tokenHelper.DeleteToken();

            return false;
        }

        /// <summary>
        /// 保存当前主题样式到配置文件
        /// </summary>
        public void SaveLookAndFeel()
        {
            Setting.SaveLookAndFeel(view.MyFeel.LookAndFeel.SkinName);
        }

        /// <summary>
        /// 初始化导航栏
        /// </summary>
        private void InitNavBar()
        {
            var url = $"{baseServer}/moduleapi/v1.0/navigations";
            var client = new HttpClient<List<Navigation>>(tokenHelper);
            if (!client.Get(url)) return;

            navItems = client.data.Where(i => i.parentId != null).ToList();
            var groups = client.data.Where(i => i.parentId == null).ToList();
            var height = view.NavMain.Height;
            foreach (var g in groups)
            {
                var expand = false;
                var items = new List<NavBarItemLink>();
                foreach (var item in navItems.Where(i => i.parentId == g.id))
                {
                    if (item.isDefault)
                    {
                        expand = true;
                        needOpens.Add(item.alias);
                    }

                    var icon = Image.FromStream(new MemoryStream(item.icon));
                    var navBarItem = new NavBarItem(item.name) { Tag = item.alias, SmallImage = icon };
                    items.Add(new NavBarItemLink(navBarItem));
                }

                var group = new NavBarGroup
                {
                    Caption = g.name,
                    Name = g.name,
                    SmallImage = Image.FromStream(new MemoryStream(g.icon))
                };
                var count = links.Count + items.Count;
                group.Expanded = groups.Count * 55 + count * 32 < height || expand;
                group.ItemLinks.AddRange(items.ToArray());

                view.NavMain.Groups.Add(group);
                links.AddRange(items);
            }
        }
    }
}
