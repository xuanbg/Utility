using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using Insight.Utils.Entity;

namespace Insight.Utils.Common
{
    public class HttpRequest
    {
        public Result Result = new Result();
        private string _Data;

        /// <summary>
        /// HttpRequest方法，用于客户端请求接口
        /// </summary>
        /// <param name="token">AccessToken</param>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法，默认GET</param>
        /// <param name="body">Body数据，默认NULL</param>
        /// <param name="compress">压缩方式(默认Gzip)</param>
        public HttpRequest(string token, string url, RequestMethod method = RequestMethod.GET, string body = null, CompressType compress = CompressType.none)
        {
            var request = GetWebRequest(url, method, token, compress);
            if (method != RequestMethod.GET)
            {
                var buffer = Encoding.UTF8.GetBytes(body ?? "");
                try
                {
                    var ms = new MemoryStream();
                    switch (compress)
                    {
                        case CompressType.gzip:
                            using (var stream = new GZipStream(ms, CompressionLevel.Optimal))
                            {
                                stream.Write(buffer, 0, buffer.Length);
                            }
                            buffer = ms.GetBuffer();
                            break;
                        case CompressType.deflate:
                            using (var stream = new DeflateStream(ms, CompressionLevel.Optimal))
                            {
                                stream.Write(buffer, 0, buffer.Length);
                            }
                            buffer = ms.GetBuffer();
                            break;
                        case CompressType.none:
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
                    Result.BadRequest(ex.Message);
                }
            }

            if (!GetResponse(request))
            {
                Result.BadRequest(_Data ?? "Response was not received data!");
                return;
            }

            Result = Util.Deserialize<Result>(_Data);
            if (Result != null) return;

            Result = new Result().BadRequest(_Data);
        }

        /// <summary>
        /// HttpRequest方法，用于客户端请求外部接口
        /// </summary>
        /// <param name="method">请求方法，默认GET</param>
        /// <param name="url">请求地址</param>
        /// <param name="body">Body数据</param>
        /// <param name="token">AccessToken</param>
        public HttpRequest(RequestMethod method, string url, string body = null, string token = null)
        {
            var request = GetWebRequest(url, method, token);
            if (method != RequestMethod.GET)
            {
                var buffer = Encoding.UTF8.GetBytes(body ?? "");
                try
                {
                    request.ContentLength = buffer.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
                catch (Exception ex)
                {
                    Result.BadRequest(ex.Message);
                }
            }

            if (GetResponse(request))
            {
                Result.Success(_Data);
                return;
            }

            Result.BadRequest(_Data ?? "Response was not received data!");
        }

        /// <summary>
        /// 获取WebRequest对象
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法</param>
        /// <param name="token">AccessToken</param>
        /// <param name="compress">压缩方式</param>
        /// <returns>HttpWebRequest</returns>
        private HttpWebRequest GetWebRequest(string url, RequestMethod method, string token = null, CompressType compress = CompressType.none)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method.ToString();
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
            request.Headers.Add(HttpRequestHeader.Authorization, token);
            if (method == RequestMethod.GET) return request;

            switch (compress)
            {
                case CompressType.gzip:
                    request.ContentType = "application/json; x-gzip";
                    request.Headers.Add(HttpRequestHeader.ContentEncoding, "gzip");
                    break;
                case CompressType.deflate:
                    request.ContentType = "application/json; x-deflate";
                    request.Headers.Add(HttpRequestHeader.ContentEncoding, "deflate");
                    break;
            }

            return request;
        }

        /// <summary>
        /// 获取Request响应数据
        /// </summary>
        /// <param name="request">WebRequest</param>
        /// <returns>Result</returns>
        private bool GetResponse(WebRequest request)
        {
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream == null) return false;

                var encoding = response.ContentEncoding.ToLower();
                switch (encoding)
                {
                    case "gzip":
                        _Data = FromGZipStream(stream);
                        break;
                    case "deflate":
                        _Data = FromDeflateStream(stream);
                        break;
                    default:
                        _Data = FromStream(stream);
                        break;
                }

                stream.Flush();
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                _Data = ex.Message;
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