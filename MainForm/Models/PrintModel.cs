using System.Collections.Generic;
using System.Drawing.Printing;
using Insight.Utils.Client;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.Models
{
    public class PrintModel
    {
        public PrintSet view = new PrintSet();

        private readonly List<object> prints = new List<object>();

        /// <summary>
        /// 构造函数，初始化视图
        /// 通过订阅事件实现双向数据绑定
        /// </summary>
        public PrintModel()
        {
            // 读取系统安装打印机列表
            var list = PrinterSettings.InstalledPrinters;
            prints.Add("请设置默认打印机…");
            foreach (var p in list)
            {
                prints.Add(p);
            }

            // 使用系统安装打印机列表初始化下拉列表
            view.DocPrint.Properties.Items.AddRange(prints);
            view.TagPrint.Properties.Items.AddRange(prints);
            view.BilPrint.Properties.Items.AddRange(prints);

            // 初始化控件初值
            view.DocPrint.EditValue = string.IsNullOrEmpty(Setting.docPrint) ? prints[0] : Setting.docPrint;
            view.TagPrint.EditValue = string.IsNullOrEmpty(Setting.tagPrint) ? prints[0] : Setting.tagPrint;
            view.BilPrint.EditValue = string.IsNullOrEmpty(Setting.bilPrint) ? prints[0] : Setting.bilPrint;
            view.MergerPrint.Checked = Setting.isMergerPrint;

            // 订阅下拉列表事件绑定数据
            view.DocPrint.EditValueChanged += (sender, args) => Setting.docPrint = view.DocPrint.SelectedIndex < 1 ? "" : view.DocPrint.Text;
            view.BilPrint.EditValueChanged += (sender, args) => Setting.bilPrint = view.DocPrint.SelectedIndex < 1 ? "" : view.BilPrint.Text;
            view.TagPrint.EditValueChanged += (sender, args) => Setting.tagPrint = view.DocPrint.SelectedIndex < 1 ? "" : view.TagPrint.Text;
            view.MergerPrint.CheckedChanged += (sender, args) => Setting.isMergerPrint = view.MergerPrint.Checked;
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        public void Save()
        {
            Setting.SavePrinter("docPrint", Setting.docPrint);
            Setting.SavePrinter("tagPrint", Setting.tagPrint);
            Setting.SavePrinter("bilPrint", Setting.bilPrint);
            Setting.SaveIsMergerPrint();
        }
    }
}
