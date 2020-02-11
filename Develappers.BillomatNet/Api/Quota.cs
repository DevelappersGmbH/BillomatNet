using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Quota
    {
        [JsonProperty("entity")]
        public string Entity { get; set; }

        [JsonProperty("available")]
        public string Available { get; set; }

        [JsonProperty("used")]
        public string Used { get; set; }
    }
}