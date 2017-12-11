using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Json
{
    internal class ClientWrapper
    {
        [JsonProperty("client")]
        public Client Client { get; set; }
    }
}
