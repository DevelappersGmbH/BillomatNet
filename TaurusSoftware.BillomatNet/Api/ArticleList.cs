using System.Collections.Generic;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ArticleList : PagedList<Article>
    {
        [JsonProperty("article")]
        [JsonConverter(typeof(CollectionConverter<Article>))]
        public override List<Article> List { get; set; }
    }
}