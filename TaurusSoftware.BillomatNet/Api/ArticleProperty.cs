using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ArticleProperty
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("article_id")]
        public int ArticleId { get; set; }

        [JsonProperty("article_property_id")]
        public int ArticlePropertyId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}