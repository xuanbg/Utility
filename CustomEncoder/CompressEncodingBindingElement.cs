using System.ServiceModel.Channels;
using System.Xml;

namespace Insight.WCF.CustomEncoder
{
    public sealed class CompressEncodingBindingElement : MessageEncodingBindingElement
    {
        private readonly XmlDictionaryReaderQuotas _ReaderQuotas;
        private readonly CompressAlgorithm _Algorithm;

        /// <summary>
        /// 消息编码器绑定元素
        /// </summary>
        public MessageEncodingBindingElement InnerMessageEncodingBindingElement { get; }

        /// <summary>
        /// 压缩方式
        /// </summary>
        public CompressAlgorithm CompressAlgorithm => _Algorithm;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="innerElement">消息编码器绑定元素</param>
        /// <param name="algorithm">压缩方式</param>
        public CompressEncodingBindingElement(MessageEncodingBindingElement innerElement, CompressAlgorithm algorithm)
        {
            _ReaderQuotas = new XmlDictionaryReaderQuotas();
            _Algorithm = algorithm;
            InnerMessageEncodingBindingElement = innerElement;
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }

        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelFactory<TChannel>();
        }

        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }

        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new CompressEncoderFactory(InnerMessageEncodingBindingElement, _Algorithm);
        }

        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas)) return _ReaderQuotas as T;

            return base.GetProperty<T>(context);
        }

        public override MessageVersion MessageVersion
        {
            get { return InnerMessageEncodingBindingElement.MessageVersion; }
            set { InnerMessageEncodingBindingElement.MessageVersion = value; }
        }

        public override BindingElement Clone()
        {
            return new CompressEncodingBindingElement(InnerMessageEncodingBindingElement, _Algorithm);
        }
    }
}