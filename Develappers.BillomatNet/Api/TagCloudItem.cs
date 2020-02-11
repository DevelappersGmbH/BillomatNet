using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class TagCloudItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}