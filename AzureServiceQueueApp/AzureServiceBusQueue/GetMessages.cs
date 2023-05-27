using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureServiceBusQueue
{
    public class GetMessages
    {
        [FunctionName("GetMessages")]
        public void Run([ServiceBusTrigger("appqueue", Connection = "serviceBusConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
