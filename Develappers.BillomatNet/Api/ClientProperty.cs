using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ClientProperty
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("client_id")]
        public int ClientId { get; set; }
        [JsonProperty("client_property_id")]
        public int ClientPropertyId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
