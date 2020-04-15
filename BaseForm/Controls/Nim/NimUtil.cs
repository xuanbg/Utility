using System;
using System.Drawing;
using System.Net;
using Insight.Utils.Client;
using Insight.Utils.Common;
using NIM;
using NIM.Session;

namespace Insight.Utils.Controls.Nim
{
    public static class NimUtil
    {
        /// <summary>
        /// 获取云信用户名片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static NimUser getUser(string id)
        {
            if (string.IsNullOrEmpty(id)) return new NimUser{name = "未知好友"};

            var url = $"{Setting.gateway}/common/message/v1.0/nimusers/{id}";
            var client = new HttpClient<NimUser>();

            return client.getData(url);
        }

        /// <summary>
        /// 获取网络图片
        /// </summary>
        /// <param name="url">图片URL</param>
        /// <returns>Image</returns>
        public static Image getImage(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;

            var iconUrl = Uri.UnescapeDataString(url);
            if (!Uri.IsWellFormedUriString(iconUrl, UriKind.RelativeOrAbsolute)) return null;

            try
            {
                using (var stream = WebRequest.Create(url).GetResponse().GetResponseStream())
                {
                    if (stream != null)
                    {
                        return Image.FromStream(stream);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        /// <summary>
        /// 获取消息中的图片
        /// </summary>
        /// <param name="message">图片消息</param>
        /// <returns>Image</returns>
        public static Image getImage(FileMessage message)
        {
            var image = Util.getImage(message.localPath);
            if (image != null) return image;

            var attach = message.getAttach;

            return getImage(attach.url);
        }

        /// <summary>
        /// 读取会话中消息内容
        /// </summary>
        /// <param name="info">会话信息</param>
        /// <returns>消息内容</returns>
        public static string readMsg(SessionInfo info)
        {
            switch (info.MsgType)
            {
                case NIMMessageType.kNIMMessageTypeText:
                    return info.Content;
                case NIMMessageType.kNIMMessageTypeImage:
                    return "[图片]";
                case NIMMessageType.kNIMMessageTypeFile:
                    return "[文件]";
                default:
                    return "[未知]";
            }
        }

        /// <summary>
        /// 读取消息内容
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>消息内容</returns>
        public static object getMsg(NIMIMMessage message)
        {
            var str = message.Serialize();
            switch (message.MessageType)
            {
                case NIMMessageType.kNIMMessageTypeText:
                    return Util.deserialize<TextMessage>(str);
                case NIMMessageType.kNIMMessageTypeImage:
                    return Util.deserialize<FileMessage>(str);
                case NIMMessageType.kNIMMessageTypeFile:
                    return Util.deserialize<FileMessage>(str);
                default:
                    return null;
            }
        }
    }

    public class NimUser
    {
        /// <summary>
        /// 云信用户ID
        /// </summary>
        public string accid { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int gender { get; set; }
    }
}
