using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class Account : Client
    {
        [JsonProperty("plan")]
        public string Plan { get; set; }

        [JsonProperty("quotas")]
        public QuotaWrapper Quotas { get; set; }
    }
}