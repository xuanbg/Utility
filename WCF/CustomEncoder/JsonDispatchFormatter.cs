using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;

namespace Insight.WCF.CustomEncoder
{
    public class JsonDispatchFormatter : IDispatchMessageFormatter
    {
        private readonly IDispatchMessageFormatter _InnerFormatter;
        private readonly OperationDescription _Operation;

        /// <summary>
        /// 构造方法，传入内置消息格式化器
        /// </summary>
        /// <param name="formatter">内置消息格式化器</param>
        /// <param name="operation">传入的操作</param>
        public JsonDispatchFormatter(IDispatchMessageFormatter formatter, OperationDescription operation)
        {
            _InnerFormatter = formatter;
            _Operation = operation;
        }

        /// <summary>
        /// 反序列化请求消息
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="parameters">请求参数集合</param>
        public void DeserializeRequest(Message message, object[] parameters)
        {
            var parts = _Operation.Messages[0].Body.Parts;
            var partNames = parts.ToDictionary(part => part.Name, part => part.Index);

            _InnerFormatter.DeserializeRequest(message, parameters);
        }

        /// <summary>
        /// 序列化响应消息
        /// </summary>
        /// <param name="messageVersion">MessageVersion对象</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="result">响应数据</param>
        /// <returns>Message</returns>
        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream, Encoding.UTF8);
            var writer = new JsonTextWriter(streamWriter) {Formatting = Formatting.Indented};
            new JsonSerializer().Serialize(writer, result);
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return WebOperationContext.Current.CreateStreamResponse(stream, "application/json");
        }
    }
}
