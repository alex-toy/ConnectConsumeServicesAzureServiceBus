using Azure.Messaging.ServiceBus;
using System.Collections.Generic;

namespace ServiceBusHelpers
{
    public class ServiceBusHelper
    {
        private readonly string _queueName;
        private readonly ServiceBusClient _client;

        public ServiceBusHelper(string connectionString, string queueName)
        {
            _queueName = queueName;
            _client = new ServiceBusClient(connectionString);
        }

        public void SendMessages(IEnumerable<Item> items)
        {
            ServiceBusSender _sender = _client.CreateSender(_queueName);

            foreach (var item in items)
            {
                string body = item.ToString();
                ServiceBusMessage _message = new ServiceBusMessage(body);
                _message.ContentType = "application/json";
                _sender.SendMessageAsync(_message).GetAwaiter().GetResult();
            }
        }

        public Response PeekMessage()
        {
            ServiceBusReceiverOptions options = new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock };
            ServiceBusReceiver _receiver = _client.CreateReceiver(_queueName, options);
            ServiceBusReceivedMessage _message = _receiver.ReceiveMessageAsync().GetAwaiter().GetResult();
            return new Response() { Message = _message.Body.ToString(), SequenceNumber = _message.SequenceNumber };
        }
    }
}
