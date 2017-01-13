using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Microsoft.Samples.GZipEncoder
{
    public class GZipMessageEncodingElement : BindingElementExtensionElement
    {
        /// <summary>
        /// 由WCF调用以发现此配置部分启用的绑定元素的类型
        /// </summary>
        public override Type BindingElementType => typeof(GZipMessageEncodingBindingElement);

        /// <summary>
        /// 内部消息编码器类型
        /// </summary>
        [ConfigurationProperty("innerMessageEncoding", DefaultValue = "textMessageEncoding")]
        public string InnerMessageEncoding
        {
            get { return (string)base["innerMessageEncoding"]; }
            set { base["innerMessageEncoding"] = value; }
        }

        /// <summary>
        /// 由WCF调用以创建绑定元素
        /// </summary>
        /// <returns>BindingElement 绑定元素</returns>
        protected override BindingElement CreateBindingElement()
        {
            var bindingElement = new GZipMessageEncodingBindingElement();
            ApplyConfiguration(bindingElement);
            return bindingElement;
        }

        /// <summary>
        /// 由WCF调用以将配置设置（上述属性）应用于绑定元素
        /// </summary>
        /// <param name="bindingElement"></param>
        public override void ApplyConfiguration(BindingElement bindingElement)
        {
            var binding = (GZipMessageEncodingBindingElement)bindingElement;
            var propertyInfo = ElementInformation.Properties;
            if (propertyInfo["innerMessageEncoding"]?.ValueOrigin == PropertyValueOrigin.Default) return;

            switch (InnerMessageEncoding)
            {
                case "textMessageEncoding":
                    binding.InnerMessageEncodingBindingElement = new TextMessageEncodingBindingElement();
                    break;
                case "binaryMessageEncoding":
                    binding.InnerMessageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
                    break;
            }
        }
    }
}