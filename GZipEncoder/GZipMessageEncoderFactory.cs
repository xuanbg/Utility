using System;
using System.IO;
using System.IO.Compression;
using System.ServiceModel.Channels;

namespace Microsoft.Samples.GZipEncoder
{
    internal class GZipMessageEncoderFactory : MessageEncoderFactory
    {
        private readonly MessageEncoder _Encoder;

        /// <summary>
        /// 当前使用的消息编码器
        /// </summary>
        public override MessageEncoder Encoder => _Encoder;

        /// <summary>
        /// 编码器的消息版本
        /// </summary>
        public override MessageVersion MessageVersion => _Encoder.MessageVersion;

        /// <summary>
        /// 构造函数，根据传入的消息编码器类型创建一个Gzip消息编码器
        /// </summary>
        /// <param name="messageEncoderFactory"></param>
        public GZipMessageEncoderFactory(MessageEncoderFactory messageEncoderFactory)
        {
            if (messageEncoderFactory == null) throw new ArgumentNullException(nameof(messageEncoderFactory), "A valid message encoder factory must be passed to the GZipEncoder");

            _Encoder = new GZipMessageEncoder(messageEncoderFactory.Encoder);
        }

        /// <summary>
        /// GZip编码器
        /// </summary>
        private class GZipMessageEncoder : MessageEncoder
        {
            private const string GZipContentType = "application/json";
            private const string GZipContentEncoding = "gzip";
            private readonly MessageEncoder _InnerEncoder;

            /// <summary>
            /// 正文类型
            /// </summary>
            public override string ContentType => GZipContentType;

            /// <summary>
            /// 正文编码类型
            /// </summary>
            public override string MediaType => GZipContentEncoding;

            /// <summary>
            /// 编码器的消息版本
            /// </summary>
            public override MessageVersion MessageVersion => _InnerEncoder.MessageVersion;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="messageEncoder">消息编码器</param>
            internal GZipMessageEncoder(MessageEncoder messageEncoder)
            {
                if (messageEncoder == null) throw new ArgumentNullException(nameof(messageEncoder), "A valid message encoder must be passed to the GZipEncoder");

                _InnerEncoder = messageEncoder;
            }

            /// <summary>
            /// 由WCF调用以将缓冲的字节数组解码为消息
            /// </summary>
            /// <param name="buffer"></param>
            /// <param name="bufferManager"></param>
            /// <param name="contentType"></param>
            /// <returns>Message 解码后的消息</returns>
            public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
            {
                var decompressedBuffer = DecompressBuffer(buffer, bufferManager);
                var returnMessage = _InnerEncoder.ReadMessage(decompressedBuffer, bufferManager);
                returnMessage.Properties.Encoder = this;
                return returnMessage;
            }

            /// <summary>
            /// 由WCF调用将消息编码为缓冲字节数组
            /// </summary>
            /// <param name="message"></param>
            /// <param name="maxMessageSize"></param>
            /// <param name="bufferManager"></param>
            /// <param name="messageOffset"></param>
            /// <returns></returns>
            public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
            {
                var buffer = _InnerEncoder.WriteMessage(message, maxMessageSize, bufferManager, 0);
                return CompressBuffer(buffer, bufferManager, messageOffset);
            }

            /// <summary>
            /// 从流中读取消息
            /// </summary>
            /// <param name="stream">流</param>
            /// <param name="maxSizeOfHeaders"></param>
            /// <param name="contentType"></param>
            /// <returns>Message 流中读取到的消息</returns>
            public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
            {
                var gzStream = new GZipStream(stream, CompressionMode.Decompress, false);
                return _InnerEncoder.ReadMessage(gzStream, maxSizeOfHeaders);
            }

            /// <summary>
            /// 写入消息到流
            /// </summary>
            /// <param name="message">Message</param>
            /// <param name="stream"></param>
            public override void WriteMessage(Message message, Stream stream)
            {
                using (var gzStream = new GZipStream(stream, CompressionMode.Compress, true))
                {
                    _InnerEncoder.WriteMessage(message, gzStream);
                }

                stream.Flush();
            }

            /// <summary>
            /// 压缩字节数组
            /// </summary>
            /// <param name="buffer"></param>
            /// <param name="bufferManager"></param>
            /// <param name="messageOffset"></param>
            /// <returns></returns>
            private static ArraySegment<byte> CompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager, int messageOffset)
            {
                var memoryStream = new MemoryStream();

                using (var gzStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzStream.Write(buffer.Array, buffer.Offset, buffer.Count);
                }

                var compressedBytes = memoryStream.ToArray();
                var totalLength = messageOffset + compressedBytes.Length;
                var bufferedBytes = bufferManager.TakeBuffer(totalLength);

                Array.Copy(compressedBytes, 0, bufferedBytes, messageOffset, compressedBytes.Length);

                bufferManager.ReturnBuffer(buffer.Array);
                return new ArraySegment<byte>(bufferedBytes, messageOffset, bufferedBytes.Length - messageOffset);
            }

            /// <summary>
            /// 解压字节数组
            /// </summary>
            /// <param name="buffer"></param>
            /// <param name="bufferManager"></param>
            /// <returns></returns>
            private static ArraySegment<byte> DecompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager)
            {
                var memoryStream = new MemoryStream(buffer.Array, buffer.Offset, buffer.Count);
                var decompressedStream = new MemoryStream();
                var blockSize = 1024;
                var tempBuffer = bufferManager.TakeBuffer(blockSize);
                using (var gzStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    while (true)
                    {
                        var bytesRead = gzStream.Read(tempBuffer, 0, blockSize);
                        if (bytesRead == 0)
                            break;
                        decompressedStream.Write(tempBuffer, 0, bytesRead);
                    }
                }
                bufferManager.ReturnBuffer(tempBuffer);

                var decompressedBytes = decompressedStream.ToArray();
                var bufferManagerBuffer = bufferManager.TakeBuffer(decompressedBytes.Length + buffer.Offset);
                Array.Copy(buffer.Array, 0, bufferManagerBuffer, 0, buffer.Offset);
                Array.Copy(decompressedBytes, 0, bufferManagerBuffer, buffer.Offset, decompressedBytes.Length);

                var byteArray = new ArraySegment<byte>(bufferManagerBuffer, buffer.Offset, decompressedBytes.Length);
                bufferManager.ReturnBuffer(buffer.Array);

                return byteArray;
            }
        }
    }
}