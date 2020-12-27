using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Insight.Base.BaseForm.Utils;
using Insight.Utils.Common;
using NIM;
using NIM.Session;

namespace Insight.Utils.NetEaseIM.Controls
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
            var client = new HttpClient<NimUser>(url);

            return client.getData();
        }

        /// <summary>
        /// 获取消息中的图片
        /// </summary>
        /// <param name="message">图片消息</param>
        /// <returns>Image</returns>
        public static Image getImage(FileMessage message)
        {
            var image = Util.getImageFromFile(message.localPath);
            if (image != null) return image;

            var attach = message.getAttach;

            return Util.getImageFromUrl(attach.url);
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
                case NIMMessageType.kNIMMessageTypeTips:
                    return info.Content;
                case NIMMessageType.kNIMMessageTypeImage:
                    return "[图片]";
                case NIMMessageType.kNIMMessageTypeFile:
                    return "[文件]";
                case NIMMessageType.kNIMMessageTypeAudio:
                    return "[文件]";
                case NIMMessageType.kNIMMessageTypeCustom:
                    return "[产品咨询]";
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
                case NIMMessageType.kNIMMessageTypeTips:
                    return Util.deserialize<TextMessage>(str);
                case NIMMessageType.kNIMMessageTypeImage:
                case NIMMessageType.kNIMMessageTypeFile:
                case NIMMessageType.kNIMMessageTypeAudio:
                case NIMMessageType.kNIMMessageTypeCustom:
                    return Util.deserialize<FileMessage>(str);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 删除指定控件的指定事件
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="eventName">事件名称</param>
        public static void clearEvent(Control control, string eventName)
        {
            if (control == null || string.IsNullOrEmpty(eventName)) return;

            const BindingFlags propertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;
            const BindingFlags fieldFlags = BindingFlags.Static | BindingFlags.NonPublic;

            var controlType = typeof(Control);
            var propertyInfo = controlType.GetProperty("Events", propertyFlags);
            var eventHandlerList = (EventHandlerList)propertyInfo?.GetValue(control, null);
            var fieldInfo = typeof(Control).GetField("Event" + eventName, fieldFlags);
            var d = eventHandlerList?[fieldInfo?.GetValue(control)];
            if (d == null) return;

            var eventInfo = controlType.GetEvent(eventName);
            foreach (var dx in d.GetInvocationList())
            {
                eventInfo.RemoveEventHandler(control, dx);
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
