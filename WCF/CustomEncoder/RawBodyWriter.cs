using System.ServiceModel.Channels;
using System.Xml;

namespace Insight.WCF.CustomEncoder
{
    public class RawBodyWriter : BodyWriter
    {
        private readonly byte[] _Content;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="content">写入内容</param>
        public RawBodyWriter(byte[] content) : base(true)
        {
            _Content = content;
        }

        /// <summary>
        /// 将内容写入到流
        /// </summary>
        /// <param name="writer">XmlDictionaryWriter</param>
        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            writer.WriteStartElement("Binary");
            writer.WriteBase64(_Content, 0, _Content.Length);
            writer.WriteEndElement();
        }
    }
}
