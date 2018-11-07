using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ContactWrapper
    {
        [JsonProperty("contact")]
        public Contact Contact { get; set; }
    }
}
