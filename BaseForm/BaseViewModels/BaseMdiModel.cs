using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using FastReport;
using Insight.Utils.BaseForms;
using Insight.Utils.Common;
using Insight.Utils.Controls;
using Insight.Utils.Entity;

namespace Insight.Utils.BaseViewModels
{
    public class BaseMdiModel<T, TV> where TV : BaseMdi, new()
    {
        private GridHitInfo hitInfo = new GridHitInfo();

        /// <summary>
        /// 模块选项集合
        /// </summary>
        public List<ModuleParam> moduleParams;

        /// <summary>
        /// MDI视图
        /// </summary>
        public TV view;

        /// <summary>
        /// 主列表分页控件
        /// </summary>
        public PageControl tab;

        /// <summary>
        /// 当前列表handle
        /// </summary>
        public int handle = 0;

        /// <summary>
        /// 列表数据
        /// </summary>
        public List<T> list;

        /// <summary>
        /// 列表当前选中数据对象
        /// </summary>
        public T item;

        /// <summary>
        /// 初始化视图
        /// </summary>
        /// <param name="module">模块信息</param>
        public void initModule(ModuleDto module)
        {
            if (module.moduleInfo.hasParams ?? false) moduleParams = BaseModel.getParams();

            var icon = Util.getImage(module.moduleInfo.iconUrl);
            view = new TV
            {
                ControlBox = module.index > 0,
                MdiParent = Application.OpenForms["MainWindow"],
                Icon = Icon.FromHandle(new Bitmap(icon).GetHicon()),
                Name = module.moduleInfo.module,
                Text = module.name
            };
            view.Show();
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
        public List<ModuleParam> getParams(IEnumerable<Dictionary<string, string>> keys)
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
        public ModuleParam getParam(string key, string deptId = null, string userId = null, string moduleId = null)
        {
            var param = moduleParams.FirstOrDefault(i => i.deptId == deptId && i.userId == userId && i.code == key && (string.IsNullOrEmpty(moduleId) || i.moduleId == moduleId));
            if (param != null) return param;

            param = new ModuleParam
            {
                id = Util.newId(),
                moduleId = moduleId,
                code = key,
                deptId = deptId,
                userId = userId
            };
            moduleParams.Add(param);

            return param;
        }

        /// <summary>
        /// 生成报表
        /// </summary>
        /// <typeparam name="TE">类型</typeparam>
        /// <param name="tid">模板ID</param>
        /// <param name="name">数据源名称</param>
        /// <param name="data">数据</param>
        /// <param name="dict">参数集合</param>
        /// <returns>Report FastReport报表</returns>
        public Report buildReport<TE>(string tid, string name, List<TE> data, Dictionary<string, object> dict)
        {
            var template = BaseModel.getTemplate(tid);
            if (template == null) return null;

            var report = new Report();
            report.LoadFromString(template);
            report.RegisterData(data, name);
            foreach (var i in dict ?? new Dictionary<string, object>())
            {
                report.SetParameterValue(i.Key, i.Value);
            }

            return report;
        }

        /// <summary>
        /// 生成报表
        /// </summary>
        /// <param name="tid">模板ID</param>
        /// <param name="id">数据ID</param>
        /// <param name="isCopy">是否副本</param>
        /// <returns></returns>
        public Report buildReport(string tid, string id, bool isCopy = true)
        {
            ImageData img;
            if (string.IsNullOrEmpty(tid))
            {
                if (string.IsNullOrEmpty(id))
                {
                    Messages.showError("尚未选定需要打印的数据！请先选择数据。");
                    return null;
                }

                // 获取电子影像

                img = BaseModel.getImage(id);
                if (img == null)
                {
                    Messages.showError("尚未设置打印模板！请先在设置对话框中设置正确的模板。");
                    return null;
                }
            }
            else
            {
                // 使用模板生成电子影像
                isCopy = false;
                img = BaseModel.newImage(id, tid);
                if (img == null)
                {
                    Messages.showError("生成打印数据错误");
                    return null;
                }
            }

            // 加载电子影像
            var print = new Report {FileName = img.id};
            print.LoadPrepared(new MemoryStream(img.image));
            if (!isCopy) return print;

            // 生成水印
            var wm = new Watermark
            {
                Enabled = true,
                Text = "副本",
                Font = new Font("宋体", 72, FontStyle.Bold)
            };

            for (var i = 0; i < print.PreparedPages.Count; i++)
            {
                var pag = print.PreparedPages.GetPage(i);
                pag.Watermark = wm;
                print.PreparedPages.ModifyPage(i, pag);
            }

            return print;
        }
    }
}