using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace Insight.WCF.CustomEncoder
{
    public static class MessageCompress
    {
        public static string Namespace = "http://myjece";

        public static Message CompressMessage(Message source)
        {
            var ms = new MemoryStream();
            var writer = XmlDictionaryWriter.CreateTextWriter(ms, Encoding.UTF8);
            source.WriteBodyContents(writer);
            writer.Flush();
            var buffer = ms.ToArray();
            ms.Close();

            if (buffer.Length <= 256) return source;

            var compressedData = Compress.Zip(buffer);
            var compressedBody = CreateCompressedBody(compressedData);
            var reader = new XmlTextReader(new StringReader(compressedBody), new NameTable());
            source.AddCompressionHeader();
            var message = Message.CreateMessage(source.Version, null, reader);
            message.Headers.CopyHeadersFrom(source);
            message.Properties.CopyProperties(source.Properties);
            return message;
        }

        public static Message DeCompressMessage(Message sourceMessage)
        {
            if (!sourceMessage.IsCompressed())
            {
                return sourceMessage;
            }
            sourceMessage.RemoveCompressionHeader();
            var deCompressedBody = Encoding.UTF8.GetString(Compress.UnZip(sourceMessage.GetCompressedBody()));

            var reader = new XmlTextReader(new StringReader(deCompressedBody), new NameTable());
            var message = Message.CreateMessage(sourceMessage.Version, null, reader);
            message.Headers.CopyHeadersFrom(sourceMessage);
            message.Properties.CopyProperties(sourceMessage.Properties);
            message.AddCompressionHeader();
            return message;
        }

        public static bool IsCompressed(this Message message)
        {
            return message.Headers.FindHeader("Compression", Namespace) > -1;
        }

        public static void AddCompressionHeader(this Message message)
        {
            message.Headers.Add(MessageHeader.CreateHeader("Compression", Namespace, "GZip"));
        }

        public static void RemoveCompressionHeader(this Message message)
        {
            message.Headers.RemoveAll("Compression", Namespace);
        }

        public static string CreateCompressedBody(byte[] content)
        {
            var output = new StringWriter();
            using (var writer2 = XmlWriter.Create(output))
            {
                writer2.WriteStartElement("CompressedBody", Namespace);
                writer2.WriteBase64(content, 0, content.Length);
                writer2.WriteEndElement();
            }
            return output.ToString();
        }

        public static byte[] GetCompressedBody(this Message message)
        {
            byte[] buffer;
            using (XmlReader reader1 = message.GetReaderAtBodyContents())
            {
                buffer = Convert.FromBase64String(reader1.ReadElementString("CompressedBody", Namespace));
            }
            return buffer;
        }

        private class Compress
        {
            public static byte[] Zip(byte[] buffer)
            {
                throw new NotImplementedException();
            }

            public static byte[] UnZip(byte[] getCompressedBody)
            {
                throw new NotImplementedException();
            }
        }
    }
}