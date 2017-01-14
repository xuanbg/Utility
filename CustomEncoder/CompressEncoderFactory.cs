using System.ServiceModel.Channels;

namespace Insight.WCF.CustomEncoder
{
    public class CompressEncoderFactory : MessageEncoderFactory
    {
        private readonly CompressEncoder _MessageEncoder;

        /// <summary>
        /// 消息编码器
        /// </summary>
        public override MessageEncoder Encoder => _MessageEncoder;

        /// <summary>
        /// 消息编码器的消息版本
        /// </summary>
        public override MessageVersion MessageVersion => InnerMessageEncodingBindingElement.MessageVersion;

        /// <summary>
        /// 消息编码器绑定元素
        /// </summary>
        public MessageEncodingBindingElement InnerMessageEncodingBindingElement { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="innerElement">消息编码器绑定元素</param>
        public CompressEncoderFactory(MessageEncodingBindingElement innerElement)
        {
            InnerMessageEncodingBindingElement = innerElement;
            _MessageEncoder = new CompressEncoder(this);
        }
    }
}