using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using Insight.Utils.Entity;
using Newtonsoft.Json;

namespace Insight.Utils.Common
{
    public static class Util
    {

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
        /// 保存配置项的值
        /// </summary>
        /// <param name="key">配置项</param>
        /// <param name="value">配置项的值</param>
        public static void SaveAppSetting(string key, string value)
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
        public static string Hash(string str)
        {
            var md5 = MD5.Create();
            var s = md5.ComputeHash(Encoding.UTF8.GetBytes(str.Trim()));
            return s.Aggregate("", (current, c) => current + c.ToString("X2"));
        }

        /// <summary>
        /// 将对象序列化为Json后再进行Base64编码
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
        /// 复制对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">源对象</param>
        /// <returns>T 复制的对象</returns>
        public static T Clone<T>(T obj)
        {
            var str = Serialize(obj);
            return Deserialize<T>(str);
        }

        /// <summary>
        /// 将Result转换为指定的类型
        /// </summary>
        /// <typeparam name="T">转换目标类型</typeparam>
        /// <param name="obj">Result对象</param>
        /// <returns>T 转换后的类型</returns>
        public static T ConvertTo<T>(Result obj)
        {
            var str = Serialize(obj);
            return Deserialize<T>(str);
        }

        /// <summary>
        /// 保存文件并打开
        /// </summary>
        /// <param name="file">文件内容</param>
        /// <param name="name">文件名</param>
        public static void SaveFile(byte[] file, string name)
        {
            var path = Path.GetTempPath() + name;
            if (!File.Exists(path))
            {
                var bw = new BinaryWriter(File.Create(path));
                bw.Write(file);
                bw.Flush();
                bw.Close();
            }
            Process.Start(path);
        }

        #endregion

        #region Image

        /// <summary>
        /// 从文件读取图片数据
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <returns>图片对象</returns>
        public static Image GetImage(string path)
        {
            return Image.FromFile(path);
        }

        /// <summary>
        /// Image 转换为 byte[]数组
        /// </summary>
        /// <param name="img">图片</param>
        /// <returns>byte[] 数组</returns>
        public static byte[] ImageToByteArray(Image img)
        {
            if (img == null) return null;

            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 获取图片缩略图
        /// </summary>
        /// <param name="img">原图片</param>
        /// <returns>Image 缩略图</returns>
        public static Image GetThumbnail(Image img)
        {
            if (img == null) return null;

            var callb = new Image.GetThumbnailImageAbort(Callback);
            return img.GetThumbnailImage(120, 150, callb, IntPtr.Zero);
        }

        private static bool Callback()
        {
            return false;
        }

        #endregion

        #region Management

        /// <summary>
        /// 获取CPU序列号
        /// </summary>
        /// <returns>String 序列号</returns>
        public static string GetCpuId()
        {
            var myCpu = new ManagementClass("win32_Processor").GetInstances();
            var data = from ManagementObject cpu in myCpu
                       select cpu.Properties["Processorid"].Value;
            return data.Aggregate("", (current, val) => current + (val?.ToString() ?? ""));
        }

        /// <summary>
        /// 获取主板序列号
        /// </summary>
        /// <returns>String 序列号</returns>
        public static string GetMbId()
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
        public static string GetHdId()
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
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>string Json字符串</returns>
        public static string Serialize<T>(T obj)
        {
            return obj == null ? String.Empty : JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将一个Json字符串反序列化为指定类型的对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns>T 反序列化的对象</returns>
        public static T Deserialize<T>(string json)
        {
            return String.IsNullOrEmpty(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
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
