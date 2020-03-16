﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using Insight.Utils.BaseForms;
using Insight.Utils.Common;
using Insight.Utils.Controls;
using Insight.Utils.Entity;

namespace Insight.Utils.BaseViewModels
{
    public class BaseMdiModel<T, TV, DM> : BaseModel<T, TV> where TV : BaseMdi, new()
    {
        private GridHitInfo hitInfo = new GridHitInfo();
        private List<BarButtonItem> buttons;
        private int waits;
        private DateTime wait;

        /// <summary>
        /// MDI Model
        /// </summary>
        public DM dataModel;

        /// <summary>
        /// 模块选项集合
        /// </summary>
        public List<ModuleParam> moduleParams;

        /// <summary>
        /// 主列表分页控件
        /// </summary>
        public PageControl tab;

        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string keyWord;

        /// <summary>
        /// 列表数据
        /// </summary>
        public List<T> list;

        /// <summary>
        /// 构造方法
        /// </summary>
        protected BaseMdiModel() : base(null)
        {
            view.Shown += (sender, args) =>
            {
                view.Refresh();
                call("loadData", new object[] {0});
            };
        }

        /// <summary>
        /// 初始化控件，子类必须有“loadData”和“itemChanged”方法
        /// </summary>
        /// <param name="grid">主列表控件</param>
        /// <param name="method">主列表控件双击事件回调方法名称</param>
        /// <param name="tab">主列表分页控件</param>
        /// <param name="input">搜索框控件</param>
        /// <param name="search">搜索按钮控件</param>
        protected void init(GridView grid, string method, PageControl tab = null, ButtonEdit input = null, SimpleButton search = null)
        {
            this.tab = tab;
            initGrid(grid, "itemChanged", method, this.tab);
            if (input == null || search == null) return;

            search.Click += (sender, args) => call("loadData", new object[] {0});
            input.Properties.Click += (sender, args) => input.EditValue = null;
            input.EditValueChanged += (sender, args) => keyWord = input.EditValue as string;
            input.KeyPress += (sender, args) =>
            {
                if (args.KeyChar != 13) return;

                call("loadData", new object[] {0});
            };
        }

        /// <summary>
        /// 初始化列表控件
        /// </summary>
        /// <param name="grid">列表控件</param>
        /// <param name="callMethod">列表数据改变事件调用方法名称</param>
        /// <param name="callbackMethod">列表控件双击事件回调方法名称</param>
        /// <param name="tab">列表分页控件</param>
        protected void initGrid(GridView grid, string callMethod = null, string callbackMethod = null, PageControl tab = null)
        {
            grid.FocusedRowObjectChanged += (sender, args) =>
            {
                if (tab != null) tab.focusedRowHandle = args.FocusedRowHandle;

                call(callMethod, new object[] {args.FocusedRowHandle});
            };
            grid.FocusedRowChanged += (sender, args) =>
            {
                if (tab != null) tab.focusedRowHandle = args.FocusedRowHandle;

                call(callMethod, new object[] {args.FocusedRowHandle});
            };
            grid.DoubleClick += (sender, args) =>
            {
                if (callbackMethod == null) return;

                var button = buttons.SingleOrDefault(i => i.Name == callbackMethod);
                if (button == null || !button.Enabled) return;

                callback(callbackMethod);
            };

            Format.gridFormat(grid);
            if (tab == null) return;

            tab.currentPageChanged += (sender, args) => call("loadData", new object[] {args.handle});
            tab.focusedRowChanged += (sender, args) => grid.FocusedRowHandle = args.rowHandle;
        }

        /// <summary>
        /// 初始化树控件
        /// </summary>
        /// <param name="tree">树控件</param>
        /// <param name="callMethod">树数据改变事件调用方法名称</param>
        /// <param name="callbackMethod">树控件双击事件回调方法名称</param>
        protected void initTree(TreeList tree, string callMethod = null, string callbackMethod = null)
        {
            tree.DoubleClick += (sender, args) => callback(callbackMethod);
            tree.FocusedNodeChanged += (sender, args) => call(callMethod, new object[] {args.Node});

            Format.treeFormat(tree);
        }

