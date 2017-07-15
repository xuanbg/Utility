using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Insight.WCF
{
    public class Service
    {
        private readonly List<ServiceHost> _Hosts = new List<ServiceHost>();

        /// <summary>
        /// 读取服务目录下的WCF服务库创建WCF服务主机
        /// </summary>
        /// <param name="address">服务基地址</param>
        /// <param name="allowOrigin">允许跨域访问的域，多个域名使用逗号分隔</param>
        public void CreateHosts(string address, string allowOrigin)
        {
            var dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Services");
            var files = dirInfo.GetFiles("*.dll", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file.FullName);
                var product = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
                if (product != "WCF Service") continue;

                var name = assembly.GetName();
                var type = assembly.DefinedTypes.Single(i => i.Name == name.Name);
                var ln = name.Name.ToLower();
                var api = ln.EndsWith("s") ? ln.Substring(0, ln.Length - 1) : ln;
                var uri = new Uri($"{address}/{api}api/v{name.Version.Major}.{name.Version.Minor}");
                CreateHost(type, uri, allowOrigin);
            }
        }

        /// <summary>
        /// 启动服务列表中的全部服务
        /// </summary>
        public void StartService()
        {
            var hosts = _Hosts.Where(h => h.State == CommunicationState.Created || h.State == CommunicationState.Closed);
            foreach (var host in hosts)
            {
                host.Open();
            }
        }

        /// <summary>
        /// 启动服务列表中的服务
        /// </summary>
        /// <param name="service">服务名称</param>
        public void StartService(string service)
        {
            var host = _Hosts.SingleOrDefault(h => h.Description.Name == service);
            if (host == null || (host.State != CommunicationState.Created && host.State != CommunicationState.Closed)) return;

            host.Open();
        }

        /// <summary>
        /// 停止服务列表中的全部服务
        /// </summary>
        public void StopService()
        {
            foreach (var host in _Hosts.Where(host => host.State == CommunicationState.Opened))
            {
                host.Abort();
                host.Close();
            }
        }

        /// <summary>
        /// 停止服务列表中的服务
        /// </summary>
        /// <param name="service">服务名称</param>
        public void StopService(string service)
        {
            var host = _Hosts.SingleOrDefault(h => h.Description.Name == service);
            if (host == null || host.State != CommunicationState.Opened) return;

            host.Abort();
            host.Close();
        }

        /// <summary>
        /// 创建WCF服务主机
        /// </summary>
        /// <param name="type">TypeInfo</param>
        /// <param name="uri">Uri</param>
        /// <param name="allowOrigin">允许跨域访问的域</param>
        private void CreateHost(TypeInfo type, Uri uri, string allowOrigin)
        {
            var host = new ServiceHost(type, uri);
            var binding = InitBinding();
            var endpoint = host.AddServiceEndpoint(type.ImplementedInterfaces.First(), binding, "");
            var behavior = new CustomWebHttpBehavior {AutomaticFormatSelectionEnabled = true, AllowOrigin = allowOrigin};
            endpoint.Behaviors.Add(behavior);

            /* Windows Server 2008 需要设置MaxItemsInObjectGraph值为2147483647
            foreach (var operation in endpoint.Contract.Operations)
            {
                var behavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (behavior != null)
                {
                    behavior.MaxItemsInObjectGraph = 2147483647;
                }
            }*/
            _Hosts.Add(host);
            LogToEvent($"WCF 服务{type.Name}已绑定于：{uri}");
        }

        /// <summary>
        /// 初始化基本HTTP服务绑定
        /// </summary>
        private CustomBinding InitBinding()
        {
            var encoder = new WebMessageEncodingBindingElement
            {
                ReaderQuotas = {MaxArrayLength = 67108864, MaxStringContentLength = 67108864}
            };
            var transport = new HttpTransportBindingElement
            {
                ManualAddressing = true,
                MaxReceivedMessageSize = 1073741824,
                TransferMode = TransferMode.Streamed
            };
            var binding = new CustomBinding
            {
                SendTimeout = TimeSpan.FromSeconds(600),
                ReceiveTimeout = TimeSpan.FromSeconds(600)
            };

            //var gZipEncode = new CompressEncodingBindingElement(encoder);
            binding.Elements.AddRange(encoder, transport);
            return binding;
        }

        /// <summary>
        /// 将事件消息写入系统日志
        /// </summary>
        /// <param name="message"></param>
        public void LogToEvent(string message)
        {
            if (!EventLog.SourceExists("WCF Service"))
            {
                EventLog.CreateEventSource("WCF Service", "应用程序");
            }

            EventLog.WriteEntry("WCF Service", message, EventLogEntryType.Information);
        }
    }
}