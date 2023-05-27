using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;

namespace ServiceBusHelpers
{
    public class ServiceBusHelper
    {
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly ServiceBusClient _client;

        public ServiceBusHelper(string connectionString, string queueName)
        {
            _connectionString = connectionString;
            _queueName = queueName;
            _client = new ServiceBusClient(connectionString);
        }

        public void SendMessages(IEnumerable<Item> items, int TTL = 30)
        {
            ServiceBusSender _sender = _client.CreateSender(_queueName);

            foreach (var item in items)
            {
                string body = item.ToString();
                ServiceBusMessage message = new ServiceBusMessage(body);
                message.ContentType = "application/json";
                message.TimeToLive = TimeSpan.FromSeconds(TTL);
                _sender.SendMessageAsync(message).GetAwaiter().GetResult();
            }
        }

        public Response PeekMessage()
        {
            ServiceBusReceiverOptions options = new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock };
            ServiceBusReceiver _receiver = _client.CreateReceiver(_queueName, options);
            ServiceBusReceivedMessage message = _receiver.ReceiveMessageAsync().GetAwaiter().GetResult();
            return new Response() { Body = message.Body.ToString(), SequenceNumber = message.SequenceNumber };
        }

        public List<Response> ReceiveMessages(int count)
        {
            ServiceBusReceiverOptions options = new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete };
            ServiceBusReceiver _receiver = _client.CreateReceiver(_queueName, options);
            IReadOnlyList<ServiceBusReceivedMessage> messages = _receiver.ReceiveMessagesAsync(count).GetAwaiter().GetResult();

            var responses = new List<Response>();
            foreach (var message in messages)
            {
                responses.Add(new Response()
                {
                    Body = message.Body.ToString(),
                    SequenceNumber = message.SequenceNumber
                });
                _receiver.CompleteMessageAsync(message);
            }
            return responses;
        }

        public List<DeadLetterResponse> ReceiveMessagesFromDLQ(int count)
        {
            string connectionString = $"{_connectionString}/$DeadLetterQueue";
            ServiceBusClient client = new ServiceBusClient(connectionString);
            string dead_letter_queue_name = $"{_queueName}/$DeadLetterQueue";
            ServiceBusReceiverOptions options = new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock };
            ServiceBusReceiver _receiver = client.CreateReceiver(dead_letter_queue_name, options);
            IReadOnlyList<ServiceBusReceivedMessage> messages = _receiver.ReceiveMessagesAsync(count).GetAwaiter().GetResult();

            var responses = new List<DeadLetterResponse>();
            foreach (var message in messages)
            {
                responses.Add(new DeadLetterResponse()
                {
                    DeadLetterReason = message.DeadLetterReason,
                    DeadLetterErrorDescription = message.DeadLetterErrorDescription,
                    Body = message.Body.ToString(),
                    SequenceNumber = message.SequenceNumber
                });
            }
            return responses;
        }
    }
}
