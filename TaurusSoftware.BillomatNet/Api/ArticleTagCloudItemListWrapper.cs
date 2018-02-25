using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ArticleTagCloudItemListWrapper : PagedListWrapper<ArticleTagCloudItemList>
    {
        [JsonProperty("article-tags")]
        public override ArticleTagCloudItemList Item { get; set; }
    }
}