using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Json
{
    internal class AccountOwnerWrapper
    {
        [JsonProperty("client")]
        public AccountOwner Client { get; set; }
    }
}
