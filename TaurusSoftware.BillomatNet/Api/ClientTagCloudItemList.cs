using System.Collections.Generic;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ClientTagCloudItemList : PagedList<TagCloudItem>
    {
        [JsonProperty("client-tag")]
        [JsonConverter(typeof(CollectionConverter<TagCloudItem>))]
        public override List<TagCloudItem> List { get; set; }
    }
}