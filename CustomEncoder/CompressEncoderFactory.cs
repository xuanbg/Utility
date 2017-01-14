using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.ServiceModel.Channels;

namespace Insight.WCF.CustomEncoder
{
    public class CompressEncoderFactory : MessageEncoderFactory
    {
        private readonly CompressEncoder _MessageEncoder;

        /// <summary>
        /// 消息编码器
        /// </summary>
        public override MessageEncoder Encoder => _MessageEncoder;

        /// <summary>
        /// 消息编码器的消息版本
        /// </summary>
        public override MessageVersion MessageVersion => InnerMessageEncodingBindingElement.MessageVersion;

        /// <summary>
        /// 消息编码器绑定元素
        /// </summary>
        public MessageEncodingBindingElement InnerMessageEncodingBindingElement { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="innerElement">消息编码器绑定元素</param>
        public CompressEncoderFactory(MessageEncodingBindingElement innerElement)
        {
            InnerMessageEncodingBindingElement = innerElement;
            _MessageEncoder = new CompressEncoder(this);
        }

        /// <summary>
        /// 自定义消息编码器
        /// </summary>
        private class CompressEncoder : MessageEncoder
        {
            private readonly MessageEncoder _InnerEncoder;

            /// <summary>
            /// 内容类型
            /// </summary>
            public override string ContentType => _InnerEncoder.ContentType;

            /// <summary>
            /// 媒体类型
            /// </summary>
            public override string MediaType => _InnerEncoder.MediaType;

            /// <summary>
            /// 编码器的消息版本
            /// </summary>
            public override MessageVersion MessageVersion => _InnerEncoder.MessageVersion;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="encoderFactory">编码器工厂</param>
            public CompressEncoder(CompressEncoderFactory encoderFactory)
            {
                _InnerEncoder = encoderFactory.InnerMessageEncodingBindingElement.CreateMessageEncoderFactory().Encoder;
            }

            public override bool IsContentTypeSupported(string contentType)
            {
                return _InnerEncoder.IsContentTypeSupported(contentType);
            }

            public override T GetProperty<T>()
            {
                return _InnerEncoder.GetProperty<T>();
            }

            public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
            {
                var decompressedBuffer = DecompressBuffer(buffer, bufferManager, contentType);
                var message = _InnerEncoder.ReadMessage(decompressedBuffer, bufferManager, contentType);
                message.Properties.Encoder = this;
                return message;
            }

            public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
            {
                var property = message.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
                var buffer = _InnerEncoder.WriteMessage(message, maxMessageSize, bufferManager, 0);
                return CompressBuffer(buffer, bufferManager, messageOffset, property?.Headers[HttpResponseHeader.ContentEncoding]);
            }

            public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
            {
                switch (contentType)
                {
                    case "application/json; x-gzip":
                        var gs = new GZipStream(stream, CompressionMode.Decompress, false);
                        return _InnerEncoder.ReadMessage(gs, maxSizeOfHeaders, "application/json");
                    case "application/json; x-deflate":
                        var ds = new DeflateStream(stream, CompressionMode.Decompress, false);
                        return _InnerEncoder.ReadMessage(ds, maxSizeOfHeaders, "application/json");
                    default:
                        return _InnerEncoder.ReadMessage(stream, maxSizeOfHeaders, contentType);
                }
            }

            public override void WriteMessage(Message message, Stream stream)
            {
                var property = message.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
                switch (property?.Headers[HttpResponseHeader.ContentEncoding])
                {
                    case "gzip":
                        using (var ms = new GZipStream(stream, CompressionLevel.Optimal, true))
                        {
                            _InnerEncoder.WriteMessage(message, ms);
                        }
                        break;
                    case "deflate":
                        using (var ms = new DeflateStream(stream, CompressionLevel.Optimal, true))
                        {
                            _InnerEncoder.WriteMessage(message, ms);
                        }
                        break;
                    default:
                        _InnerEncoder.WriteMessage(message, stream);
                        break;
                }
                stream.Flush();
            }

            /// <summary>
            /// 压缩字节数组
            /// </summary>
            /// <param name="buffer"></param>
            /// <param name="bufferManager"></param>
            /// <param name="messageOffset"></param>
            /// <param name="encoding"></param>
            /// <returns></returns>
            private ArraySegment<byte> CompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager, int messageOffset, string encoding)
            {
                var ms = new MemoryStream();
                switch (encoding)
                {
                    case "gzip":
                        using (var stream = new GZipStream(ms, CompressionMode.Compress, true))
                        {
                            stream.Write(buffer.Array, buffer.Offset, buffer.Count);
                        }
                        break;
                    case "deflate":
                        using (var stream = new DeflateStream(ms, CompressionLevel.Optimal, true))
                        {
                            stream.Write(buffer.Array, buffer.Offset, buffer.Count);
                        }
                        break;
                    default:
                        ms = new MemoryStream(buffer.Array);
                        break;
                }

                var bytes = ms.ToArray();
                var totalLength = messageOffset + bytes.Length;
                var bufferedBytes = bufferManager.TakeBuffer(totalLength);

                Array.Copy(bytes, 0, bufferedBytes, messageOffset, bytes.Length);

                bufferManager.ReturnBuffer(buffer.Array);
                return new ArraySegment<byte>(bufferedBytes, messageOffset, bufferedBytes.Length - messageOffset);
            }

            /// <summary>
            /// 解压字节数组
            /// </summary>
            /// <param name="buffer"></param>
            /// <param name="bufferManager"></param>
            /// <param name="contentType"></param>
            /// <returns></returns>
            private ArraySegment<byte> DecompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
            {
                var ms = new MemoryStream(buffer.Array, buffer.Offset, buffer.Count);
                var decompressedStream = new MemoryStream();
                const int blockSize = 1024;
                var tempBuffer = bufferManager.TakeBuffer(blockSize);
                switch (contentType)
                {
                    case "application/json; x-gzip":
                        using (var stream = new GZipStream(ms, CompressionMode.Decompress))
                        {
                            while (true)
                            {
                                var bytesRead = stream.Read(tempBuffer, 0, blockSize);
                                if (bytesRead == 0) break;

                                decompressedStream.Write(tempBuffer, 0, bytesRead);
                            }
                        }
                        break;
                    case "application/json; x-deflate":
                        using (var stream = new DeflateStream(ms, CompressionMode.Decompress))
                        {
                            while (true)
                            {
                                var bytesRead = stream.Read(tempBuffer, 0, blockSize);
                                if (bytesRead == 0) break;

                                decompressedStream.Write(tempBuffer, 0, bytesRead);
                            }
                        }
                        break;
                    default:
                        decompressedStream.Write(buffer.Array, 0, blockSize);
                        break;
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