using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ClientList : PagedList<Client>{
        [JsonProperty("client")]
        [JsonConverter(typeof(CollectionConverter<Client>))]
        public override List<Client> List { get; set; }
    }
}