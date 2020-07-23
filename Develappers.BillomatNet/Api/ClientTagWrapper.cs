using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ClientTagWrapper
    {
        [JsonProperty("client-tag")]
        public ClientTag ClientTag { get; set; }
    }
}