using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class SupplierPropertyValue
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("supplier_id")]
        public string SupplierId { get; set; }
        [JsonProperty("supplier_property_id")]
        public string SupplierPropertyId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
