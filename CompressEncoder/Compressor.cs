using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Insight.WCF.CompressEncoder
{
    public class Compressor
    {
        private readonly CompressAlgorithm _Algorithm;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">压缩方式</param>
        public Compressor(CompressAlgorithm algorithm)
        {
            _Algorithm = algorithm;
        }

        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="data">源数据</param>
        /// <returns>压缩后的字节数组</returns>
        public ArraySegment<byte> Compress(ArraySegment<byte> data)
        {
            using (var ms = new MemoryStream())
            {
                switch (_Algorithm)
                {
                    case CompressAlgorithm.GZip:
                        using (var stream = new GZipStream(ms, CompressionLevel.Optimal, true))
                        {
                            stream.Write(data.Array, 0, data.Count);
                        }
                        break;
                    case CompressAlgorithm.Deflate:
                        using (var stream = new DeflateStream(ms, CompressionLevel.Optimal, true))
                        {
                            stream.Write(data.Array, 0, data.Count);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return new ArraySegment<byte>(ms.GetBuffer());
            }
        }

        /// <summary>
        /// 压缩流
        /// </summary>
        /// <param name="input">输入流</param>
        /// <returns>Stream</returns>
        public Stream Compress(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                var buffer = new byte[input.Length];
                input.Read(buffer, 0, buffer.Length);
                switch (_Algorithm)
                {
                    case CompressAlgorithm.GZip:
                        using (var stream = new GZipStream(ms, CompressionLevel.Optimal, true))
                        {
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        break;
                    case CompressAlgorithm.Deflate:
                        using (var stream = new DeflateStream(ms, CompressionLevel.Optimal, true))
                        {
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return ms;
            }
        }

        /// <summary>
        /// 解压缩数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ArraySegment<byte> DeCompress(ArraySegment<byte> data)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(data.Array, 0, data.Count);
                ms.Seek(0, SeekOrigin.Begin);
                switch (_Algorithm)
                {
                    case CompressAlgorithm.GZip:
                        using (var stream = new GZipStream(ms, CompressionMode.Decompress, false))
                        {
                            var buffer = RetrieveBytesFromStream(stream);
                            return new ArraySegment<byte>(buffer);
                        }
                    case CompressAlgorithm.Deflate:
                        using (var stream = new DeflateStream(ms, CompressionMode.Decompress, false))
                        {
                            var buffer = RetrieveBytesFromStream(stream);
                            return new ArraySegment<byte>(buffer);
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// 解压缩流
        /// </summary>
        /// <param name="input">输入流</param>
        /// <returns>Stream</returns>
        public Stream DeCompress(Stream input)
        {
            input.Seek(0, SeekOrigin.Begin);
            switch (_Algorithm)
            {
                case CompressAlgorithm.GZip:
                    using (var stream = new GZipStream(input, CompressionMode.Decompress, false))
                    {
                        var buffer = RetrieveBytesFromStream(stream);
                        return new MemoryStream(buffer);
                    }
                case CompressAlgorithm.Deflate:
                    using (var stream = new DeflateStream(input, CompressionMode.Decompress, false))
                    {
                        var buffer = RetrieveBytesFromStream(stream);
                        return new MemoryStream(buffer);
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 从流中读取字节数组
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>byte[] 字节数组</returns>
        public static byte[] RetrieveBytesFromStream(Stream stream)
        {
            var list = new List<byte>();
            var data = new byte[1024];
            while (true)
            {
                var bytesRead = stream.Read(data, 0, data.Length);
                if (bytesRead == 0) break;

                var buffers = new byte[bytesRead];
                Array.Copy(data, buffers, bytesRead);
                list.AddRange(buffers);
            }

            return list.ToArray();
        }
    }
}