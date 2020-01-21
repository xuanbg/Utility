using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FastReport;
using Insight.Utils.BaseForms;
using Insight.Utils.BaseViewModels;
using Insight.Utils.Common;
using Insight.Utils.Entities;
using Insight.Utils.Entity;

namespace Insight.Utils.BaseControllers
{
    public class MdiController<T, TV, TM> : BaseController where TV : BaseMdi, new() where TM : BaseMdiModel<T, TV>, new()
    {
        private readonly DataModel dataModel = new DataModel();

        /// <summary>
        /// MDI Model
        /// </summary>
        protected readonly TM mdiModel;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="module">模块信息</param>
        protected MdiController(ModuleDto module)
        {
            mdiModel = new TM();
            mdiModel.initMdiView(module);
            mdiModel.callbackEvent += (sender, args) => buttonClick(args.methodName, args.param);
            mdiModel.show();

            mdiModel.initToolBar(dataModel.getActions(module.id));
            if (module.moduleInfo.hasParams ?? false) mdiModel.initParams(dataModel.getParams());
        }

        /// <summary>
        /// 设计报表
        /// </summary>
        /// <typeparam name="TE">类型</typeparam>
        /// <param name="tid">模板ID</param>
        /// <param name="name">数据源名称</param>
        /// <param name="data">数据</param>
        /// <param name="param">参数集合</param>
        public void design<TE>(string tid, string name, List<TE> data, Dictionary<string, object> param = null)
        {
            if (string.IsNullOrEmpty(tid))
            {
                Messages.showError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return;
            }

            var report = buildReport(tid, name, data, param);
            if (report == null)
            {
                Messages.showError("初始化报表失败！");
                return;
            }

            report.Design();
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="tid">模板ID</param>
        /// <param name="id">数据ID</param>
        public void preview(string tid, string id = null)
        {
            if (string.IsNullOrEmpty(tid))
            {
                Messages.showError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return;
            }

            var print = buildReport(tid, id);
            print?.ShowPrepared(true);
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="set">打印设置</param>
        public void preview<TE>(PrintSetting<TE> set)
        {
            if (string.IsNullOrEmpty(set.templateId))
            {
                Messages.showError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return;
            }

            var report = buildReport(set.templateId, set.dataName, set.data, set.parameter);
            if (report == null || !report.Prepare())
            {
                Messages.showError("生成报表失败！");
                return;
            }

            if (set.pagesOnSheet != PagesOnSheet.One)
            {
                report.PrintSettings.PrintMode = PrintMode.Scale;
                report.PrintSettings.PagesOnSheet = set.pagesOnSheet;
            }
            else
            {
                report.PrintSettings.PrintMode = set.printMode;
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
        public string print(string printer, string tid, string id = null, int onSheet = 0)
        {
            if (string.IsNullOrEmpty(tid))
            {
                Messages.showError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return null;
            }

            var report = buildReport(tid, id);
            if (report == null) return null;

            if (onSheet > 0)
            {
                report.PrintSettings.PrintMode = PrintMode.Scale;
                report.PrintSettings.PagesOnSheet = PagesOnSheet.Three;
            }

            if (!string.IsNullOrEmpty(printer))
            {
                report.PrintSettings.ShowDialog = false;
                report.PrintSettings.Printer = printer;
            }

            report.PrintPrepared();

            return report.FileName;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="set">打印设置</param>
        /// <returns>string 电子影像文件名</returns>
        public string print<TE>(PrintSetting<TE> set)
        {
            if (string.IsNullOrEmpty(set.templateId))
            {
                Messages.showError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return null;
            }

            var report = buildReport(set.templateId, set.dataName, set.data, set.parameter);
            if (report == null || !report.Prepare())
            {
                Messages.showError("生成报表失败！");
                return null;
            }

            report.PrintSettings.Copies = set.copies;
            if (!string.IsNullOrEmpty(set.printer))
            {
                report.PrintSettings.ShowDialog = false;
                report.PrintSettings.Printer = set.printer;
            }

            if (set.pagesOnSheet != PagesOnSheet.One)
            {
                report.PrintSettings.PrintMode = PrintMode.Scale;
                report.PrintSettings.PagesOnSheet = set.pagesOnSheet;
            }
            else
            {
                report.PrintSettings.PrintMode = set.printMode;
            }

            report.PrintPrepared();

            return report.FileName;
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
        private Report buildReport<TE>(string tid, string name, List<TE> data, Dictionary<string, object> dict)
        {
            var template = dataModel.getTemplate(tid);
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
        private Report buildReport(string tid, string id, bool isCopy = true)
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

                img = dataModel.getImage(id);
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
                img = dataModel.newImage(id, tid);
                if (img == null)
                {
                    Messages.showError("生成打印数据错误");
                    return null;
                }
            }

            // 加载电子影像
            var print = new Report { FileName = img.id };
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