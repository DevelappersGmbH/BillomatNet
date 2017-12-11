using System;
using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Json
{
    internal class AccountOwner : Client
    {
        [JsonProperty("plan")]
        public string Plan { get; set; }

        [JsonProperty("quotas")]
        public QuotaWrapper Quotas { get; set; }
    }
}