using System;
using DevExpress.XtraEditors;

namespace Insight.Utils.Controls.Nim
{
    public partial class ButtonLabel : XtraUserControl
    {
        /// <summary>
        /// 控件点击事件的委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public new delegate void Click(object sender, EventArgs args);

        /// <summary>
        /// 控件点击事件
        /// </summary>
        public event Click click;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ButtonLabel()
        {
            InitializeComponent();

            labButton.Click += (sender, args) => click?.Invoke(sender, args);
        }
    }
}
