using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Insight.Utils.Entity;
using Newtonsoft.Json;

namespace Insight.Utils.Common
{
    public static class Util
    {
        #region 常用方法

        /// <summary>
        /// 时间戳转DateTime
        /// </summary>
        /// <param name="ts">时间戳</param>
        /// <returns>DateTime</returns>
        public static DateTime getDateTime(long ts)
        {
            var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var toNow = new TimeSpan(ts * 10000000);

            return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime转时间戳
        /// </summary>
        /// <param name="time">DateTime</param>
        /// <returns>时间戳</returns>
        public static long getTimeStamp(DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            return Convert.ToInt64((time - startTime).TotalSeconds);
        }

        /// <summary>
        /// 生成ID
        /// </summary>
        /// <param name="format">输出格式(N:无分隔符;默认D:有分隔符)</param>
        /// <returns>ID</returns>
        public static string newId(string format = "D")
        {
            return Guid.NewGuid().ToString(format);
        }

        /// <summary>
        /// 读取配置项的值
        /// </summary>
        /// <param name="key">配置项</param>
        /// <returns>配置项的值</returns>
        public static string getAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 保存配置项的值
        /// </summary>
        /// <param name="key">配置项</param>
        /// <param name="value">配置项的值</param>
        public static void saveAppSetting(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings[key].Value = value;

            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 计算字符串的Hash值
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>String Hash值</returns>
        public static string hash(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;

            var md5 = MD5.Create();
            var s = md5.ComputeHash(Encoding.UTF8.GetBytes(str.Trim()));
            return s.Aggregate("", (current, c) => current + c.ToString("x2"));
        }

        /// <summary>
        /// 金额大写转换
        /// </summary>
        /// <param name="amount">金额</param>
        /// <param name="type">补整类型:0到元补整；1到角补整</param>
        /// <returns>string 中文大写金额</returns>
        public static string amountConvertToCn(decimal amount, int type = 1)
        {
            if (amount >= 1000000000000) return "金额不能支持万亿及更高";

            if (amount == 0) return "零元整";

            const string digital = "零壹贰叁肆伍陆柒捌玖";
            const string position = "仟佰拾亿仟佰拾万仟佰拾元角分";
            var zeroCount = 0;
            var isNegative = amount < 0;
            var amountCn = isNegative ? "(负)" : "";
            var value = (Math.Abs(amount) * 100).ToString("####");
            var length = value.Length;
            var pos = position.Substring(14 - length);
            for (var i = 0; i < length; i++)
            {
                var val = Convert.ToInt32(value.Substring(i, 1));
                var digVal = digital.Substring(val, 1);
                var posVal = pos.Substring(i, 1);
                if (val > 0)
                {
                    if (zeroCount > 0 && (length - i + 2) % 4 > 0) amountCn += "零";

                    zeroCount = 0;
                }
                else
                {
                    digVal = "";
                    if ((length - i + 1) % 4 > 0 || zeroCount > 2 && i == length - 7) posVal = "";

                    if (zeroCount + type > 0 && i == length - 1) posVal = "整";

                    zeroCount++;
                }

                amountCn = amountCn + digVal + posVal;
            }

            return amountCn;
        }

        /// <summary>
        /// 将对象序列化为Json后再进行Base64编码
        /// </summary>
        /// <param name="obj">用于转换的数据对象</param>
        /// <returns>string Base64编码的字符串</returns>
        public static string base64(object obj)
        {
            return obj == null ? null : base64Encode(serialize(obj));
        }

        /// <summary>
        /// 将字符串进行Base64编码
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>string Base64编码的字符串</returns>
        public static string base64Encode(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;

            var buff = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// 将字符串进行Base64解码
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>string Base64编码的字符串</returns>
        public static string base64Decode(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;

            try
            {
                var buff = Convert.FromBase64String(str);
                return Encoding.UTF8.GetString(buff);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 16进制字符串转字节数组
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>byte[]</returns>
        public static byte[] hexToByteArray(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;

            var hex = str.Replace("0x", "");
            var bytes = new byte[hex.Length / 2];
            for (var x = 0; x < bytes.Length; x++)
            {
                bytes[x] = Convert.ToByte(hex.Substring(x * 2, 2), 16);
            }

            return bytes;
        }

        /// <summary>
        /// 忽略大小写情况下比较两个字符串
        /// </summary>
        /// <param name="s1">字符串1</param>
        /// <param name="s2">字符串2</param>
        /// <returns>bool 是否相同</returns>
        public static bool stringCompare(string s1, string s2)
        {
            return string.Equals(s1, s2, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 复制源对象的属性值到目标对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        public static void copyValue<T>(T source, T target)
        {
            var propertys = typeof(T).GetProperties();
            foreach (var property in propertys)
            {
                if (!property.CanRead || !property.CanWrite) continue;

                var value = property.GetValue(source, null);
                property.SetValue(target, value, null);
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">源对象</param>
        /// <returns>T 复制的对象</returns>
        public static T clone<T>(T obj)
        {
            if (obj == null) return default(T);

            var str = serialize(obj);
            return deserialize<T>(str);
        }

        /// <summary>
        /// 将任意对象转换为指定的类型，请保证对象能够相互转换为目标类型！
        /// </summary>
        /// <typeparam name="T">转换目标类型</typeparam>
        /// <param name="obj">任意对象</param>
        /// <returns>T 转换后的类型</returns>
        public static T convertTo<T>(object obj)
        {
            if (obj == null) return default(T);

            var str = serialize(obj);
            return deserialize<T>(str);
        }

        /// <summary>
        /// 将List转为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable convertToDataTable<T>(List<T> list)
        {
            if (list == null) return null;

            var table = new DataTable();
            var propertys = typeof(T).GetProperties().ToList();
            propertys.ForEach(p => table.Columns.Add(getPropertyName(p), p.PropertyType));

            foreach (var item in list)
            {
                var row = table.NewRow();
                propertys.ForEach(p => row[getPropertyName(p)] = p.GetValue(item, null));
                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// 将DataTable转为List
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>List</returns>
        public static List<T> convertToList<T>(DataTable table) where T : new()
        {
            if (table == null) return null;

            var list = new List<T>();
            var propertys = typeof(T).GetProperties();
            foreach (DataRow row in table.Rows)
            {
                var obj = new T();
                foreach (var p in propertys)
                {
                    var name = getPropertyName(p);
                    if (!p.CanWrite || !table.Columns.Contains(name)) continue;

                    var value = row[name];
                    if (value == DBNull.Value) continue;

                    p.SetValue(obj, value, null);
                }
                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 获取属性别名或名称
        /// </summary>
        /// <param name="info">PropertyInfo</param>
        /// <returns>string 属性别名或名称</returns>
        public static string getPropertyName(PropertyInfo info)
        {
            if (info == null) return null;

            var attributes = info.GetCustomAttributes(typeof(AliasAttribute), false);
            if (attributes.Length <= 0) return info.Name;

            var type = (AliasAttribute)attributes[0];
            return type.alias;
        }

        #endregion

        #region 文件操作

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file">文件内容</param>
        /// <param name="name">文件名</param>
        /// <param name="open">是否打开文件，默认不打开</param>
        public static void saveFile(byte[] file, string name, bool open = false)
        {
            var path = Path.GetTempPath() + name;
            if (!File.Exists(path))
            {
                var bw = new BinaryWriter(File.Create(path));
                bw.Write(file);
                bw.Flush();
                bw.Close();
            }

            if (!open) return;

            Process.Start(path);
        }

        #endregion

        #region Image

        /// <summary>
        /// 从文件读取图片数据
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <returns>图片对象</returns>
        public static Image getImageFromFile(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return null;

            try
            {
                return Image.FromFile(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// 获取网络图片
        /// </summary>
        /// <param name="url">图片URL</param>
        /// <returns>Image</returns>
        public static Image getImageFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;

            var iconUrl = Uri.UnescapeDataString(url);
            if (!Uri.IsWellFormedUriString(iconUrl, UriKind.RelativeOrAbsolute)) return null;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 10000;
                var response = (HttpWebResponse)request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    return stream == null ? null : Image.FromStream(stream);
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Image 转换为 byte[]数组
        /// </summary>
        /// <param name="img">图片</param>
        /// <returns>byte[] 数组</returns>
        public static byte[] imageToByteArray(Image img)
        {
            if (img == null) return null;

            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="img">原图片</param>
        /// <param name="width">目标宽度(像素)</param>
        /// <param name="height">目标高度(像素)</param>
        /// <returns>byte[] 缩放后图片的字节数组</returns>
        public static byte[] resize(Image img, int width, int height)
        {
            if (img == null) return null;

            var image = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.Bilinear;

            var rect = new Rectangle(0, 0, width, height);
            graphics.DrawImage(img, rect);
            graphics.Dispose();

            using (var stream = new MemoryStream())
            {
                image.Save(stream, img.RawFormat);
                image.Dispose();

                return stream.ToArray();
            }
        }

        /// <summary>
        /// 获取图片缩略图
        /// </summary>
        /// <param name="img">原图片</param>
        /// <returns>Image 缩略图</returns>
        public static Image getThumbnail(Image img)
        {
            if (img == null) return null;

            var call = new Image.GetThumbnailImageAbort(callback);
            return img.GetThumbnailImage(120, 150, call, IntPtr.Zero);
        }

        private static bool callback()
        {
            return false;
        }

        #endregion

        #region Management

        /// <summary>
        /// 获取CPU序列号
        /// </summary>
        /// <returns>String 序列号</returns>
        public static string getCpuId()
        {
            var myCpu = new ManagementClass("win32_Processor").GetInstances();
            var data = from ManagementObject cpu in myCpu
                       select cpu.Properties["ProcessorId"].Value;
            return data.Aggregate("", (current, val) => current + (val?.ToString() ?? ""));
        }

        /// <summary>
        /// 获取主板序列号
        /// </summary>
        /// <returns>String 序列号</returns>
        public static string getMbId()
        {
            var myMb = new ManagementClass("Win32_BaseBoard").GetInstances();
            var data = from ManagementObject mb in myMb
                       select mb.Properties["SerialNumber"].Value;
            return data.Aggregate("", (current, val) => current + (val?.ToString() ?? ""));
        }

        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        /// <returns>String 序列号</returns>
        public static string getHdId()
        {
            var lpm = new ManagementClass("Win32_PhysicalMedia").GetInstances();
            var data = from ManagementObject hd in lpm
                       select hd.Properties["SerialNumber"].Value;
            return data.Aggregate("", (current, val) => current + (val?.ToString().Trim() ?? ""));
        }

        #endregion

        #region Serialize/Deserialize

        /// <summary>
        /// 将一个对象序列化为Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>string Json字符串</returns>
        public static string serialize(object obj)
        {
            if (obj == null) return null;

            try
            {
                var json = JsonConvert.SerializeObject(obj);
                if (Regex.IsMatch(json, "^[\\[|\\{].*[\\}|\\]]$")) return json;

                return obj.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 将一个Json字符串反序列化为指定类型的对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns>T 反序列化的对象</returns>
        public static T deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) return default(T);

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 将result数据使用Json.NET序列化，并按指定的压缩模式压缩为一个字节数组
        /// </summary>
        /// <param name="result">响应结果数据</param>
        /// <param name="model">压缩模式</param>
        /// <returns>byte[] 压缩后的字节数组</returns>
        public static byte[] jsonWrite(object result, CompressType model)
        {
            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream, Encoding.UTF8))
                {
                    var writer = new JsonTextWriter(streamWriter) {Formatting = Formatting.Indented};
                    new JsonSerializer().Serialize(writer, result);
                    streamWriter.Flush();

                    return compress(stream.ToArray(), model);
                }
            }
        }

        #endregion

        #region Compress/Decompress

        /// <summary>
        /// GZip/Deflate压缩
        /// </summary>
        /// <param name="data">输入字节数组</param>
        /// <param name="model">压缩模式，默认Gzip</param>
        /// <returns>byte[] 压缩后的字节数组</returns>
        public static byte[] compress(byte[] data, CompressType model = CompressType.GZIP)
        {
            using (var ms = new MemoryStream())
            {
                switch (model)
                {
                    case CompressType.GZIP:
                        using (var stream = new GZipStream(ms, CompressionMode.Compress, true))
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        break;
                    case CompressType.DEFLATE:
                        using (var stream = new DeflateStream(ms, CompressionMode.Compress, true))
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        break;
                    case CompressType.NONE:
                        return data;
                    default:
                        return data;
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Gzip/Deflate解压缩
        /// </summary>
        /// <param name="data">输入字节数组</param>
        /// <param name="model">压缩模式，默认Gzip</param>
        /// <returns>byte[] 解压缩后的字节数组</returns>
        public static byte[] decompress(byte[] data, CompressType model = CompressType.GZIP)
        {
            using (var ms = new MemoryStream(data))
            {
                var buffer = new MemoryStream();
                var block = new byte[1024];
                switch (model)
                {
                    case CompressType.GZIP:
                        using (var stream = new GZipStream(ms, CompressionMode.Decompress))
                        {
                            while (true)
                            {
                                var read = stream.Read(block, 0, block.Length);
                                if (read <= 0) break;
                                buffer.Write(block, 0, read);
                            }
                        }
                        break;
                    case CompressType.DEFLATE:
                        using (var stream = new DeflateStream(ms, CompressionMode.Decompress))
                        {
                            while (true)
                            {
                                var read = stream.Read(block, 0, block.Length);
                                if (read <= 0) break;
                                buffer.Write(block, 0, read);
                            }
                        }
                        break;
                    case CompressType.NONE:
                        return data;
                    default:
                        return data;
                }
                return buffer.ToArray();
            }
        }

        #endregion

        #region Encrypt/Decrypt

        /// <summary>
        /// 生成RSA密钥
        /// </summary>
        /// <returns>string 包含RSA公私钥对的JSON字符串</returns>
        public static string createKey()
        {
            var provider = new RSACryptoServiceProvider();
            var key = new
            {
                PublicKey = base64Encode(provider.ToXmlString(false)),
                PrivateKey = base64Encode(provider.ToXmlString(true))
            };
            return serialize(key);
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="key">RSA公钥(xml)</param>
        /// <param name="source">输入明文</param>
        /// <returns>string RSA密文</returns>
        public static string encrypt(string key, string source)
        {
            var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(key);

            var buffer = provider.Encrypt(Encoding.UTF8.GetBytes(source), false);
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="key">RSA私钥(xml)</param>
        /// <param name="source">RSA密文</param>
        /// <returns>string 输出明文</returns>
        public static string decrypt(string key, string source)
        {
            try
            {
                var provider = new RSACryptoServiceProvider();
                provider.FromXmlString(key);

                var buffer = provider.Decrypt(Convert.FromBase64String(source), false);
                return Encoding.UTF8.GetString(buffer);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Loger

        /// <summary>
        /// 将事件消息写入系统日志
        /// </summary>
        /// <param name="source">事件源</param>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public static string logToEvent(string source, string message, EventLogEntryType type)
        {
            try
            {
                if (!EventLog.Exists("应用程序") || !EventLog.SourceExists(source))
                {
                    EventLog.CreateEventSource(source, "应用程序");
                }

                EventLog.WriteEntry(source, message, type);
                return null;
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}
