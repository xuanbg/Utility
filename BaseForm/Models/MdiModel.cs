using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Insight.Utils.BaseForm;
using Insight.Utils.Client;
using Insight.Utils.Entity;

namespace Insight.Utils.Models
{
    public class MdiModel<T> where T : BaseMDI, new()
    {
        private const int MinWaitTime = 800;
        private int waits;
        private DateTime wait;
        private GridHitInfo hitInfo = new GridHitInfo();

        /// <summary>
        /// MDI视图
        /// </summary>
        public T view;

        /// <summary>
        /// 工具栏按钮集合
        /// </summary>
        public List<BarButtonItem> buttons;

        /// <summary>
        /// 令牌管理器
        /// </summary>
        public TokenHelper tokenHelper = Setting.tokenHelper;

        /// <summary>
        /// 应用服务地址
        /// </summary>
        public string appServer = Setting.appServer;

        /// <summary>
        /// 基础服务地址
        /// </summary>
        public string baseServer = Setting.baseServer;

        /// <summary>
        /// 业务模块ID
        /// </summary>
        public readonly string moduleId;

        /// <summary>
        /// 构造函数，初始化MDI窗体并显示
        /// </summary>
        /// <param name="nav">导航信息</param>
        protected MdiModel(Navigation nav)
        {
            moduleId = nav.id;
            view = new T
            {
                ControlBox = nav.index > 0,
                MdiParent = Application.OpenForms["MainWindow"],
                Icon = Icon.FromHandle(new Bitmap(new MemoryStream(nav.icon)).GetHicon()),
                Name = nav.alias,
                Text = nav.name
            };

            view.Show();
            InitToolBar();
        }

        /// <summary>
        /// 切换工具栏按钮状态
        /// </summary>
        /// <param name="dict"></param>
        public void SwitchItemStatus(Dictionary<string, bool> dict)
        {
            foreach (var obj in dict)
            {
                var item = buttons.Single(b => b.Name == obj.Key);
                item.Enabled = obj.Value && (bool)item.Tag;
            }
        }

        /// <summary>
        /// 是否允许双击
        /// </summary>
        /// <param name="key">操作名称</param>
        /// <returns>是否允许双击</returns>
        public bool AllowDoubleClick(string key)
        {
            var button = buttons.SingleOrDefault(i => i.Name == key);
            return button != null && button.Enabled;
        }

        /// <summary>
        /// 显示等待提示
        /// </summary>
        public void ShowWaitForm()
        {
            waits++;
            if (view.Wait.IsSplashFormVisible) return;

            wait = DateTime.Now;
            view.Wait.ShowWaitForm();
        }

        /// <summary>
        /// 关闭等待提示
        /// </summary>
        public void CloseWaitForm()
        {
            waits--;
            if (waits > 0) return;

            var time = (int) (DateTime.Now - wait).TotalMilliseconds;
            if (time < MinWaitTime) Thread.Sleep(MinWaitTime - time);

            view.Wait.CloseWaitForm();
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="args">MouseEventArgs</param>
        public void MouseDownEvent(GridView gridView, MouseEventArgs args)
        {
            if (args.Button != MouseButtons.Right) return;

            var point = new Point(args.X, args.Y);
            hitInfo = gridView.CalcHitInfo(point);
        }

        /// <summary>
        /// 创建右键菜单并注册事件
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <returns>ContextMenuStrip</returns>
        public ContextMenuStrip CreateCopyMenu(GridView gridView)
        {
            var tsmi = new ToolStripMenuItem { Text = "复制" };
            tsmi.Click += (sender, args) =>
            {
                if (hitInfo.Column == null) return;

                var content = gridView.GetRowCellDisplayText(hitInfo.RowHandle, hitInfo.Column);
                if (string.IsNullOrEmpty(content)) return;

                Clipboard.Clear();
                Clipboard.SetData(DataFormats.Text, content);
            };

            var menu = new ContextMenuStrip();
            menu.Items.Add(tsmi);
            return menu;
        }

        /// <summary>
        /// 初始化模块工具栏
        /// </summary>
        private void InitToolBar()
        {
            buttons = (from a in GetActions()
                       select new BarButtonItem
                       {
                           AllowDrawArrow = a.isBegin,
                           Caption = a.name,
                           Enabled = a.permit,
                           Name = a.alias.Split(',')[0],
                           Tag = a.permit,
                           Glyph = Image.FromStream(new MemoryStream(a.icon)),
                           PaintStyle = a.isShowText ? BarItemPaintStyle.CaptionGlyph : BarItemPaintStyle.Standard,
                       }).ToList();
            buttons.ForEach(i => view.ToolBar.ItemLinks.Add(i, i.AllowDrawArrow));
        }

        /// <summary>
        /// 获取模块功能按钮集合
        /// </summary>
        /// <returns>功能按钮集合</returns>
        private IEnumerable<Function> GetActions()
        {
            var url = $"{baseServer}/commonapi/v1.0/navigations/{moduleId}/functions";
            var client = new HttpClient<List<Function>>(tokenHelper);
            return client.Get(url) ? client.data : new List<Function>();
        }
    }
}