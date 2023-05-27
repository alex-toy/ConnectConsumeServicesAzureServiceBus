using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureServiceBusTopic
{
    public class GetMessages
    {
        private readonly ILogger<GetMessages> _logger;

        public GetMessages(ILogger<GetMessages> log)
        {
            _logger = log;
        }

        [FunctionName("GetMessages")]
        public void Run([ServiceBusTrigger("apptopic", "SubscriptionA", Connection = "connectionString")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
