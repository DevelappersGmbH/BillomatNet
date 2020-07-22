using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Develappers.BillomatNet.Api
{
    internal class ClientPropertyList : PagedList<ClientProperty>
    {
        [JsonProperty("client-property-value")]
        [JsonConverter(typeof(CollectionConverter<ClientProperty>))]
        public override List<ClientProperty> List { get; set; }
    }
}
