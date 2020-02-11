using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ContactWrapper
    {
        [JsonProperty("contact")]
        public Contact Contact { get; set; }
    }
}
