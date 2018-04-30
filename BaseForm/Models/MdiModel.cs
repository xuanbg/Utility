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
using FastReport;
using Insight.Utils.BaseForm;
using Insight.Utils.Client;
using Insight.Utils.Common;
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
        /// 模块选项集合
        /// </summary>
        public List<ModuleParam> moduleParams;

        /// <summary>
        /// 构造函数，初始化MDI窗体并显示
        /// </summary>
        /// <param name="nav">导航信息</param>
        public MdiModel(Navigation nav)
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
            GetParams();
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <typeparam name="E">类型</typeparam>
        /// <param name="tid">模板ID</param>
        /// <param name="name">数据源名称</param>
        /// <param name="data">数据</param>
        public void Design<E>(string tid, string name, List<E> data)
        {
            if (string.IsNullOrEmpty(tid))
            {
                Messages.ShowError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return;
            }

            var report = BuildReport(tid, name, data);
            if (report == null)
            {
                Messages.ShowError("初始化报表失败！");
                return;
            }

            report.Design();
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="tid">模板ID</param>
        /// <param name="id">数据ID</param>
        public void Preview(string tid, string id = null)
        {
            if (string.IsNullOrEmpty(tid))
            {
                Messages.ShowError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return;
            }

            var print = BuildReport(tid, id);
            print?.ShowPrepared(true);
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <typeparam name="E">类型</typeparam>
        /// <param name="tid">模板ID</param>
        /// <param name="name">数据源名称</param>
        /// <param name="data">数据</param>
        public void Preview<E>(string tid, string name, List<E> data)
        {
            if (string.IsNullOrEmpty(tid))
            {
                Messages.ShowError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return;
            }

            var report = BuildReport(tid, name, data);
            if (report == null || !report.Prepare())
            {
                Messages.ShowError("生成报表失败！");
                return;
            }

            report.ShowPrepared(true);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="printer">打印机名称</param>
        /// <param name="tid">模板ID</param>
        /// <param name="id">数据ID</param>
        /// <param name="onSheet">合并打印模式</param>
        /// <returns>string 打印文档名称</returns>
        public string Print(string printer, string tid, string id = null, int onSheet = 0)
        {
            if (string.IsNullOrEmpty(tid))
            {
                Messages.ShowError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return null;
            }

            var print = BuildReport(tid, id);
            if (print == null) return null;

            var type = (PagesOnSheet) onSheet;
            if (type != PagesOnSheet.One)
            {
                print.PrintSettings.PrintMode = PrintMode.Scale;
                print.PrintSettings.PagesOnSheet = type;
            }

            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintSettings.ShowDialog = false;
                print.PrintSettings.Printer = printer;
            }

            print.PrintPrepared();
            return print.FileName;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="printer">打印机名称</param>
        /// <param name="tid">模板ID</param>
        /// <param name="name">数据源名称</param>
        /// <param name="data">数据</param>
        /// <param name="onSheet">合并打印模式</param>
        /// <returns>string 打印文档名称</returns>
        public string Print<E>(string printer, string tid, string name, List<E> data, int onSheet = 0)
        {
            if (string.IsNullOrEmpty(tid))
            {
                Messages.ShowError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return null;
            }

            var report = BuildReport(tid, name, data);
            if (report == null || !report.Prepare())
            {
                Messages.ShowError("生成报表失败！");
                return null;
            }

            report.PrintPrepared();
            return report.FileName;
        }

        /// <summary>
        /// 切换工具栏按钮状态
        /// </summary>
        /// <param name="dict"></param>
        public void SwitchItemStatus(Dictionary<string, bool> dict)
        {
            foreach (var obj in dict)
            {
                var item = buttons.SingleOrDefault(b => b.Name == obj.Key);
                if (item == null) continue;

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
        /// 获取选项数据
        /// </summary>
        /// <param name="key">选项代码</param>
        /// <param name="deptId">部门ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>ModuleParam 选项数据</returns>
        public ModuleParam GetParam(string key, string deptId = null, string userId = null)
        {
            var param = moduleParams.FirstOrDefault(i => i.code == key && i.deptId == deptId && i.userId == userId);
            if (param == null)
            {
                param = new ModuleParam
                {
                    id = Util.NewId(),
                    moduleId = moduleId,
                    code = key,
                    deptId = deptId,
                    userId = userId
                };
                moduleParams.Add(param);
            }

            return param;
        }

        /// <summary>
        /// 保存选项数据
        /// </summary>
        /// <returns>bool 是否成功</returns>
        public bool SaveParam()
        {
            var url = $"{baseServer}/commonapi/v1.0/navigations/{moduleId}/params";
            var dict = new Dictionary<string, object> {{"list", moduleParams}};
            var client = new HttpClient<List<ModuleParam>>(tokenHelper);

            return client.Put(url, dict);
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
        /// 生成报表
        /// </summary>
        /// <typeparam name="E">类型</typeparam>
        /// <param name="tid">模板ID</param>
        /// <param name="name">数据源名称</param>
        /// <param name="data">数据</param>
        /// <returns>Report FastReport报表</returns>
        private Report BuildReport<E>(string tid, string name, List<E> data)
        {
            var url = $"{baseServer}/commonapi/v1.0/templates/{tid}";
            var client = new HttpClient<object>(tokenHelper);
            if (!client.Get(url)) return null;

            var report = new Report();
            report.LoadFromString(client.data.ToString());
            report.RegisterData(data, name);

            return report;
        }

        /// <summary>
        /// 生成报表
        /// </summary>
        /// <param name="tid">模板ID</param>
        /// <param name="id">数据ID</param>
        /// <returns></returns>
        private Report BuildReport(string tid, string id)
        {
            var isCopy = false;
            ImageData img;
            if (string.IsNullOrEmpty(tid))
            {
                if (string.IsNullOrEmpty(id))
                {
                    Messages.ShowError("尚未选定需要打印的数据！请先选择数据。");
                    return null;
                }

                isCopy = true;
                img = GetImageData(id);
                if (img == null)
                {
                    Messages.ShowError("尚未设置打印模板！请先在设置对话框中设置正确的模板。");
                    return null;
                }
            }
            else
            {
                img = BuildImageData(id, tid);
            }

            if (img == null)
            {
                Messages.ShowError("生成打印数据错误");
                return null;
            }

            var print = new Report {FileName = img.id};
            print.LoadPrepared(new MemoryStream(img.image));

            if (isCopy) AddWatermark(print, "副 本");

            return print;
        }

        /// <summary>
        /// 获取电子影像数据
        /// </summary>
        /// <param name="id">影像ID</param>
        /// <returns>ImageData 电子影像数据</returns>
        private ImageData GetImageData(string id)
        {
            var url = $"{Setting.baseServer}/commonapi/v1.0/images/{id}";
            var client = new HttpClient<ImageData>(Setting.tokenHelper);

            return client.Get(url) ? client.data : null;
        }

        /// <summary>
        /// 生成电子影像数据
        /// </summary>
        /// <param name="id">业务数据ID</param>
        /// <param name="templateId">模板ID</param>
        /// <returns></returns>
        private ImageData BuildImageData(string id, string templateId)
        {
            var url = $"{Setting.baseServer}/commonapi/v1.0/images/{id ?? "null"}";
            var client = new HttpClient<ImageData>(Setting.tokenHelper);
            var dict = new Dictionary<string, object>
            {
                {"templateId", templateId},
                {"deptName", Setting.deptName}
            };

            return client.Post(url, dict) ? client.data : null;
        }

        /// <summary>
        /// 增加水印
        /// </summary>
        /// <param name="fr">Report对象实体</param>
        /// <param name="str">水印文字</param>
        /// <param name="size"></param>
        /// <returns>Report对象实体</returns>
        private void AddWatermark(Report fr, string str, int size = 72)
        {
            var wm = new Watermark
            {
                Enabled = true,
                Text = str,
                Font = new Font("宋体", size, FontStyle.Bold)
            };

            for (var i = 0; i < fr.PreparedPages.Count; i++)
            {
                var pag = fr.PreparedPages.GetPage(i);
                pag.Watermark = wm;
                fr.PreparedPages.ModifyPage(i, pag);
            }
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

        /// <summary>
        /// 获取选项数据
        /// </summary>
        private void GetParams()
        {
            var url = $"{baseServer}/commonapi/v1.0/navigations/{moduleId}/params";
            var client = new HttpClient<List<ModuleParam>>(tokenHelper);
            if (!client.Get(url)) return;

            moduleParams = client.data;
        }
    }
}