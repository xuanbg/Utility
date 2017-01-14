using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Insight.WCF.CustomEncoder
{
    public class CompressInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var property = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
            var accept = property.Headers[HttpRequestHeader.AcceptEncoding];
            switch (accept)
            {
                case "gzip":
                    OperationContext.Current.Extensions.Add(new GzipExtension());
                    break;
                case "deflate":
                    OperationContext.Current.Extensions.Add(new DeflateExtension());
                    break;
            }
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var property = reply.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
            var exts = OperationContext.Current.Extensions;
            if (exts.OfType<GzipExtension>().Any())
            {
                property.Headers.Add(HttpResponseHeader.ContentEncoding, "gzip");
            }
            else if (exts.OfType<DeflateExtension>().Any())
            {
                property.Headers.Add(HttpResponseHeader.ContentEncoding, "deflate");
            }
        }
    }

    public class GzipExtension : IExtension<OperationContext>
    {
        public void Attach(OperationContext owner)
        {
        }

        public void Detach(OperationContext owner)
        {
        }
    }

    public class DeflateExtension : IExtension<OperationContext>
    {
        public void Attach(OperationContext owner)
        {
        }

        public void Detach(OperationContext owner)
        {
        }
    }
}