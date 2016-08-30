using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Windows.Forms;
using Insight.WCF.Entity;

namespace Insight.WCF
{
    public class Services
    {
        private readonly List<ServiceHost> Hosts = new List<ServiceHost>();

        #region 公共方法

        /// <summary>
        /// 创建WCF服务主机
        /// </summary>
        /// <param name="info">ServiceInfo</param>
        public void CreateHost(ServiceInfo info)
        {
            var file = $"{Application.StartupPath}\\{info.ServiceFile}";
            if (!File.Exists(file)) return;

            var ver = string.IsNullOrEmpty(info.Version) ? "" : "/" + info.Version;
            var path = string.IsNullOrEmpty(info.Path) ? "" : "/" + info.Path;
            var address = new Uri($"{info.BaseAddress}:{info.Port}{path}{ver}");
            var asm = Assembly.LoadFrom(file);
            var host = new ServiceHost(asm.GetType($"{info.NameSpace}.{info.ComplyType}"), address);
            var binding = InitBinding();
            var inter = $"{info.NameSpace}.{info.Interface}";
            var endpoint = host.AddServiceEndpoint(asm.GetType(inter), binding, "");
            endpoint.Behaviors.Add(new WebHttpBehavior());
            Hosts.Add(host);
        }

        /// <summary>
        /// 启动服务列表中的全部服务
        /// </summary>
        public void StartService()
        {
            var hosts = Hosts.Where(h => h.State == CommunicationState.Created || h.State == CommunicationState.Closed);
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
            var host = Hosts.SingleOrDefault(h => h.Description.Name == service);
            if (host == null || (host.State != CommunicationState.Created && host.State != CommunicationState.Closed)) return;

            host.Open();
        }

        /// <summary>
        /// 停止服务列表中的全部服务
        /// </summary>
        public void StopService()
        {
            foreach (var host in Hosts.Where(host => host.State == CommunicationState.Opened))
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
            var host = Hosts.SingleOrDefault(h => h.Description.Name == service);
            if (host == null || host.State != CommunicationState.Opened) return;

            host.Abort();
            host.Close();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化基本HTTP服务绑定
        /// </summary>
        private CustomBinding InitBinding()
        {
            var encoder = new WebMessageEncodingBindingElement { ReaderQuotas = { MaxArrayLength = 67108864, MaxStringContentLength = 67108864 } };
            var transport = new HttpTransportBindingElement { ManualAddressing = true, MaxReceivedMessageSize = 1073741824, TransferMode = TransferMode.Streamed };
            var binding = new CustomBinding { SendTimeout = TimeSpan.FromSeconds(600), ReceiveTimeout = TimeSpan.FromSeconds(600) };
            binding.Elements.AddRange(encoder, transport);
            return binding;
        }

        #endregion

    }

}
