using System.Collections.Generic;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ArticleTagCloudItemList : PagedList<TagCloudItem>
    {
        [JsonProperty("article-tag")]
        [JsonConverter(typeof(CollectionConverter<TagCloudItem>))]
        public override List<TagCloudItem> List { get; set; }
    }
}