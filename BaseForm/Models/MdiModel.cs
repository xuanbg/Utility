using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using FastReport;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Controls;
using Insight.Utils.Entity;

namespace Insight.Utils.Models
{
    public class MdiModel
    {
        private GridHitInfo hitInfo = new GridHitInfo();

        /// <summary>
        /// 模块选项集合
        /// </summary>
        public List<ModuleParam> moduleParams;

        /// <summary>
        /// 令牌管理器
        /// </summary>
        protected readonly TokenHelper tokenHelper = Setting.tokenHelper;

        /// <summary>
        /// 服务地址
        /// </summary>
        protected readonly string gateway = Setting.gateway;

        /// <summary>
        /// 主列表分页控件
        /// </summary>
        protected PageControl tab;

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
        public List<ModuleParam> getParams(List<Dictionary<string, string>> keys)
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
            var url = $"{gateway}/report/v1.0/templates/{tid}";
            var client = new HttpClient<object>(tokenHelper);
            if (!client.get(url)) return null;

            var report = new Report();
            report.LoadFromString(client.data.ToString());
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
                var url = $"{gateway}/report/v1.0/images/{id}";
                var client = new HttpClient<ImageData>(tokenHelper);
                if (!client.get(url)) return null;

                img = client.data;
                if (img == null)
                {
                    Messages.showError("尚未设置打印模板！请先在设置对话框中设置正确的模板。");
                    return null;
                }
            }
            else
            {
                // 使用模板生成电子影像
                var url = $"{gateway}/report/v1.0/images/{id ?? "null"}";
                var client = new HttpClient<ImageData>(tokenHelper);
                var dict = new Dictionary<string, object>
                {
                    {"templateId", tid},
                    {"deptName", Setting.deptName}
                };
                if (!client.post(url, dict)) return null;

                isCopy = false;
                img = client.data;
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

        /// <summary>
        /// 获取模块功能按钮集合
        /// </summary>
        /// <returns>功能按钮集合</returns>
        public IEnumerable<Function> getActions(string moduleId)
        {
            var url = $"{gateway}/base/auth/v1.0/navigators/{moduleId}/functions";
            var client = new HttpClient<List<Function>>(tokenHelper);

            return client.getData(url);
        }

        /// <summary>
        /// 获取选项数据
        /// </summary>
        /// <returns>选项数据集合</returns>
        public void getParams()
        {
            var url = $"{gateway}/common/v1.0/params";
            var client = new HttpClient<List<ModuleParam>>(tokenHelper);

            moduleParams = client.getData(url);
        }

        /// <summary>
        /// 保存选项数据
        /// </summary>
        /// <returns>bool 是否成功</returns>
        public void saveParam()
        {
            var url = $"{gateway}/common/v1.0/params";
            var dict = new Dictionary<string, object> {{"list", moduleParams}};
            var client = new HttpClient<List<ModuleParam>>(tokenHelper);
            client.put(url, dict);
        }
    }
}