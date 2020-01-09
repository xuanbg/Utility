using System.Collections.Generic;
using System.Windows.Forms;
using FastReport;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entities;

namespace Insight.Utils.Models
{
    public class BaseModel
    {
        private readonly List<InputItem> checkItems = new List<InputItem>();

        /// <summary>
        /// 令牌管理器
        /// </summary>
        protected readonly TokenHelper tokenHelper = Setting.tokenHelper;

        /// <summary>
        /// 服务地址
        /// </summary>
        protected string gateway = Setting.gateway;

        /// <summary>
        /// 设置一个输入检查对象
        /// </summary>
        /// <param name="control"></param>
        /// <param name="key">输入对象的值</param>
        /// <param name="message">错误消息</param>
        /// <param name="clear">是否清除集合</param>
        protected void setCheckItem(Control control, string key, string message, bool clear = false)
        {
            if (clear) checkItems.Clear();

            var item = new InputItem{control = control, key = key, message = message};
            checkItems.Add(item);
        }

        /// <summary>
        /// 设置多个输入检查对象
        /// </summary>
        /// <param name="items">输入检查对象集合</param>
        public void setCheckItems(IEnumerable<InputItem> items)
        {
            checkItems.AddRange(items);
        }

        /// <summary>
        /// 检查输入检查对象是否都有值
        /// </summary>
        /// <returns>bool 对象是否都有值</returns>
        protected bool inputExamine()
        {
            return new InputCheck(checkItems).result;
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
            var url = $"{gateway}/commonapi/v1.0/templates/{tid}";
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
    }
}