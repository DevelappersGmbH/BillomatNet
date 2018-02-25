using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ArticleTagWrapper
    {
        [JsonProperty("article-tag")]
        public ArticleTag ArticleTag { get; set; }
    }
}