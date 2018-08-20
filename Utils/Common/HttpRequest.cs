using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public class HttpRequest
    {
        private readonly string token;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string message { get; private set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public string data { get; private set; }

        /// <summary>
        /// ContentType
        /// </summary>
        public string contentType { private get; set; } = "application/json; charset=utf-8";

        /// <summary>
        /// 下行数据压缩方式(默认Gzip)
        /// </summary>
        public CompressType acceptEncoding { private get; set; } = CompressType.GZIP;

        /// <summary>
        /// 上行数据压缩方式(默认无压缩)
        /// </summary>
        public CompressType contentEncoding { private get; set; } = CompressType.NONE;

        /// <summary>
        /// 自定义请求头内容
        /// </summary>
        public Dictionary<HttpRequestHeader, string> headers { private get; set; }

        /// <summary>
        /// HttpRequest方法，用于客户端请求接口
        /// </summary>
        /// <param name="token">AccessToken</param>
        public HttpRequest(string token = null)
        {
            this.token = token;
        }

        /// <summary>
        /// 发送Http请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法，默认GET</param>
        /// <param name="body">BODY数据</param>
        /// <returns>bool 是否成功</returns>
        public bool send(string url, RequestMethod method, Dictionary<string, object> body)
        {
            if (method != RequestMethod.GET)
            {
                var json = body == null ? "" : new JavaScriptSerializer().Serialize(body);
                return send(url, method, json);
            }

            if (body != null)
            {
                var param = body.Aggregate("?", (c, p) => c + $"&{p.Key}={p.Value}");
                url += param.Replace("?&", "?");
            }

            return send(url);
        }

        /// <summary>
        /// 发送Http请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法，默认GET</param>
        /// <param name="body">BODY数据</param>
        /// <returns>bool 是否成功</returns>
        public bool send(string url, RequestMethod method = RequestMethod.GET, string body = null)
        {
            // 初始化请求对象及默认请求头
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method.ToString();
            request.Accept = "application/json";
            request.ContentType = contentType;
            if (token != null)
            {
                request.Headers.Add(HttpRequestHeader.Authorization, token);
            }

            if (acceptEncoding != CompressType.NONE)
            {
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, acceptEncoding.ToString().ToLower());
            }

            if (contentEncoding != CompressType.NONE)
            {
                request.Headers.Add(HttpRequestHeader.ContentEncoding, contentEncoding.ToString().ToLower());
            }

            // 覆写指定的请求头
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers[header.Key] = header.Value;
                }
            }

            // 上传数据
            if (method != RequestMethod.GET)
            {
                var buffer = encodingBody(body ?? "");
                request.ContentLength = buffer.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }

            // 读取返回数据
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    switch (response.ContentEncoding.ToLower())
                    {
                        case "gzip":
                            data = fromGZipStream(stream);
                            break;
                        case "deflate":
                            data = fromDeflateStream(stream);
                            break;
                        default:
                            data = fromStream(stream);
                            break;
                    }

                    stream?.Flush();
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 将请求体数据编码为字节数组
        /// </summary>
        /// <param name="body">请求体数据</param>
        /// <returns>byte[]</returns>
        private byte[] encodingBody(string body)
        {
            var buffer = Encoding.UTF8.GetBytes(body);
            var ms = new MemoryStream();
            switch (contentEncoding)
            {
                case CompressType.GZIP:
                    using (var stream = new GZipStream(ms, CompressionMode.Compress))
                    {
                        stream.Write(buffer, 0, buffer.Length);
                        return ms.GetBuffer();
                    }

                case CompressType.DEFLATE:
                    using (var stream = new DeflateStream(ms, CompressionMode.Compress))
                    {
                        stream.Write(buffer, 0, buffer.Length);
                        return ms.GetBuffer();
                    }

                default:
                    return buffer;
            }
        }

        /// <summary>
        /// 从DGZip压缩的流中获取数据
        /// </summary>
        /// <param name="response">GZip压缩的流</param>
        /// <returns>string 流中的数据</returns>
        private static string fromGZipStream(Stream response)
        {
            using (var stream = new GZipStream(response, CompressionMode.Decompress))
            {
                return fromStream(stream);
            }
        }

        /// <summary>
        /// 从Deflate压缩的流中获取数据
        /// </summary>
        /// <param name="response">Deflate压缩的流</param>
        /// <returns>string 流中的数据</returns>
        private static string fromDeflateStream(Stream response)
        {
            using (var stream = new DeflateStream(response, CompressionMode.Decompress))
            {
                return fromStream(stream);
            }
        }

        /// <summary>
        /// 从流中获取数据
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>string 流中的数据</returns>
        private static string fromStream(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }

    /// <summary>
    /// Http请求方法
    /// </summary>
    public enum RequestMethod
    {
        GET,
        POST,
        PUT,
        PATCH,
        DELETE,
        OPTIONS
    }
}