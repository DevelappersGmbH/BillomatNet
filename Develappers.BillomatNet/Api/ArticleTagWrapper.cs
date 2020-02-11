using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticleTagWrapper
    {
        [JsonProperty("article-tag")]
        public ArticleTag ArticleTag { get; set; }
    }
}