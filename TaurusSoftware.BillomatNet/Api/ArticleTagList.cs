using System.Collections.Generic;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ArticleTagList : PagedList<ArticleTag>
    {
        [JsonProperty("article-tags")]
        [JsonConverter(typeof(CollectionConverter<ArticleTag>))]
        public override List<ArticleTag> List { get; set; }
    }
}