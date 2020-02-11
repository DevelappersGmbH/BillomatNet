using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticleTagList : PagedList<ArticleTag>
    {
        [JsonProperty("article-tags")]
        [JsonConverter(typeof(CollectionConverter<ArticleTag>))]
        public override List<ArticleTag> List { get; set; }
    }
}