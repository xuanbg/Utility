using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public class HttpRequest
    {
        private HttpWebRequest request;
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
        public CompressType acceptEncoding { private get; set; } = CompressType.Gzip;

        /// <summary>
        /// 上行数据压缩方式(默认无压缩)
        /// </summary>
        public CompressType contentEncoding { private get; set; } = CompressType.None;

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
        public bool Send(string url, RequestMethod method = RequestMethod.GET, object body = null)
        {
            Create(url, method);
            if (method == RequestMethod.GET) return GetResponse();

            var buffer = Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(body));
            try
            {
                var ms = new MemoryStream();
                switch (contentEncoding)
                {
                    case CompressType.Gzip:
                        using (var stream = new GZipStream(ms, CompressionMode.Compress))
                        {
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        buffer = ms.GetBuffer();
                        break;
                    case CompressType.Deflate:
                        using (var stream = new DeflateStream(ms, CompressionMode.Compress))
                        {
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        buffer = ms.GetBuffer();
                        break;
                    case CompressType.None:
                        break;
                }

                request.ContentLength = buffer.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }

            return GetResponse();
        }

        /// <summary>
        /// 新建并初始化一个Http请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法</param>
        private void Create(string url, RequestMethod method)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method.ToString();
            request.Accept = "application/json";
            request.ContentType = contentType;
            if (token != null)
            {
                request.Headers.Add(HttpRequestHeader.Authorization, token);
            }

            if (acceptEncoding != CompressType.None)
            {
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, acceptEncoding.ToString().ToLower());
            }

            if (contentEncoding != CompressType.None)
            {
                request.Headers.Add(HttpRequestHeader.ContentEncoding, contentEncoding.ToString().ToLower());
            }

            if (headers == null) return;

            foreach (var header in headers)
            {
                request.Headers[header.Key] = header.Value;
            }
        }

        /// <summary>
        /// 获取Request响应数据
        /// </summary>
        private bool GetResponse()
        {
            try
            {
                var response = (HttpWebResponse) request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    switch (response.ContentEncoding.ToLower())
                    {
                        case "gzip":
                            data = FromGZipStream(stream);
                            break;
                        case "deflate":
                            data = FromDeflateStream(stream);
                            break;
                        default:
                            data = FromStream(stream);
                            break;
                    }
                    // ReSharper disable once PossibleNullReferenceException
                    stream.Flush();
                }

                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 从DGZip压缩的流中获取数据
        /// </summary>
        /// <param name="response">GZip压缩的流</param>
        /// <returns>string 流中的数据</returns>
        private static string FromGZipStream(Stream response)
        {
            using (var stream = new GZipStream(response, CompressionMode.Decompress))
            {
                return FromStream(stream);
            }
        }

        /// <summary>
        /// 从Deflate压缩的流中获取数据
        /// </summary>
        /// <param name="response">Deflate压缩的流</param>
        /// <returns>string 流中的数据</returns>
        private static string FromDeflateStream(Stream response)
        {
            using (var stream = new DeflateStream(response, CompressionMode.Decompress))
            {
                return FromStream(stream);
            }
        }

        /// <summary>
        /// 从流中获取数据
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>string 流中的数据</returns>
        private static string FromStream(Stream stream)
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