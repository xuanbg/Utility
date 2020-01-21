using System.Drawing;
using System.Windows.Forms;
using Insight.Utils.BaseViewModels;
using Insight.Utils.Common;
using Insight.Utils.MainForm.Views;

namespace Insight.Utils.MainForm.ViewModels
{
    public class WaitingModel : BaseModel<Waiting>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title">窗体标题</param>
        public WaitingModel(string title) : base(title)
        {
            view.Icon = new Icon("logo.ico");
            view.BackgroundImage = Util.getImage("bg.png");
            view.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
