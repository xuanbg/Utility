using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Utils.Common;
using Newtonsoft.Json;

namespace Insight.Utils.Controls.Nim
{
    public partial class MessageBox : XtraUserControl
    {
        private int maxWidth;
        private bool isSend;

        /// <summary>
        /// 设置宽度
        /// </summary>
        public int width
        {
            set
            {
                maxWidth = (value > 1000 ? 1000 : value) - 180;
                Width = value;
            }
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        public NimMessage message
        {
            set
            {
                isSend = value.direction == 0;
                switch (value.type)
                {
                    case 0:
                        showTextMessage(value);
                        break;
                    case 1:
                        showImageMessage(value);
                        break;
                    case 6:
                        showFileMessage(value);
                        break;
                    default:
                        value.body = new TextMessage{msg = "此版本不支持该消息类型" };
                        showTextMessage(value);
                        break;
                }
            }
        }

        /// <summary>
        /// 显示头像
        /// </summary>
        public Image headImage
        {
            set
            {
                picTarget.Visible = !isSend;
                picMe.Visible = isSend;
                if (value == null) return;

                if (isSend) picMe.Image = value;
                else picTarget.Image = value;
            }
        }

        /// <summary>
        /// 进度
        /// </summary>
        public int position
        {
            set => pbcSend.Position = value * 100;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public MessageBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示文本消息
        /// </summary>
        private void showTextMessage(NimMessage message)
        {
            var body = (TextMessage) message.body;
            var x = 70;
            var y = 5;

            // 计算字符宽度
            var rw = TextRenderer.MeasureText(body.msg, Font).Width;
            var tw = rw < maxWidth - 10 ? rw : maxWidth - 10;
            labMessage.Width = tw;
            labMessage.Text = body.msg;

            // 计算气泡宽高
            var th = labMessage.Height;
            if (th < 50) y = (50 - th) / 2;

            pceText.Width = tw + 10;
            pceText.Height = th + 10;

            // 计算控件宽高
            var h = pceText.Height < 60 ? 60 : pceText.Height;
            Height = h + 10;

            // 发送气泡靠右
            if (isSend)
            {
                x = Width - pceText.Width - 70;
                pceText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }

            pceText.Location = new Point(x, y);
            pceText.Visible = true;
        }

        /// <summary>
        /// 显示图片消息
        /// </summary>
        private void showImageMessage(NimMessage message)
        {
            var body = (FileMessage) message.body;
            var image = body.image ?? NimUtil.getImage(body);
            if (image == null) return;

            // 计算图片宽高
            var x = 70;
            var w = image.Width < maxWidth ? image.Width : maxWidth;
            var h = image.Width < maxWidth ? image.Height : image.Height * w / image.Width;
            picImage.Width = w;
            picImage.Height = h;
            picImage.Image = image;

            // 计算控件宽高
            Height = h + 10;

            // 发送图片靠右
            if (isSend)
            {
                x = Width - w - 70;
                picImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }

            picImage.Location = new Point(x, 5);
            picImage.Visible = true;
        }

        /// <summary>
        /// 显示文件消息
        /// </summary>
        private void showFileMessage(NimMessage message)
        {
            var body = (FileMessage) message.body;
            var x = 70;
            var y = 5;

            // 计算字符宽度
            var fileName = body.getAttach.name;
            var name = fileName.Substring(fileName.LastIndexOf('\\') + 1);
            var rw = TextRenderer.MeasureText(name, Font).Width;
            var tw = rw < maxWidth - 10 ? rw : maxWidth - 10;
            labMessage.Width = tw;
            labMessage.Text = name;

            // 计算气泡宽高
            var th = labMessage.Height;
            if (th < 50) y = (50 - th) / 2;

            pceText.Width = tw + 10;
            pceText.Height = th + 10;
            pbcSend.Width = tw + 10;
            pbcSend.Visible = true;

            // 计算控件宽高
            var h = pceText.Height < 60 ? 60 : pceText.Height;
            Height = h + 10;

            // 发送气泡靠右
            if (isSend)
            {
                x = Width - pceText.Width - 70;
                pceText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                pbcSend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }

            pceText.Location = new Point(x, y);
            pbcSend.Location = new Point(x, 32);
            pceText.Visible = true;
        }
    }

    public class NimMessage
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 云信消息ID
        /// </summary>
        public long msgid { get; set; }

        /// <summary>
        /// 发送者accid
        /// </summary>
        public string from { get; set; }

        /// <summary>
        /// 接收者accid
        /// </summary>
        public string to { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int ope => 0;

        /// <summary>
        /// 方向：0.发送;1.接收
        /// </summary>
        public int direction { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public object body { get; set; }

        /// <summary>
        /// 消息发送时间戳
        /// </summary>
        public long timetag { get; set; }
    }

    public class TextMessage
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        [JsonProperty("msg_body")]
        public string msg { get; set; }
    }

    public class FileMessage
    {
        /// <summary>
        /// 图片
        /// </summary>
        public Image image { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [JsonProperty("msg_attach")]
        public string attach { get; set; }

        /// <summary>
        /// 本地路径
        /// </summary>
        [JsonProperty("local_res_path")]
        public string localPath { get; set; }

        public Attach getAttach => Util.deserialize<Attach>(attach);
    }

    public class Attach
    {
        /// <summary>
        /// 图片名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string ext { get; set; }

        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string md5 { get; set; }

        /// <summary>
        /// 字节数
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 图片宽度
        /// </summary>
        public int w { get; set; }

        /// <summary>
        /// 图片高度
        /// </summary>
        public int h { get; set; }

        /// <summary>
        /// 文件URL
        /// </summary>
        public string url { get; set; }
    }
}
