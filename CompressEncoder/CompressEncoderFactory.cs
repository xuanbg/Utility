using System.ServiceModel.Channels;

namespace Insight.WCF.CompressEncoder
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
        /// <param name="algorithm">压缩方式</param>
        public CompressEncoderFactory(MessageEncodingBindingElement innerElement, CompressAlgorithm algorithm)
        {
            InnerMessageEncodingBindingElement = innerElement;
            _MessageEncoder = new CompressEncoder(this, algorithm);
        }
    }
}