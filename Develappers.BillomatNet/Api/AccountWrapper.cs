using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class AccountWrapper
    {
        [JsonProperty("client")]
        public Account Client { get; set; }
    }
}
