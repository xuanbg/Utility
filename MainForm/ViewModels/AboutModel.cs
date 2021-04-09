using System.Diagnostics;
using System.Windows.Forms;
using Insight.Base.BaseForm.ViewModels;
using Insight.Base.MainForm.Views;

namespace Insight.Base.MainForm.ViewModels
{
    public class AboutModel : BaseDialogModel<object, AboutDialog>
    {
        /// <summary>
        /// 构造函数，初始化视图
        /// </summary>
        /// <param name="title">窗体标题</param>
        public AboutModel(string title) : base(title, null, true)
        {
            var fileVersion = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            view.labProduct.Text = fileVersion.ProductName;
            view.labVer.Text = fileVersion.FileVersion;
            view.labDev.Text = fileVersion.CompanyName;

            view.txtDescription.Text = "";
            view.txtDescription.Enabled = false;
        }
    }
}