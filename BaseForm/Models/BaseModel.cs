using System.Collections.Generic;
using System.Windows.Forms;
using FastReport;
using Insight.Utils.Client;
using Insight.Utils.Common;

namespace Insight.Utils.Models
{
    public class BaseModel
    {
        private readonly List<InputItem> checkItems = new List<InputItem>();

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
        /// 设置一个输入检查对象
        /// </summary>
        /// <param name="control"></param>
        /// <param name="key">输入对象的值</param>
        /// <param name="message">错误消息</param>
        /// <param name="clear">是否清除集合</param>
        public void setCheckItem(Control control, string key, string message, bool clear = false)
        {
            if (clear) checkItems.Clear();

            var item = new InputItem{control = control, key = key, message = message};
            checkItems.Add(item);
        }

        /// <summary>
        /// 设置多个输入检查对象
        /// </summary>
        /// <param name="items">输入检查对象集合</param>
        /// <param name="clear">是否清除集合</param>
        public void setCheckItems(IEnumerable<InputItem> items, bool clear = false)
        {
            checkItems.AddRange(items);
        }

        /// <summary>
        /// 检查输入检查对象是否都有值
        /// </summary>
        /// <returns>bool 对象是否都有值</returns>
        public bool inputExamine()
        {
            return new InputCheck(checkItems).result;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <typeparam name="TE"></typeparam>
        /// <param name="printer">打印机名称</param>
        /// <param name="tid">模板ID</param>
        /// <param name="name">数据源名称</param>
        /// <param name="data">数据</param>
        /// <param name="onSheet">合并打印模式</param>
        /// <returns>string 打印文档名称</returns>
        public string print<TE>(string printer, string tid, string name, List<TE> data, int onSheet = 0)
        {
            if (string.IsNullOrEmpty(tid))
            {
                Messages.showError("未配置打印模板，请先在选项中设置对应的打印模板！");
                return null;
            }

            var report = buildReport(tid, name, data);
            if (report == null || !report.Prepare())
            {
                Messages.showError("生成报表失败！");
                return null;
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
        /// <returns>Report FastReport报表</returns>
        private Report buildReport<TE>(string tid, string name, List<TE> data)
        {
            var url = $"{baseServer}/commonapi/v1.0/templates/{tid}";
            var client = new HttpClient<object>(tokenHelper);
            if (!client.get(url)) return null;

            var report = new Report();
            report.LoadFromString(client.data.ToString());
            report.RegisterData(data, name);

            return report;
        }
    }
}