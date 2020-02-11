using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ClientWrapper
    {
        [JsonProperty("client")]
        public Client Client { get; set; }
    }
}
