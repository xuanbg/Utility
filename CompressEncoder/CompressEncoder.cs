using System;
using System.IO;
using System.ServiceModel.Channels;

namespace Insight.WCF.CompressEncoder
{
    public class CompressEncoder : MessageEncoder
    {
        readonly MessageEncoder _InnserEncoder;
        private readonly CompressAlgorithm _Algorithm;

        /// <summary>
        /// 内容类型
        /// </summary>
        public override string ContentType => _InnserEncoder.ContentType;

        /// <summary>
        /// 媒体类型
        /// </summary>
        public override string MediaType => _InnserEncoder.MediaType;

        /// <summary>
        /// 编码器的消息版本
        /// </summary>
        public override MessageVersion MessageVersion => _InnserEncoder.MessageVersion;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="encoderFactory">编码器工厂</param>
        /// <param name="algorithm">压缩方式</param>
        public CompressEncoder(CompressEncoderFactory encoderFactory, CompressAlgorithm algorithm)
        {
            _Algorithm = algorithm;
            _InnserEncoder = encoderFactory.InnerMessageEncodingBindingElement.CreateMessageEncoderFactory().Encoder;
        }

        public override bool IsContentTypeSupported(string contentType)
        {
            return _InnserEncoder.IsContentTypeSupported(contentType);
        }

        public override T GetProperty<T>()
        {
            return _InnserEncoder.GetProperty<T>();
        }

        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            var bytes = new Compressor(_Algorithm).DeCompress(buffer);
            var totalLength = bytes.Count;
            var totalBytes = bufferManager.TakeBuffer(totalLength);
            Array.Copy(bytes.Array, 0, totalBytes, 0, bytes.Count);
            var byteArray = new ArraySegment<byte>(totalBytes, 0, bytes.Count);
            bufferManager.ReturnBuffer(byteArray.Array);
            return _InnserEncoder.ReadMessage(byteArray, bufferManager, contentType);
        }

        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            var bytes = _InnserEncoder.WriteMessage(message, maxMessageSize, bufferManager);
            var buffer = new Compressor(_Algorithm).Compress(bytes);
            var totalLength = buffer.Count + messageOffset;
            var totalBytes = bufferManager.TakeBuffer(totalLength);
            Array.Copy(buffer.Array, 0, totalBytes, messageOffset, buffer.Count);
            return new ArraySegment<byte>(totalBytes, messageOffset, buffer.Count);
        }

        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
            var ms = new Compressor(_Algorithm).DeCompress(stream);
            return _InnserEncoder.ReadMessage(ms, maxSizeOfHeaders, contentType);
        }

        public override void WriteMessage(Message message, Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                _InnserEncoder.WriteMessage(message, ms);
                stream = new Compressor(_Algorithm).Compress(ms);
            }
        }
    }
}
