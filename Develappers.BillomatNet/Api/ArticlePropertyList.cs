using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticlePropertyList : PagedList<ArticleProperty>
    {
        [JsonProperty("article-property-value")]
        [JsonConverter(typeof(CollectionConverter<ArticleProperty>))]
        public override List<ArticleProperty> List { get; set; }
    }
}