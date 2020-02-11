using System.Collections.Generic;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class QuotaWrapper
    {
        [JsonProperty("quota")]
        public List<Quota> Quota { get; set; }
    }
}