using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticleTagCloudItemList : PagedList<TagCloudItem>
    {
        [JsonProperty("article-tag")]
        [JsonConverter(typeof(CollectionConverter<TagCloudItem>))]
        public override List<TagCloudItem> List { get; set; }
    }
}