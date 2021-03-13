using System;
using System.Collections.Generic;
using System.IO;
using FastReport;
using Insight.Base.BaseForm.Entities;
using Insight.Base.BaseForm.Forms;
using Insight.Base.BaseForm.Utils;
using Insight.Base.BaseForm.ViewModels;
using Insight.Utils.Common;

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
            mdiModel = new TM {dataModel = dataModel, moduleId = module.id};

            mdiModel.initMdiView(module);
            mdiModel.callbackEvent += (sender, args) => buttonClick(args.methodName, args.param);
            mdiModel.show();

            mdiModel.initToolBar(module.functions);
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="id">报表ID</param>
        protected bool preview(long id)
        {
            var report = buildReport(id);
            if (report == null) return false;

            report.ShowPrepared(true);
            return true;
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <typeparam name="E">数据类型</typeparam>
        /// <param name="set">打印设置</param>
        protected void preview<E>(PrintSetting<E> set)
        {
            try
            {
                var report = buildReport(set);
                if (report == null || !report.Prepare())
                {
                    Messages.showError("生成报表失败！");
                    return;
                }

                if (set.pagesOnSheet == PagesOnSheet.One)
                {
                    report.PrintSettings.PrintMode = set.printMode;
                }
                else
                {
                    report.PrintSettings.PrintMode = PrintMode.Scale;
                    report.PrintSettings.PagesOnSheet = set.pagesOnSheet;
                }

                report.ShowPrepared(true);
            }
            catch (Exception e)
            {
                Messages.showError(e.Message);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="id">报表ID</param>
        /// <returns>string 电子影像文件名</returns>
        protected bool print(long id)
        {
            var report = buildReport(id);
            if (report == null) return false;

            report.PrintPrepared();
            return true;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <typeparam name="E">数据类型</typeparam>
        /// <param name="set">打印设置</param>
        /// <param name="id">报表ID</param>
        /// <returns>string 电子影像文件名</returns>
        protected void print<E>(PrintSetting<E> set, long? id = null)
        {
            try
            {
                var report = buildReport(set);
                if (report == null || !report.Prepare())
                {
                    Messages.showError("生成报表失败！");
                    return;
                }

                if (id != null)
                {
                    Stream stream = new MemoryStream();
                    report.SavePrepared(stream);

                    var bytes = new byte[stream.Length];
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Read(bytes, 0, bytes.Length);

                    const string url = "/common/report/v1.0/reports";
                    var data = new ReportDto {id = (long) id, bytes = bytes};
                    var client = new HttpClient<object>(url);

                    client.post(data);
                }

                if (set.pagesOnSheet == PagesOnSheet.One)
                {
                    report.PrintSettings.PrintMode = set.printMode;
                }
                else
                {
                    report.PrintSettings.PrintMode = PrintMode.Scale;
                    report.PrintSettings.PagesOnSheet = set.pagesOnSheet;
                }

                if (!string.IsNullOrEmpty(set.printer))
                {
                    report.PrintSettings.ShowDialog = false;
                    report.PrintSettings.Printer = set.printer;
                }

                report.PrintSettings.Copies = set.copies;
                report.PrintPrepared();
            }
            catch (Exception ex)
            {
                Messages.showError(ex.Message);
            }
        }

        /// <summary>
        /// 生成报表
        /// </summary>
        /// <typeparam name="E">类型</typeparam>
        /// <param name="set">打印参数集</param>
        /// <returns>Report FastReport报表</returns>
        private static Report buildReport<E>(PrintSetting<E> set)
        {
            if (string.IsNullOrEmpty(set.template))
            {
                Messages.showError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return null;
            }

            var report = new Report();
            report.LoadFromString(set.template);
            if (set.data != null)
            {
                report.RegisterData(set.data, set.dataName);
            }

            if (set.parameter != null)
            {
                foreach (var i in set.parameter ?? new Dictionary<string, object>())
                {
                    report.SetParameterValue(i.Key, i.Value);
                }
            }

            return report;
        }

        /// <summary>
        /// 生成报表
        /// </summary>
        /// <param name="id">报表ID</param>
        /// <returns>Report FastReport报表</returns>
        private static Report buildReport(long? id)
        {
            if (id == null) return null;

            var url = $"/common/report/v1.0/reports/{id}";
            var client = new HttpClient<ReportDto>(url, false);
            var content = client.getData()?.content;
            if (string.IsNullOrEmpty(content)) return null;

            try
            {
                var bytes = Util.deserialize<byte[]>(content);
                var report = new Report();
                report.LoadPrepared(new MemoryStream(bytes));

                return report;
            }
            catch (Exception ex)
            {
                Messages.showError(ex.Message);
                return null;
            }
        }
    }
}