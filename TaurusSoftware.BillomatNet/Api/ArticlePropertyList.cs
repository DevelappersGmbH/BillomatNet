using System.Collections.Generic;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ArticlePropertyList : PagedList<ArticleProperty>
    {
        [JsonProperty("article-property-value")]
        [JsonConverter(typeof(CollectionConverter<ArticleProperty>))]
        public override List<ArticleProperty> List { get; set; }
    }
}