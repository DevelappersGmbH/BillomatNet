using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ClientTagCloudItemList : PagedList<TagCloudItem>
    {
        [JsonProperty("client-tag")]
        [JsonConverter(typeof(CollectionConverter<TagCloudItem>))]
        public override List<TagCloudItem> List { get; set; }
    }
}