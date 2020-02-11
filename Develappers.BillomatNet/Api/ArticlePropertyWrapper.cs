using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticlePropertyWrapper
    {
        [JsonProperty("article-property-value")]
        public ArticleProperty ArticleProperty { get; set; }
    }
}