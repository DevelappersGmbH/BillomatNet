using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Json
{
    internal class AccountWrapper
    {
        [JsonProperty("client")]
        public Account Client { get; set; }
    }
}
