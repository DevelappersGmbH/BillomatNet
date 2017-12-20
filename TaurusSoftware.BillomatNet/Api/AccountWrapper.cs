using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class AccountWrapper
    {
        [JsonProperty("client")]
        public Account Client { get; set; }
    }
}
