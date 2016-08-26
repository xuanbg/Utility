using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web.Script.Serialization;
using Insight.Utils.Entity;

namespace Insight.Utils
{
    public static class Util
    {

        /// <summary>
        /// 接口调用时间记录
        /// </summary>
        private static readonly Dictionary<string, DateTime> Requests = new Dictionary<string, DateTime>();

        #region 常用方法

        /// <summary>
        /// 读取配置项的值
        /// </summary>
        /// <param name="key">配置项</param>
        /// <returns>配置项的值</returns>
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 计算字符串的Hash值
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>String Hash值</returns>
        public static string Hash(string str)
        {
            var md5 = MD5.Create();
            var s = md5.ComputeHash(Encoding.UTF8.GetBytes(str.Trim()));
            return s.Aggregate("", (current, c) => current + c.ToString("X2"));
        }

        /// <summary>
        /// 将对象转换为Base64编码的字符串
        /// </summary>
        /// <param name="obj">用于转换的数据对象</param>
        /// <returns>string Base64编码的字符串</returns>
        public static string Base64(object obj)
        {
            var json = Serialize(obj);
            var buff = Encoding.UTF8.GetBytes(json);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// 忽略大小写情况下比较两个字符串
        /// </summary>
        /// <param name="s1">字符串1</param>
        /// <param name="s2">字符串2</param>
        /// <returns>bool 是否相同</returns>
        public static bool StringCompare(string s1, string s2)
        {
            return string.Equals(s1, s2, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 将Result对象复制为其派生类
        /// </summary>
        /// <typeparam name="T">Result的派生类型</typeparam>
        /// <param name="input">输入的Result对象</param>
        /// <returns>T Result的派生类</returns>
        public static T Copy<T>(Result input) where T:Result
        {
            var str = Serialize(input);
            return Deserialize<T>(str);
        }

        /// <summary>
        /// 根据传入的时长返回当前调用的剩余限制时间（秒）
        /// </summary>
        /// <param name="seconds">限制访问时长（秒）</param>
        /// <returns>int 剩余限制时间（秒）</returns>
        public static int LimitCall(int seconds)
        {
            if (seconds <= 0) return 0;

            var properties = OperationContext.Current.IncomingMessageProperties;
            var endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpoint == null) return 0;

            var ip = endpoint.Address;
            if (!Requests.ContainsKey(ip))
            {
                Requests.Add(ip, DateTime.Now);
                return 0;
            }

            var span = Requests[ip].AddSeconds(seconds) - DateTime.Now;
            var surplus = (int)Math.Floor(span.TotalSeconds);
            if (seconds - surplus > 0 && seconds - surplus < 3)
            {
                Requests[ip] = DateTime.Now;
                return seconds;
            }

            if (surplus > 0) return surplus;

            Requests[ip] = DateTime.Now;
            return 0;
        }

        #endregion

        #region Serialize/Deserialize

        /// <summary>
        /// 将一个对象序列化为Json字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>string Json字符串</returns>
        public static string Serialize<T>(T obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        /// <summary>
        /// 将一个Json字符串反序列化为指定类型的对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns>T 反序列化的对象</returns>
        public static T Deserialize<T>(string json)
        {
            return new JavaScriptSerializer().Deserialize<T>(json);
        }

        #endregion

        #region Compress/Decompress

        /// <summary>
        /// GZip压缩
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            var ms = new MemoryStream();
            var stream = new GZipStream(ms, CompressionMode.Compress, true);
            stream.Write(data, 0, data.Length);
            stream.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// ZIP解压
        /// </summary>
        /// <param name="dada"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] dada)
        {
            var ms = new MemoryStream(dada);
            var stream = new GZipStream(ms, CompressionMode.Decompress);
            var buffer = new MemoryStream();
            var block = new byte[1024];
            while (true)
            {
                var read = stream.Read(block, 0, block.Length);
                if (read <= 0) break;
                buffer.Write(block, 0, read);
            }
            stream.Close();
            return buffer.ToArray();
        }

        #endregion

    }
}
