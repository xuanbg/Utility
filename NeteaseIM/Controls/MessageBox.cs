﻿using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Insight.Base.BaseForm.Utils;
using Insight.Base.BaseForm.Views;
using Insight.Utils.Common;
using Newtonsoft.Json;
using NIMAudio;

namespace Insight.Utils.NetEaseIM.Controls
{
    public partial class MessageBox : XtraUserControl
    {
        private CancellationTokenSource tokenSource;
        private Image image;
        private int maxWidth;
        private bool isSend;
        private string localFilePath;
        private int dur;
        private bool stopping;

        /// <summary>
        /// 播放语音的委托方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void PlayAudioHandle(object sender, PlayEventArgs e);

        /// <summary>
        /// 停止播放语音的委托方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void StopAudioHandle(object sender, PlayEventArgs e);

        /// <summary>
        /// 播放语音的事件
        /// </summary>
        public event PlayAudioHandle play;

        /// <summary>
        /// 停止播放语音的事件
        /// </summary>
        public event StopAudioHandle stop;

        /// <summary>
        /// 消息类型
        /// </summary>
        public int type;

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
                type = value.type;
                isSend = value.direction == 0;
                picTarget.Visible = !isSend;
                picMe.Visible = isSend;
                Name = value.id;

                switch (value.type)
                {
                    case 0:
                    case 10:
                        showTextMessage(value);
                        break;
                    case 1:
                        showImageMessage(value);
                        break;
                    case 2:
                        showAudioMessage(value);
                        break;
                    case 6:
                        showFileMessage(value);
                        break;
                    case 100:
                        showCustomMessage(value);
                        break;
                    default:
                        value.body = new TextMessage{msg = "此版本不支持该消息类型" };
                        showTextMessage(value);
                        break;
                }
            }
        }

        /// <summary>
        /// 对方头像
        /// </summary>
        public Image targetHead
        {
            set
            {
                if (!isSend && value != null)
                {
                    picTarget.Image = value;
                }
            }
        }

        /// <summary>
        /// 进度
        /// </summary>
        public int position
        {
            set => pbcSend.Position = value;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public MessageBox()
        {
            InitializeComponent();

            if(Setting.myHead != null) picMe.Image = Setting.myHead;

            picImage.Click += (sender, args) =>
            {
                var show = new ShowImage();
                show.show(image);
                show.ShowDialog();
            };
            sbePlay.Click += (sender, args) => play?.Invoke(this, new PlayEventArgs(Name));
            sbeStop.Click += (sender, args) => stopAudio();
            sbeDownload.Click += (sender, args) => saveFile();
        }

        /// <summary>
        /// 播放语音
        /// </summary>
        public void playAudio()
        {
            var start = DateTime.Now;
            var end = start.AddMilliseconds(dur + 1000);
            Invoke(new Action(() =>
            {
                sbePlay.Visible = false;
                sbeStop.Visible = true;
                Refresh();
            }));

            tokenSource = new CancellationTokenSource();
            AudioAPI.RegStopPlayCb((z, xx, c, v) =>
            {
                stop?.Invoke(this, new PlayEventArgs(Name));
                if (stopping)
                {
                    stopping = false;
                    return;
                }

                Invoke(new Action(() =>
                {
                    sbePlay.Visible = true;
                    sbeStop.Visible = false;
                    Refresh();
                }));
            });
            AudioAPI.PlayAudio(localFilePath, "", "", NIMAudioType.kNIMAudioAAC);

            Task.Run(() =>
            {
                while (DateTime.Now.CompareTo(end) < 0)
                {
                    if (tokenSource.IsCancellationRequested) break;

                    Thread.Sleep(200);
                    Invoke(new Action(() =>
                    {
                        pbcSend.Position = (int)(100 * (DateTime.Now - start).TotalMilliseconds / dur);
                        pbcSend.Refresh();
                    }));
                }
            });
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public void stopAudio()
        {
            if (tokenSource == null || tokenSource.IsCancellationRequested) return;

            AudioAPI.StopPlayAudio();
            tokenSource.Cancel();

            stopping = true;
            sbePlay.Visible = true;
            sbeStop.Visible = false;
            Refresh();
        }

        /// <summary>
        /// 显示文本消息
        /// </summary>
        /// <param name="message">云信消息数据</param>
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
            var th = labMessage.Height + 12;
            if (th < 50) y = (60 - th) / 2;

            pceText.Width = tw + 10;
            pceText.Height = th;

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
        /// <param name="message">云信消息数据</param>
        private void showImageMessage(NimMessage message)
        {
            var body = (FileMessage) message.body;
            image = body.image ?? NimUtil.getImage(body);
            if (image == null) return;

            // 计算图片宽高
            var x = 70;
            var w = image.Width < maxWidth ? image.Width : maxWidth;
            var h = image.Width < maxWidth ? image.Height : image.Height * w / image.Width;
            picImage.Width = w;
            picImage.Height = h;
            picImage.Image = image;

            // 计算控件宽高
            Height = h > 60 ? h + 10 : 70;

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
        /// 显示音频消息
        /// </summary>
        /// <param name="message">云信消息数据</param>
        private void showAudioMessage(NimMessage message)
        {
            if (!AudioAPI.InitModule(Application.StartupPath)) return;

            var body = (FileMessage)message.body;
            var x = 70;
            var y = 5;
            dur = body.getAttach.dur;
            localFilePath = body.localPath;

            // 计算字符宽度
            var info = $"语音聊天 - {body.getAttach.dur / 1000} 秒";
            var rw = TextRenderer.MeasureText(info, Font).Width;
            labMessage.Width = rw;
            labMessage.Text = info;

            // 计算气泡宽高
            var th = labMessage.Height + 12;
            if (th < 50) y = (60 - th) / 2;

            pceText.Width = rw + 30;
            pceText.Height = th;
            pbcSend.Width = rw + 30;
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
            pbcSend.Location = new Point(x, y + th + 3);
            pceText.Visible = true;
            sbePlay.Location = new Point(pceText.Width - 25, 2);
            sbeStop.Location = new Point(pceText.Width - 25, 2);
            sbePlay.Visible = true;
        }

        /// <summary>
        /// 显示文件消息
        /// </summary>
        /// <param name="message">云信消息数据</param>
        private void showFileMessage(NimMessage message)
        {
            var body = (FileMessage) message.body;
            var x = 70;
            var y = 5;
            localFilePath = body.localPath;

            // 计算字符宽度
            var fileName = body.getAttach.name;
            var name = fileName.Substring(fileName.LastIndexOf('\\') + 1);
            var rw = TextRenderer.MeasureText(name, Font).Width;
            var tw = rw < maxWidth - 10 ? rw : maxWidth - 10;
            labMessage.Width = tw;
            labMessage.Text = name;

            // 计算气泡宽高
            var th = labMessage.Height + 12;
            if (th < 50) y = (60 - th) / 2;

            pceText.Width = tw + 30;
            pceText.Height = th;
            pbcSend.Width = tw + 30;
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
            pbcSend.Location = new Point(x, y + th + 3);
            pceText.Visible = true;
            sbeDownload.Location = new Point(pceText.Width - 25, 2);
            sbeDownload.Visible = true;
        }

        /// <summary>
        /// 显示自定义消息
        /// </summary>
        /// <param name="message">云信消息数据</param>
        private void showCustomMessage(NimMessage message)
        {
            var body = (FileMessage)message.body;
            var info = body.getAttach;

            // 计算字符宽度
            var name = $"我想咨询一下产品【{info.name}】";
            var rw = TextRenderer.MeasureText(name, Font).Width;
            var tw = rw < maxWidth - 80 ? rw : maxWidth - 80;
            labMessage.Width = tw;
            labMessage.Text = name;

            // 计算气泡宽高
            var y = 5;
            var th = labMessage.Height + 12;
            if (th < 50) y = (60 - th) / 2;

            pceText.Width = tw + 10;
            pceText.Height = labMessage.Height + 12;
            pceText.Location = new Point(70, y);
            pceText.Visible = true;

            // 显示图片
            image = Util.getImageFromUrl(info.image);
            picImage.Image = image;
            picImage.Location = new Point(tw + 85, 5);
            picImage.Visible = true;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        private void saveFile()
        {
        }
    }

    public class PlayEventArgs : EventArgs
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public readonly string id;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id">消息ID</param>
        public PlayEventArgs(string id)
        {
            this.id = id;
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

        /// <summary>
        /// 获取附件
        /// </summary>
        public Attach getAttach => Util.deserialize<Attach>(attach);
    }

    public class Attach
    {
        /// <summary>
        /// 图片名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 图片URL
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string ext { get; set; }

        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string md5 { get; set; }

        /// <summary>
        /// 播放时长
        /// </summary>
        public int dur { get; set; }

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
