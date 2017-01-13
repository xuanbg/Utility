using System.IO;
using System.IO.Compression;

namespace Insight.WCF.Compression
{
    public class DataCompressor
    {
        /// <summary>
        /// 压缩数据
        /// </summary>
        /// <param name="decompressedData">字节流数据</param>
        /// <param name="algorithm">压缩类型</param>
        /// <returns>byte[] 压缩后的字节流数据</returns>
        public static byte[] Compress(byte[] decompressedData, CompressionAlgorithm algorithm)
        {
            using (var sourceStream = new MemoryStream())
            {
                switch (algorithm)
                {
                    case CompressionAlgorithm.GZip:
                        using (var stream = new GZipStream(sourceStream, CompressionLevel.Optimal))
                        {
                            stream.Write(decompressedData, 0, decompressedData.Length);
                        }
                        break;
                    case CompressionAlgorithm.Deflate:
                        using (var stream = new DeflateStream(sourceStream, CompressionLevel.Optimal))
                        {
                            stream.Write(decompressedData, 0, decompressedData.Length);
                        }
                        break;
                    case CompressionAlgorithm.None:
                        break;
                }
                return sourceStream.ToArray();
            }
        }

        /// <summary>
        /// 解压数据
        /// </summary>
        /// <param name="compressedData">字节流数据</param>
        /// <param name="algorithm">压缩类型</param>
        /// <returns>byte[] 解压后的字节流数据</returns>
        public static byte[] Decompress(byte[] compressedData, CompressionAlgorithm algorithm)
        {
            using (var sourceStream = new MemoryStream(compressedData))
            {
                switch (algorithm)
                {
                    case CompressionAlgorithm.GZip:
                        using (var stream = new GZipStream(sourceStream, CompressionMode.Decompress))
                        {
                            return LoadToBuffer(stream);
                        }
                    case CompressionAlgorithm.Deflate:
                        using (var stream = new DeflateStream(sourceStream, CompressionMode.Decompress))
                        {
                            return LoadToBuffer(stream);
                        }
                    default:
                        return LoadToBuffer(sourceStream);
                }
            }
        }

        /// <summary>
        /// 从流中读取字节流
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <returns>byte[] 字节流数据</returns>
        private static byte[] LoadToBuffer(Stream stream)
        {
            using (var stream2 = new MemoryStream())
            {
                int num;
                var buffer = new byte[0x400];
                while ((num = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream2.Write(buffer, 0, num);
                }

                return stream2.ToArray();
            }
        }
    }
}