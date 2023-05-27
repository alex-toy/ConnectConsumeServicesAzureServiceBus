using AzureServiceQueueApp.Models;
using ServiceBusHelpers;
using System;
using System.Collections.Generic;

namespace AzureServiceQueueApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connection_string = "Endpoint=sb://alexeiservicequeuens.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=wdhLeD91ATZJJHKg8tVo24CQCg6dPDREM+ASbEd8WaA=;EntityPath=appqueue";
            string queue_name = "appqueue";

            var serviceBus = new ServiceBusHelper(connection_string, queue_name);


            //List<Order> orders = new List<Order>()
            //{
            //    new Order() {OrderID="O1",Quantity=10,UnitPrice=9.99m},
            //    new Order() {OrderID="O2",Quantity=15,UnitPrice=10.99m },
            //    new Order() {OrderID="O3",Quantity=20,UnitPrice=11.99m},
            //    new Order() {OrderID="O4",Quantity=25,UnitPrice=12.99m},
            //    new Order() {OrderID="O5",Quantity=30,UnitPrice=13.99m }
            //};
            //serviceBus.SendMessages(orders, 10);


            //Response response = serviceBus.PeekMessage();
            //Console.WriteLine(response.Body);
            //Console.WriteLine($"The Sequence number is {response.SequenceNumber}");


            //List<Response> responses = serviceBus.ReceiveMessages(1);
            //foreach (var response in responses)
            //{
            //    Console.WriteLine(response.Body);
            //    Console.WriteLine($"The Sequence number is {response.SequenceNumber}");
            //}


            List<DeadLetterResponse> responses = serviceBus.ReceiveMessagesFromDLQ(3);
            foreach (var response in responses)
            {
                Console.WriteLine(response.Body);
                Console.WriteLine($"The Sequence number is {response.SequenceNumber}");
                Console.WriteLine($"Dead Letter Reason {response.DeadLetterReason}");
                Console.WriteLine($"Dead Letter Description {response.DeadLetterErrorDescription}");
            }
        }
    }
}
