using System.Drawing;
using System.Windows.Forms;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.MainForm.Login.Views;

namespace Insight.Utils.MainForm.Login.Models
{
    public class WaitingModel
    {
        public Waiting view = new Waiting
        {
            Text = Setting.appName,
            Icon = new Icon("logo.ico"),
            BackgroundImage = Util.getImage("bg.png"),
            BackgroundImageLayout = ImageLayout.Stretch
        };
    }
}
