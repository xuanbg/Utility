using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.ServiceModel.Channels;

namespace Insight.WCF.CustomEncoder
{
    public class CompressEncoder : MessageEncoder
    {
        private readonly MessageEncoder _InnerEncoder;
        private readonly CompressAlgorithm _Algorithm;

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
        /// <param name="algorithm">压缩方式</param>
        public CompressEncoder(CompressEncoderFactory encoderFactory, CompressAlgorithm algorithm)
        {
            _Algorithm = algorithm;
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
            var decompressedBuffer = DecompressBuffer(buffer, bufferManager);
            var message = _InnerEncoder.ReadMessage(decompressedBuffer, bufferManager, contentType);
            message.Properties.Encoder = this;
            return message;
        }

        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            var property = message.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
            var buffer = _InnerEncoder.WriteMessage(message, maxMessageSize, bufferManager, 0);
            return CompressBuffer(buffer, bufferManager, messageOffset, property.Headers[HttpResponseHeader.ContentEncoding]);
        }

        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
            switch (_Algorithm)
            {
                case CompressAlgorithm.GZip:
                    var gs = new GZipStream(stream, CompressionMode.Decompress, false);
                    return _InnerEncoder.ReadMessage(gs, maxSizeOfHeaders, contentType);
                case CompressAlgorithm.Deflate:
                    var ds = new DeflateStream(stream, CompressionMode.Decompress, false);
                    return _InnerEncoder.ReadMessage(ds, maxSizeOfHeaders, contentType);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void WriteMessage(Message message, Stream stream)
        {
            var property = message.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
            switch (property.Headers[HttpResponseHeader.ContentEncoding])
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
        /// <returns></returns>
        private ArraySegment<byte> DecompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager)
        {
            var ms = new MemoryStream(buffer.Array, buffer.Offset, buffer.Count);
            var decompressedStream = new MemoryStream();
            const int blockSize = 1024;
            var tempBuffer = bufferManager.TakeBuffer(blockSize);
            switch (_Algorithm)
            {
                case CompressAlgorithm.GZip:
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
                case CompressAlgorithm.Deflate:
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
                    throw new ArgumentOutOfRangeException();
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