        /// <summary>
        /// 初始化MDI窗体
        /// </summary>
        /// <param name="module">模块信息</param>
        public void initMdiView(ModuleDto module)
        {
            var icon = Util.getImage(module.moduleInfo.iconUrl);
            view.ControlBox = module.index > 0;
            view.MdiParent = Application.OpenForms["MainWindow"];
            view.Icon = Icon.FromHandle(new Bitmap(icon).GetHicon());
            view.Name = module.moduleInfo.module;
            view.Text = module.name;
        }

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="functions"></param>
        public void initToolBar(IEnumerable<FunctionDto> functions)
        {
            buttons = (from a in functions
                select new BarButtonItem
                {
                    AllowDrawArrow = a.funcInfo.beginGroup,
                    Caption = a.name,
                    Enabled = a.permit ?? false,
                    Name = a.funcInfo.method,
                    Tag = a.permit ?? false,
                    Glyph = Util.getImage(a.funcInfo.iconUrl),
                    PaintStyle = a.funcInfo.hideText ? BarItemPaintStyle.Standard : BarItemPaintStyle.CaptionGlyph
                }).ToList();
            buttons.ForEach(i => i.ItemClick += (sender, args) => callback(args.Item.Name));
            buttons.ForEach(i => view.ToolBar.ItemLinks.Add(i, i.AllowDrawArrow));
        }

        /// <summary>
        /// 初始化模块选项集合
        /// </summary>
        /// <param name="moduleParams">模块选项集合</param>
        public void initParams(List<ModuleParam> moduleParams)
        {
            this.moduleParams = moduleParams;
        }

        /// <summary>
        /// 切换工具栏按钮状态
        /// </summary>
        /// <param name="dict"></param>
        protected void switchItemStatus(Dictionary<string, bool> dict)
        {
            var keys = new[] { "enable", "disable" };
            foreach (var obj in dict)
            {
                var button = buttons.SingleOrDefault(b => b.Name == obj.Key);
                if (button == null) continue;

                button.Enabled = obj.Value && (bool)button.Tag;
                if (keys.All(i => !button.Name.Contains(i))) continue;

                button.Visibility = button.Enabled ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
        }
        
        /// <summary>
        /// 显示等待提示
        /// </summary>
        protected void showWaitForm()
        {
            waits++;
            if (view.Wait.IsSplashFormVisible) return;

            wait = DateTime.Now;
            view.Wait.ShowWaitForm();
        }

        /// <summary>
        /// 关闭等待提示
        /// </summary>
        protected void closeWaitForm()
        {
            waits--;
            if (waits > 0) return;

            var time = (int)(DateTime.Now - wait).TotalMilliseconds;
            if (time < 800) Thread.Sleep(800 - time);

            view.Wait.CloseWaitForm();
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="args">MouseEventArgs</param>
        protected void mouseDownEvent(GridView gridView, MouseEventArgs args)
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
        protected ContextMenuStrip createCopyMenu(GridView gridView)
        {
            var tsmi = new ToolStripMenuItem {Text = @"复制"};
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
        /// 获取选项数据
        /// </summary>
        /// <param name="keys">选项代码集</param>
        /// <returns>ModuleParam 选项数据</returns>
        protected List<ModuleParam> getParams(IEnumerable<Dictionary<string, string>> keys)
        {
            var datas = new List<ModuleParam>();
            foreach (var key in keys)
            {
                var code = key.ContainsKey("code") ? key["code"] : null;
                var deptId = key.ContainsKey("deptId") ? key["deptId"] : null;
                var userId = key.ContainsKey("userId") ? key["userId"] : null;
                var moduleId = key.ContainsKey("moduleId") ? key["moduleId"] : null;
                var data = getParam(code, deptId, userId, moduleId);
                datas.Add(data);
            }

            return datas;
        }

        /// <summary>
        /// 获取选项数据
        /// </summary>
        /// <param name="key">选项代码</param>
        /// <param name="deptId">部门ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <returns>ModuleParam 选项数据</returns>
        protected ModuleParam getParam(string key, string deptId = null, string userId = null, string moduleId = null)
        {
            var param = moduleParams.FirstOrDefault(i => i.deptId == deptId && i.creatorId == userId && i.code == key && (string.IsNullOrEmpty(moduleId) || i.moduleId == moduleId));
            if (param != null) return param;

            param = new ModuleParam
            {
                id = Util.newId(),
                moduleId = moduleId,
                code = key,
                deptId = deptId,
                creatorId = userId
            };
            moduleParams.Add(param);

            return param;
        }
    }
}