using System.Collections.Generic;

namespace ServiceBusHelpers
{
    public class Response
    {
        public string Body { get; set; }
        public long SequenceNumber { get; set; }
        public Dictionary<string, string> Properties { get; set; }

    }
}
