using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticleWrapper
    {
        [JsonProperty("article")]
        public Article Article { get; set; }
    }
}