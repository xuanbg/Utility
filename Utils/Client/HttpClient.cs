using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.Client
{
    public class HttpClient
    {
        private string _AccessToken;
        private readonly TokenHelper _Token;
        private readonly DateTime _Time = DateTime.Now;

        /// <summary>
        /// 是否记录接口调用日志
        /// </summary>
        public bool Logging { get; set; } = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HttpClient()
        {
            Logging = false;
        }

        /// <summary>
        /// 构造函数，传入AccessToken
        /// </summary>
        /// <param name="token">AccessToken</param>
        public HttpClient(string token)
        {
            _AccessToken = token;
        }

        /// <summary>
        /// 构造函数，传入TokenHelper
        /// </summary>
        /// <param name="token">TokenHelper</param>
        public HttpClient(TokenHelper token)
        {
            _Token = token;
            _AccessToken = token.AccessToken;
        }

        /// <summary>
        /// HttpRequest:GET方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Get<T>(string url, string message = null)
        {
            var result = Request(url);
            if (result.successful) return Util.Deserialize<T>(result.data);

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            if (result.code != "406") Messages.ShowError(msg);

            if (result.code == "204") Messages.ShowMessage(result.message);

            return default(T);
        }

        /// <summary>
        /// HttpRequest:POST方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="data">POST的数据</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Post<T>(string url, object data, string message = null)
        {
            var result = Request(url, "POST", data);
            if (result.successful) return Util.Deserialize<T>(result.data);

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            if (result.code != "406") Messages.ShowError(msg);

            if (result.code == "204") Messages.ShowMessage(result.message);

            return default(T);
        }

        /// <summary>
        /// HttpRequest:PUT方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="data">PUT的数据</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Put<T>(string url, object data, string message = null)
        {
            var result = Request(url, "PUT", data);
            if (result.successful) return Util.Deserialize<T>(result.data);

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            if (result.code != "406") Messages.ShowError(msg);

            if (result.code == "204") Messages.ShowMessage(result.message);

            return default(T);
        }

        /// <summary>
        /// HttpRequest:DELETE方法
        /// </summary>
        /// <typeparam name="T">返回值数据类型</typeparam>
        /// <param name="url">接口URL</param>
        /// <param name="data">DELETE的数据，默认NULL</param>
        /// <param name="message">错误消息，默认NULL</param>
        /// <returns>T 指定类型的数据</returns>
        public T Delete<T>(string url, object data = null, string message = null)
        {
            var result = Request(url, "DELETE", data);
            if (result.successful) return Util.Deserialize<T>(result.data);

            var newline = string.IsNullOrEmpty(message) ? "" : "\r\n";
            var msg = $"{result.message}{newline}{message}";
            if (result.code != "406") Messages.ShowError(msg);

            if (result.code == "204") Messages.ShowMessage(result.message);

            return default(T);
        }

        /// <summary>
        /// HttpRequest方法，用于客户端请求接口
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法，默认GET</param>
        /// <param name="data">请求数据，默认NULL</param>
        /// <param name="compress">压缩方式(默认Gzip)</param>
        /// <returns>Result</returns>
        public Result Request(string url, string method = "GET", object data = null, CompressType compress = CompressType.None)
        {
            var result = new Result();
            var request = GetWebRequest(method, url, _AccessToken, compress);
            if (method != "GET")
            {
                var body = new JavaScriptSerializer().Serialize(data ?? "");
                var buffer = Encoding.UTF8.GetBytes(body);
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
                    result.BadRequest(ex.Message);
                    return result;
                }
            }

            result = GetResponse(request);
            if (_Token == null || result.code != "406") return result;

            // AccessToken失效时自动更新AccessToken，并重新调用接口
            _Token.GetTokens();
            _AccessToken = _Token.AccessToken;
            return Request(url, method, data);
        }

        /// <summary>
        /// 获取WebRequest对象
        /// </summary>
        /// <param name="method">请求方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="token">AccessToken</param>
        /// <param name="compress">压缩方式</param>
        /// <returns>HttpWebRequest</returns>
        private HttpWebRequest GetWebRequest(string method, string url, string token, CompressType compress)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = method;
            request.Accept = "application/json";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
            switch (compress)
            {
                case CompressType.Gzip:
                    request.ContentType = "application/json; x-gzip";
                    break;
                case CompressType.Deflate:
                    request.ContentType = "application/json; x-deflate";
                    break;
                default:
                    request.ContentType = "application/json";
                    break;
            }

            if (string.IsNullOrEmpty(token)) return request;

            request.Headers.Add(HttpRequestHeader.Authorization, token);
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
                var response = (HttpWebResponse) request.GetResponse();
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
#if DEBUG
            // 在DEBUG模式下且AccessToken有效时记录接口调用日志
            if (Logging && result.code != "406") LogAsync(_AccessToken, request.Method, request.RequestUri.AbsolutePath, result.message);
#endif
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

        /// <summary>
        /// 记录接口调用日志
        /// </summary>
        /// <param name="token">AccessToken</param>
        /// <param name="method">请求方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="message">接口返回消息</param>
        private void Log(string token, string method, string url, string message)
        {
            if (string.IsNullOrEmpty(token)) return;

            var ts = DateTime.Now - _Time;
            var loginfo = new LogInfo
            {
                Interface = $"{Util.GetAppSetting("BaseServer")}/logapi/v1.0/logs",
                Token = token,
                Code = "700101",
                Source = "系统平台",
                Action = "接口调用",
                Message = $"[{method}]{url};{message};用时{ts.TotalMilliseconds}毫秒"
            };
            var log = new LogClient(loginfo);
            log.LogToServer();
        }

        /// <summary>
        /// 异步记录接口调用日志
        /// </summary>
        /// <param name="token">AccessToken</param>
        /// <param name="method">请求方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="message">接口返回消息</param>
        private void LogAsync(string token, string method, string url, string message)
        {
            Task.Run(() => Log(token, method, url, message));
        }
    }
}