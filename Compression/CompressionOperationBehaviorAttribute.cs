using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Insight.WCF.Compression
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CompressionOperationBehaviorAttribute : Attribute, IOperationBehavior
    {
        public CompressionAlgorithm Algorithm { get; set; }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.SerializeRequest = true;
            clientOperation.DeserializeReply = true;
            var dataContractFormatAttribute = operationDescription.SyncMethod.GetCustomAttributes(typeof(DataContractFormatAttribute), true).FirstOrDefault() as DataContractFormatAttribute ?? new DataContractFormatAttribute();

            var dataContractSerializerOperationBehavior = operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>();
            clientOperation.Formatter = new CompressionMessageFormatter(Algorithm, operationDescription, dataContractFormatAttribute, dataContractSerializerOperationBehavior);
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.SerializeReply = true;
            dispatchOperation.DeserializeRequest = true;
            var dataContractFormatAttribute = operationDescription.SyncMethod.GetCustomAttributes(typeof(DataContractFormatAttribute), true).FirstOrDefault() as DataContractFormatAttribute ?? new DataContractFormatAttribute();
            var dataContractSerializerOperationBehavior = operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>();
            dispatchOperation.Formatter = new CompressionMessageFormatter(Algorithm, operationDescription, dataContractFormatAttribute, dataContractSerializerOperationBehavior);
        }

        public void Validate(OperationDescription operationDescription)
        {
        }
    }
}