using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ClientListWrapper : PagedListWrapper<ClientList>
    {
        [JsonProperty("clients")]
        public override ClientList Item { get; set; }
    }
}