using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public class HttpRequest
    {
        private readonly HttpWebRequest _Request;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// 请求方法，默认GET
        /// </summary>
        public RequestMethod Method { private get; set; } = RequestMethod.GET;

        /// <summary>
        /// 下行数据压缩方式(默认Gzip)
        /// </summary>
        public CompressType AcceptEncoding { private get; set; } = CompressType.Gzip;

        /// <summary>
        /// 上行数据压缩方式(默认无压缩)
        /// </summary>
        public CompressType ContentEncoding { private get; set; } = CompressType.None;

        /// <summary>
        /// HttpRequest方法，用于客户端请求接口
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="token">AccessToken</param>
        public HttpRequest(string url, string token)
        {
            _Request = (HttpWebRequest)WebRequest.Create(url);
            _Request.Method = Method.ToString();
            _Request.Accept = "application/json";
            _Request.ContentType = "application/json; charset=utf-8";
            if (token != null)
            {
                _Request.Headers.Add(HttpRequestHeader.Authorization, token);
            }

            if (AcceptEncoding != CompressType.None)
            {
                _Request.Headers.Add(HttpRequestHeader.AcceptEncoding, AcceptEncoding.ToString().ToLower());
            }

            if (ContentEncoding != CompressType.None)
            {
                _Request.Headers.Add(HttpRequestHeader.ContentEncoding, ContentEncoding.ToString().ToLower());
            }
        }

        /// <summary>
        /// HttpRequest方法，用于客户端请求外部接口
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="contentType"></param>
        /// <param name="headers">请求头</param>
        public HttpRequest(string url, string contentType = "application/json; charset=utf-8", Dictionary<HttpRequestHeader, string> headers = null)
        {
            _Request = (HttpWebRequest)WebRequest.Create(url);
            _Request.ContentType = contentType;
            if (headers == null) return;

            foreach (var header in headers)
            {
                _Request.Headers[header.Key] = header.Value;
            }
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="body">BODY数据</param>
        /// <returns>bool 是否成功</returns>
        public bool Request(string body = null)
        {
            if (Method == RequestMethod.GET) return GetResponse();

            var buffer = Encoding.UTF8.GetBytes(body ?? "");
            try
            {
                var ms = new MemoryStream();
                switch (ContentEncoding)
                {
                    case CompressType.Gzip:
                        using (var stream = new GZipStream(ms, CompressionLevel.Optimal))
                        {
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        buffer = ms.GetBuffer();
                        break;
                    case CompressType.Deflate:
                        using (var stream = new DeflateStream(ms, CompressionLevel.Optimal))
                        {
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        buffer = ms.GetBuffer();
                        break;
                    case CompressType.None:
                        break;
                }

                _Request.ContentLength = buffer.Length;
                using (var stream = _Request.GetRequestStream())
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return false;
            }

            return GetResponse();
        }

        /// <summary>
        /// 获取Request响应数据
        /// </summary>
        private bool GetResponse()
        {
            try
            {
                var response = (HttpWebResponse) _Request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream == null) return false;

                var encoding = response.ContentEncoding.ToLower();
                switch (encoding)
                {
                    case "gzip":
                        Data = FromGZipStream(stream);
                        break;
                    case "deflate":
                        Data = FromDeflateStream(stream);
                        break;
                    default:
                        Data = FromStream(stream);
                        break;
                }

                stream.Flush();
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 从DGZip压缩的流中获取数据
        /// </summary>
        /// <param name="response">GZip压缩的流</param>
        /// <returns>string 流中的数据</returns>
        private string FromGZipStream(Stream response)
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
        private string FromDeflateStream(Stream response)
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
        private string FromStream(Stream stream)
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