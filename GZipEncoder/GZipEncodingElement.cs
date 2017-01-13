using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Microsoft.Samples.GZipEncoder
{
    public class GZipEncodingElement : BindingElementExtensionElement
    {
        //Called by the WCF to discover the type of binding element this config section enables
        public override Type BindingElementType => typeof(GZipBindingElement);

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
            var bindingElement = new GZipBindingElement();
            ApplyConfiguration(bindingElement);
            return bindingElement;
        }

        /// <summary>
        /// 由WCF调用以将配置设置（上述属性）应用于绑定元素
        /// </summary>
        /// <param name="bindingElement"></param>
        public override void ApplyConfiguration(BindingElement bindingElement)
        {
            var binding = (GZipBindingElement)bindingElement;
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