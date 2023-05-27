using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;

namespace ServiceBusHelpers
{
    public class ServiceBusHelper
    {
        private readonly string _connectionString;
        private readonly string _queueName;
        private ServiceBusSender _sender;
        private readonly ServiceBusClient _client;

        public ServiceBusHelper(string connectionString, string queueName)
        {
            _connectionString = connectionString;
            _queueName = queueName;
            _client = new ServiceBusClient(connectionString);
        }

        public void SendMessage(Item item, Dictionary<string, string> properties = null, int TTL = 30)
        {
            if (_sender == null) _sender = _client.CreateSender(_queueName);

            string body = item.ToString();
            ServiceBusMessage message = new ServiceBusMessage(body);
            message.ContentType = "application/json";
            message.TimeToLive = TimeSpan.FromSeconds(TTL);
            message.MessageId = item.Id;
            if (properties != null)
            {
                foreach (var property in properties) message.ApplicationProperties.Add(property.Key, property.Value);
            }
            _sender.SendMessageAsync(message).GetAwaiter().GetResult();
        }

        public void SendMessages(IEnumerable<Item> items, Dictionary<string, string> properties = null, int TTL = 30)
        {
            foreach (var item in items)
            {
                SendMessage(item, properties, TTL);
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
                var response = new Response()
                {
                    Body = message.Body.ToString(),
                    SequenceNumber = message.SequenceNumber,
                    Properties = new Dictionary<string, string>()
                };

                IReadOnlyDictionary<string, object> properties = message.ApplicationProperties;
                foreach (var property in properties)
                {
                    string key = property.Key;
                    string value = property.Value.ToString();
                    response.Properties.Add(key, value);
                }
                responses.Add(response);

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

        public List<Response> ReceiveMessagesFromTopic(int count, string subscription)
        {
            ServiceBusReceiverOptions options = new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete };
            ServiceBusReceiver _receiver = _client.CreateReceiver(_queueName, subscription, options);
            IReadOnlyList<ServiceBusReceivedMessage> messages;
            try
            {
                messages = _receiver.ReceiveMessagesAsync(count).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
            }

            var responses = new List<Response>();
            foreach (var message in messages)
            {
                var response = new Response()
                {
                    Body = message.Body.ToString(),
                    SequenceNumber = message.SequenceNumber,
                    Properties = new Dictionary<string, string>()
                };

                IReadOnlyDictionary<string, object> properties = message.ApplicationProperties;
                foreach (var property in properties)
                {
                    string key = property.Key;
                    string value = property.Value.ToString();
                    response.Properties.Add(key, value);
                }
                responses.Add(response);

                _receiver.CompleteMessageAsync(message);
            }
            return responses;
        }
    }
}
