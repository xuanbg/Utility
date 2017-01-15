using System.ServiceModel.Channels;
using System.Xml;

namespace Insight.WCF.CustomEncoder
{
    public sealed class CompressEncodingBindingElement : MessageEncodingBindingElement
    {
        private readonly XmlDictionaryReaderQuotas _ReaderQuotas;

        /// <summary>
        /// 消息编码器绑定元素
        /// </summary>
        public MessageEncodingBindingElement InnerMessageEncodingBindingElement { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="innerElement">消息编码器绑定元素</param>
        public CompressEncodingBindingElement(MessageEncodingBindingElement innerElement)
        {
            _ReaderQuotas = new XmlDictionaryReaderQuotas();
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
            return new CompressEncoderFactory(InnerMessageEncodingBindingElement);
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
            return new CompressEncodingBindingElement(InnerMessageEncodingBindingElement);
        }
    }
}