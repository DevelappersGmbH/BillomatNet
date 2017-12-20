using System.Collections.Generic;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ClientList : PagedList<Client>{
        [JsonProperty("client")]
        [JsonConverter(typeof(CollectionConverter<Client>))]
        public override List<Client> List { get; set; }
    }
}