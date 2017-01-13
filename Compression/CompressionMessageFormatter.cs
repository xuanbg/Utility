using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Insight.WCF.Compression
{
    public class CompressionMessageFormatter : IDispatchMessageFormatter, IClientMessageFormatter
    {
        private const string DataContractSerializerOperationFormatterTypeName = "System.ServiceModel.Dispatcher.DataContractSerializerOperationFormatter, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

        public IDispatchMessageFormatter InnerDispatchMessageFormatter { get; }

        public IClientMessageFormatter InnerClientMessageFormatter { get; }

        public MessageCompressor MessageCompressor { get; }

        public CompressionMessageFormatter(CompressionAlgorithm algorithm, OperationDescription description, DataContractFormatAttribute dataContractFormatAttribute, DataContractSerializerOperationBehavior serializerFactory)
        {
            MessageCompressor = new MessageCompressor(algorithm);
            var innerFormatterType = Type.GetType(DataContractSerializerOperationFormatterTypeName);
            var innerFormatter = Activator.CreateInstance(innerFormatterType, description, dataContractFormatAttribute,
                serializerFactory);
            InnerClientMessageFormatter = innerFormatter as IClientMessageFormatter;
            InnerDispatchMessageFormatter = innerFormatter as IDispatchMessageFormatter;
        }

        public void DeserializeRequest(Message message, object[] parameters)
        {
            message = MessageCompressor.DecompressMessage(message);
            InnerDispatchMessageFormatter.DeserializeRequest(message, parameters);
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            var message = InnerDispatchMessageFormatter.SerializeReply(messageVersion, parameters, result);
            return MessageCompressor.CompressMessage(message);
        }

        public object DeserializeReply(Message message, object[] parameters)
        {
            message = MessageCompressor.DecompressMessage(message);
            return InnerClientMessageFormatter.DeserializeReply(message, parameters);
        }

        public Message SerializeRequest(MessageVersion messageVersion, object[] parameters)
        {
            var message = InnerClientMessageFormatter.SerializeRequest(messageVersion, parameters);
            return MessageCompressor.CompressMessage(message);
        }
    }
}