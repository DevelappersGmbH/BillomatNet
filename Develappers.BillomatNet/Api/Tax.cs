using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Tax
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("rate")]
        public string Rate { get; set; }
        [JsonProperty("is_default")]
        public string IsDefault { get; set; }
    }
}