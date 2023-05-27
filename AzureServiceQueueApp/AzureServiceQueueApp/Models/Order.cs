using ServiceBusHelpers;

namespace AzureServiceQueueApp.Models
{
    public class Order : Item
    {
        public string OrderID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
