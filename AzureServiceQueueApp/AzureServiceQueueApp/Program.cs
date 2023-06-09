﻿using AzureServiceQueueApp.Models;
using ServiceBusHelpers;
using System;
using System.Collections.Generic;

namespace AzureServiceQueueApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SimpleSend();
            //SendMessages();


            //SendWithProperties();
            //ReceiveWithProperties();

            SendTopic();
            //ReceiveFromTopic();




            //Response response = serviceBus.PeekMessage();
            //Console.WriteLine(response.Body);
            //Console.WriteLine($"The Sequence number is {response.SequenceNumber}");


            //List<Response> responses = serviceBus.ReceiveMessages(1);
            //foreach (var response in responses)
            //{
            //    Console.WriteLine(response.Body);
            //    Console.WriteLine($"The Sequence number is {response.SequenceNumber}");
            //}


            //List<DeadLetterResponse> responses = serviceBus.ReceiveMessagesFromDLQ(3);
            //foreach (var response in responses)
            //{
            //    Console.WriteLine(response.Body);
            //    Console.WriteLine($"The Sequence number is {response.SequenceNumber}");
            //    Console.WriteLine($"Dead Letter Reason {response.DeadLetterReason}");
            //    Console.WriteLine($"Dead Letter Description {response.DeadLetterErrorDescription}");
            //}

            //SendDuplicateMessage();
        }

        private static void SendMessages()
        {
            string connection_string = "Endpoint=sb://alexeiservicequeuens.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=wdhLeD91ATZJJHKg8tVo24CQCg6dPDREM+ASbEd8WaA=;EntityPath=appqueue";
            string queue_name = "appqueue";

            var serviceBus = new ServiceBusHelper(connection_string, queue_name);

            List<Order> orders = new List<Order>()
            {
                new Order() {OrderID="01",Quantity=10,UnitPrice=9.99m},
                new Order() {OrderID="02",Quantity=15,UnitPrice=10.99m },
                new Order() {OrderID="03",Quantity=20,UnitPrice=11.99m},
                new Order() {OrderID="04",Quantity=25,UnitPrice=12.99m},
                new Order() {OrderID="05",Quantity=30,UnitPrice=13.99m }
            };
            serviceBus.SendMessages(orders);
        }

        private static void SimpleSend()
        {
            string connection_string = "Endpoint=sb://alexeiservicequeuens.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=wdhLeD91ATZJJHKg8tVo24CQCg6dPDREM+ASbEd8WaA=;EntityPath=appqueue";
            string queue_name = "appqueue";

            var serviceBus = new ServiceBusHelper(connection_string, queue_name);

            var order = new Order() { OrderID = "001", Quantity = 10, UnitPrice = 9.99m };
            serviceBus.SendMessage(order);
        }

        private static void SendDuplicateMessage()
        {
            string connection_string = "Endpoint=sb://alexeiservicequeuens.servicebus.windows.net/;SharedAccessKeyName=Sens;SharedAccessKey=qLMlc0kbPPoXw/n2kAnDADxc6ac0igjOp+ASbHUZ3MU=;EntityPath=dmdqueue";
            string queue_name = "dmdqueue";

            var serviceBus = new ServiceBusHelper(connection_string, queue_name);

            var order1 = new Order() { OrderID = "01", Quantity = 10, UnitPrice = 9.99m };
            serviceBus.SendMessage(order1);
            serviceBus.SendMessage(order1);
            serviceBus.SendMessage(order1);
        }

        private static void SendWithProperties()
        {
            string connection_string = "Endpoint=sb://alexeiservicequeuens.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=wdhLeD91ATZJJHKg8tVo24CQCg6dPDREM+ASbEd8WaA=;EntityPath=appqueue";
            string queue_name = "appqueue";

            var serviceBus = new ServiceBusHelper(connection_string, queue_name);

            var order = new Order() { OrderID = "001", Quantity = 10, UnitPrice = 9.99m };
            var properties = new Dictionary<string, string> { { "departement", "engineering" }, { "boss", "alexei" } };
            serviceBus.SendMessage(order, properties);
        }

        private static void ReceiveWithProperties()
        {
            string connection_string = "Endpoint=sb://alexeiservicequeuens.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=wdhLeD91ATZJJHKg8tVo24CQCg6dPDREM+ASbEd8WaA=;EntityPath=appqueue";
            string queue_name = "appqueue";

            var serviceBus = new ServiceBusHelper(connection_string, queue_name);

            List<Response> responses = serviceBus.ReceiveMessages(1);
            foreach (var response in responses)
            {
                Console.WriteLine(response.Body);
                Console.WriteLine($"The Sequence number is {response.SequenceNumber}");
                foreach (var property in response.Properties)
                {
                    Console.WriteLine($"{property.Key} : {property.Value}");
                }
            }

        }

        private static void SendTopic()
        {
            string connection_string = "Endpoint=sb://alexeiservicequeuens.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=oFBip3efjsaKvqJIxNzlWg23V97Qc5Nnx+ASbKYr1cw=;EntityPath=apptopic";
            string queue_name = "apptopic";
            var serviceBus = new ServiceBusHelper(connection_string, queue_name);

            List<Order> orders = new List<Order>()
            {
                new Order() {OrderID="01",Quantity=10,UnitPrice=9.99m},
                new Order() {OrderID="02",Quantity=15,UnitPrice=10.99m },
                new Order() {OrderID="03",Quantity=20,UnitPrice=11.99m},
                new Order() {OrderID="04",Quantity=25,UnitPrice=12.99m},
                new Order() {OrderID="05",Quantity=30,UnitPrice=13.99m }
            };
            serviceBus.SendMessages(orders);
        }

        private static void ReceiveFromTopic()
        {
            string connection_string = "Endpoint=sb://alexeiservicequeuens.servicebus.windows.net/;SharedAccessKeyName=listen;SharedAccessKey=liS0ca+PUUAlsU5CB1bq1UUV6yDyXU26H+ASbMxqk0s=;EntityPath=apptopic";
            string queue_name = "apptopic";

            var serviceBus = new ServiceBusHelper(connection_string, queue_name);

            List<Response> responses = serviceBus.ReceiveMessagesFromTopic(3, "SubscriptionA");
            foreach (var response in responses)
            {
                Console.WriteLine(response.Body);
                Console.WriteLine($"The Sequence number is {response.SequenceNumber}");
                foreach (var property in response.Properties)
                {
                    Console.WriteLine($"{property.Key} : {property.Value}");
                }
            }

        }
    }
}
