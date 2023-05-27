using Newtonsoft.Json;

namespace ServiceBusHelpers
{
    public abstract class Item
    {
        public abstract string Id { get; }

        public override string ToString()
        {
            string item = JsonConvert.SerializeObject(this);
            return item;
        }
    }
}
