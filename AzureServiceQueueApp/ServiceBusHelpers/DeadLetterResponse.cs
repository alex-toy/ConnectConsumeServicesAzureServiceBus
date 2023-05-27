namespace ServiceBusHelpers
{
    public class DeadLetterResponse
    {
        public string DeadLetterReason { get; set; }
        public string DeadLetterErrorDescription { get; set; }
        public string Body { get; set; }
        public long SequenceNumber { get; set; }

    }
}
