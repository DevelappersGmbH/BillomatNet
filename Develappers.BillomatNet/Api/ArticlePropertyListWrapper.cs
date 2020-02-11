using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticlePropertyListWrapper : PagedListWrapper<ArticlePropertyList>
    {
        [JsonProperty("article-property-values")]
        public override ArticlePropertyList Item { get; set; }
    }
}