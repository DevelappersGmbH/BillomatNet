using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Develappers.BillomatNet.Api
{
    internal class ClientTagList : PagedList<ClientTag>
    {
        [JsonProperty("client-tags")]
        [JsonConverter(typeof(CollectionConverter<ArticleTag>))]
        public override List<ClientTag> List { get; set; }
    }
}