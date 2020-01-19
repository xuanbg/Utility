using System.Diagnostics;
using System.Windows.Forms;
using Insight.Utils.BaseViewModels;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.ViewModels
{
    public class AboutModel : BaseDialogModel<object, About>
    {
        /// <summary>
        /// 构造函数，初始化视图
        /// </summary>
        /// <param name="title">窗体标题</param>
        public AboutModel(string title) : base(title, null, true)
        {
            // 显示文件版本信息
            var fileVersion = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

            view.labProduct.Text = fileVersion.ProductName;
            view.labVer.Text = fileVersion.FileVersion;
            view.labDev.Text = fileVersion.CompanyName;
            view.txtDescription.Text = "程序设计：宣炳刚\r\n联系电话：13958085903";
        }
    }
}