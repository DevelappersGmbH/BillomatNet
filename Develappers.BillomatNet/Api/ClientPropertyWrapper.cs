using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ClientPropertyWrapper
    {
        [JsonProperty("client-property-value")]
        public ClientProperty ClientProperty { get; set; }
    }
}
