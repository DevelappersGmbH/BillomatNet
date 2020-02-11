using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticleListWrapper : PagedListWrapper<ArticleList>
    {
        [JsonProperty("articles")]
        public override ArticleList Item { get; set; }
    }
}