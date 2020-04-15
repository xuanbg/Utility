using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Insight.Utils.Controls.Nim
{
    public partial class ShowImage : XtraForm
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ShowImage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="image"></param>
        public void show(Image image)
        {
            picImage.Image = image;
            if (image.Width > 1600 || image.Height > 900)
            {
                Width = 1616;
                Height = 939;
                picImage.Dock = DockStyle.None;
                picImage.Location = new Point(0, 0);
                picImage.Width = image.Width;
                picImage.Height = image.Height;
            }
            else if (image.Width > 300 || image.Height > 300)
            {
                Width = image.Width + 16;
                Height = image.Height + 39;
            }

            Refresh();
        }
    }
}
