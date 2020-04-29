using System.Collections.Generic;
using FastReport;
using Insight.Base.BaseForm.Entities;
using Insight.Base.BaseForm.Forms;
using Insight.Base.BaseForm.Utils;
using Insight.Base.BaseForm.ViewModels;

namespace Insight.Base.BaseForm.Controllers
{
    public class MdiController<T, TV, TM, DM> : BaseController where TV : BaseMdi, new() where TM : BaseMdiModel<T, TV, DM>, new() where DM : new()
    {
        /// <summary>
        /// Data Model
        /// </summary>
        protected readonly DM dataModel = new DM();

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
            mdiModel = new TM {dataModel = dataModel};
            mdiModel.initMdiView(module);
            mdiModel.callbackEvent += (sender, args) => buttonClick(args.methodName, args.param);
            mdiModel.show();

            mdiModel.initToolBar(module.functions);
        }

        /// <summary>
        /// 设计报表
        /// </summary>
        /// <typeparam name="TE">类型</typeparam>
        /// <param name="set">打印设置</param>
        public void design<TE>(PrintSetting<TE> set)
        {
            if (string.IsNullOrEmpty(set.template))
            {
                Messages.showError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return;
            }

            var report = buildReport(set);
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
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="set">打印设置</param>
        public void preview<TE>(PrintSetting<TE> set)
        {
            if (string.IsNullOrEmpty(set.template))
            {
                Messages.showError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return;
            }

            var report = buildReport(set);
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
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="set">打印设置</param>
        /// <returns>string 电子影像文件名</returns>
        public string print<TE>(PrintSetting<TE> set)
        {
            if (string.IsNullOrEmpty(set.template))
            {
                Messages.showError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return null;
            }

            var report = buildReport(set);
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
        /// <param name="set">打印参数集</param>
        /// <returns>Report FastReport报表</returns>
        private static Report buildReport<TE>(PrintSetting<TE> set)
        {
            if (set.template == null) return null;

            var report = new Report();
            report.LoadFromString(set.template);
            report.RegisterData(set.data, set.dataName);
            foreach (var i in set.parameter ?? new Dictionary<string, object>()) report.SetParameterValue(i.Key, i.Value);

            return report;
        }
    }
}