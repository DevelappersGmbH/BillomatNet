using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    /// <summary>
    /// Represents your account.
    /// </summary>
    internal class Account : Client
    {
        [JsonProperty("plan")]
        public string Plan { get; set; }

        [JsonProperty("quotas")]
        public QuotaWrapper Quotas { get; set; }
    }
}