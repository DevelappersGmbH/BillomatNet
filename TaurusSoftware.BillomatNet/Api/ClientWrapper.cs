using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ClientWrapper
    {
        [JsonProperty("client")]
        public Client Client { get; set; }
    }
}
