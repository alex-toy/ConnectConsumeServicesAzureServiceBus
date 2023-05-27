using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureServiceBusQueue
{
    public class GetMessages
    {
        [FunctionName("GetMessages")]
        public void Run([ServiceBusTrigger("appqueue", Connection = "serviceBusConnection")]Message message, ILogger log)
        {
            string body = Encoding.UTF8.GetString(message.Body);
            log.LogInformation($"Body : {body}");
            log.LogInformation($"ContentType : {message.ContentType}");
        }
    }
}
