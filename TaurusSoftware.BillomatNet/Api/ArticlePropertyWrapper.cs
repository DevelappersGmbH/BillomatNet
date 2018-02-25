using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ArticlePropertyWrapper
    {
        [JsonProperty("article-property-value")]
        public ArticleProperty ArticleProperty { get; set; }
    }
}