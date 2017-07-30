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
        private HttpWebRequest _Request;
        private readonly string _Token;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType { private get; set; } = "application/json; charset=utf-8";

        /// <summary>
        /// 下行数据压缩方式(默认Gzip)
        /// </summary>
        public CompressType AcceptEncoding { private get; set; } = CompressType.Gzip;

        /// <summary>
        /// 上行数据压缩方式(默认无压缩)
        /// </summary>
        public CompressType ContentEncoding { private get; set; } = CompressType.None;

        /// <summary>
        /// 自定义请求头内容
        /// </summary>
        public Dictionary<HttpRequestHeader, string> Headers { private get; set; }

        /// <summary>
        /// HttpRequest方法，用于客户端请求接口
        /// </summary>
        /// <param name="token">AccessToken</param>
        public HttpRequest(string token = null)
        {
            _Token = token;
        }

        /// <summary>
        /// 发送Http请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="body">BODY数据</param>
        /// <param name="method">请求方法，默认GET</param>
        /// <returns>bool 是否成功</returns>
        public bool Send(string url, string body = null, RequestMethod method = RequestMethod.GET)
        {
            Create(url, method);
            if (method == RequestMethod.GET) return GetResponse();

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
        /// 新建并初始化一个Http请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法</param>
        private void Create(string url, RequestMethod method)
        {
            _Request = (HttpWebRequest)WebRequest.Create(url);
            _Request.Method = method.ToString();
            _Request.Accept = "application/json";
            _Request.ContentType = ContentType;
            if (_Token != null)
            {
                _Request.Headers.Add(HttpRequestHeader.Authorization, _Token);
            }

            if (AcceptEncoding != CompressType.None)
            {
                _Request.Headers.Add(HttpRequestHeader.AcceptEncoding, AcceptEncoding.ToString().ToLower());
            }

            if (ContentEncoding != CompressType.None)
            {
                _Request.Headers.Add(HttpRequestHeader.ContentEncoding, ContentEncoding.ToString().ToLower());
            }

            if (Headers == null) return;

            foreach (var header in Headers)
            {
                _Request.Headers[header.Key] = header.Value;
            }
        }

        /// <summary>
        /// 获取Request响应数据
        /// </summary>
        private bool GetResponse()
        {
            try
            {
                var response = (HttpWebResponse) _Request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    switch (response.ContentEncoding.ToLower())
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
                    // ReSharper disable once PossibleNullReferenceException
                    stream.Flush();
                }

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