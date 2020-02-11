using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticleList : PagedList<Article>
    {
        [JsonProperty("article")]
        [JsonConverter(typeof(CollectionConverter<Article>))]
        public override List<Article> List { get; set; }
    }
}