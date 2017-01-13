using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace Insight.WCF.Compression
{
    public class MessageCompressor
    {
        private readonly CompressionAlgorithm _Algorithm;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">压缩类型</param>
        public MessageCompressor(CompressionAlgorithm algorithm)
        {
            _Algorithm = algorithm;
        }

        /// <summary>
        /// 对Message进行压缩
        /// </summary>
        /// <param name="sourceMessage">源Message</param>
        /// <returns>Message 压缩后的Message</returns>
        public Message CompressMessage(Message sourceMessage)
        {
            byte[] buffer;
            using (var reader = sourceMessage.GetReaderAtBodyContents())
            {
                buffer = Encoding.UTF8.GetBytes(reader.ReadOuterXml());
            }
            if (buffer.Length == 0)
            {
                var emptyMessage = Message.CreateMessage(sourceMessage.Version, null);
                sourceMessage.Headers.CopyHeadersFrom(sourceMessage);
                sourceMessage.Properties.CopyProperties(sourceMessage.Properties);
                emptyMessage.Close();
                return emptyMessage;
            }

            var compressedData = DataCompressor.Compress(buffer, _Algorithm);
            var copressedBody = CompressionUtil.CreateCompressedBody(compressedData);
            var xmlReader = new XmlTextReader(new StringReader(copressedBody), new NameTable());
            var message = Message.CreateMessage(sourceMessage.Version, null, xmlReader);
            message.Headers.CopyHeadersFrom(sourceMessage);
            message.Properties.CopyProperties(sourceMessage.Properties);
            message.AddCompressionHeader(_Algorithm);
            sourceMessage.Close();
            return message;
        }

        /// <summary>
        /// 对Message进行解压
        /// </summary>
        /// <param name="sourceMessage">源Message</param>
        /// <returns>Message 解压后的Message</returns>
        public Message DecompressMessage(Message sourceMessage)
        {
            if (!sourceMessage.IsCompressed())
            {
                return sourceMessage;
            }

            var algorithm = sourceMessage.GetCompressionAlgorithm();
            sourceMessage.RemoveCompressionHeader();
            var compressedBody = sourceMessage.GetCompressedBody();
            var decompressedBody = DataCompressor.Decompress(compressedBody, algorithm);
            var newMessageXml = Encoding.UTF8.GetString(decompressedBody);
            var reader2 = new XmlTextReader(new StringReader(newMessageXml));
            var newMessage = Message.CreateMessage(sourceMessage.Version, null, reader2);
            newMessage.Headers.CopyHeadersFrom(sourceMessage);
            newMessage.Properties.CopyProperties(sourceMessage.Properties);
            return newMessage;
        }
    }
}