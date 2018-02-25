using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ArticleTagListWrapper : PagedListWrapper<ArticleTagList>
    {
        [JsonProperty("article-tags")]
        public override ArticleTagList Item { get; set; }
    }
}