using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Insight.WCF.CustomEncoder
{
    public class JsonBehavior : WebHttpBehavior
    {
        /// <summary>
        /// 设置请求消息反序列化器
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        protected override IDispatchMessageFormatter GetRequestDispatchFormatter(OperationDescription operationDescription, ServiceEndpoint endpoint)
        {
            var innerFormatter = base.GetRequestDispatchFormatter(operationDescription, endpoint);
            return new JsonDispatchFormatter(innerFormatter, operationDescription);
        }

        /// <summary>
        /// 设置响应消息序列化器
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        protected override IDispatchMessageFormatter GetReplyDispatchFormatter(OperationDescription operationDescription, ServiceEndpoint endpoint)
        {
            var innerFormatter = base.GetRequestDispatchFormatter(operationDescription, endpoint);
            return new JsonDispatchFormatter(innerFormatter, operationDescription);
        }

        /// <summary>
        /// ServiceEndpoint 检查器
        /// </summary>
        /// <param name="endpoint"></param>
        public override void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
