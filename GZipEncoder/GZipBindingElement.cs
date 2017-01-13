using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace Microsoft.Samples.GZipEncoder
{
    public sealed class GZipBindingElement : MessageEncodingBindingElement, IPolicyExportExtension
    {
        // 使用内部绑定元素来存储内部编码器所需的信息
        private MessageEncodingBindingElement _InnerBindingElement;

        /// <summary>
        /// 内部消息编码绑定元素
        /// </summary>
        public MessageEncodingBindingElement InnerMessageEncodingBindingElement
        {
            get { return _InnerBindingElement; }
            set { _InnerBindingElement = value; }
        }

        /// <summary>
        /// 消息版本
        /// </summary>
        public override MessageVersion MessageVersion
        {
            get { return _InnerBindingElement.MessageVersion; }
            set { _InnerBindingElement.MessageVersion = value; }
        }

        /// <summary>
        /// 构造函数，使用默认文本编码器作为内部编码器
        /// </summary>
        public GZipBindingElement()
        {
            _InnerBindingElement = new TextMessageEncodingBindingElement();
        }

        /// <summary>
        /// 构造函数，使用指定的消息编码器
        /// </summary>
        /// <param name="messageEncoderBindingElement">指定的消息编码器</param>
        public GZipBindingElement(MessageEncodingBindingElement messageEncoderBindingElement)
        {
            _InnerBindingElement = messageEncoderBindingElement;
        }

        /// <summary>
        /// 创建一个消息编码工厂(进入编码器绑定元素的主入口点)
        /// </summary>
        /// <returns></returns>
        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new GZipEncoderFactory(_InnerBindingElement.CreateMessageEncoderFactory());
        }

        /// <summary>
        /// 克隆一个内置绑定元素
        /// </summary>
        /// <returns></returns>
        public override BindingElement Clone()
        {
            return new GZipBindingElement(_InnerBindingElement);
        }

        /// <summary>
        /// 获取指定类型的绑定参数属性
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="context">绑定参数</param>
        /// <returns>T 绑定参数属性</returns>
        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return _InnerBindingElement.GetProperty<T>(context);
            }

            return base.GetProperty<T>(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exporter"></param>
        /// <param name="policyContext"></param>
        void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext policyContext)
        {
            if (policyContext == null)
            {
                throw new ArgumentNullException(nameof(policyContext));
            }

            var document = new XmlDocument();
            policyContext.GetBindingAssertions().Add(document.CreateElement(GZipEncodingPolicy.GZipEncodingPrefix, GZipEncodingPolicy.GZipEncodingName, GZipEncodingPolicy.GZipEncodingNamespace));
        }
    }
}