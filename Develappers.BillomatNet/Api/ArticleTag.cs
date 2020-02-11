using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticleTag
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("article_id")]
        public int ArticleId { get; set; }
    }
}