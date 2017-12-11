using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Json
{
    internal class QuotaWrapper
    {
        [JsonProperty("quota")]
        public List<Quota> Quota { get; set; }
    }
}