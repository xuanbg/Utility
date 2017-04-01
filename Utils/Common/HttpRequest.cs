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

        public Result Result;

        /// <summary>
        /// HttpRequest方法，用于客户端请求接口
        /// </summary>
        /// <param name="token">AccessToken</param>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法，默认GET</param>
        /// <param name="body">Body数据，默认NULL</param>
        /// <param name="compress">压缩方式(默认Gzip)</param>
        public HttpRequest(string token, string url, string method = "GET", string body = null, CompressType compress = CompressType.None)
        {
            var request = GetWebRequest(token, method, url, compress);
            if (method != "GET")
            {
                var buffer = Encoding.UTF8.GetBytes(body ?? "");
                try
                {
                    var ms = new MemoryStream();
                    switch (compress)
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

            Result = GetResponse(request);
        }

        /// <summary>
        /// 获取WebRequest对象
        /// </summary>
        /// <param name="token">AccessToken</param>
        /// <param name="method">请求方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="compress">压缩方式</param>
        /// <returns>HttpWebRequest</returns>
        private HttpWebRequest GetWebRequest(string token, string method, string url, CompressType compress)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
            request.Headers.Add(HttpRequestHeader.Authorization, token);
            if (method == "GET") return request;

            switch (compress)
            {
                case CompressType.Gzip:
                    request.ContentType = "application/json; x-gzip";
                    request.Headers.Add(HttpRequestHeader.ContentEncoding, "gzip");
                    break;
                case CompressType.Deflate:
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
        private Result GetResponse(WebRequest request)
        {
            var result = new Result();
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    result.BadRequest("Response was not received data!");
                    return result;
                }

                string data;
                var encoding = response.ContentEncoding.ToLower();
                switch (encoding)
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

                result = Util.Deserialize<Result>(data);
                stream.Flush();
                stream.Close();
            }
            catch (Exception ex)
            {
                result.BadRequest(ex.Message);
                return result;
            }
            return result;
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
}