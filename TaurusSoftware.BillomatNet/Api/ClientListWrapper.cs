using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ClientListWrapper : PagedListWrapper<ClientList>
    {
        [JsonProperty("clients")]
        public override ClientList Item { get; set; }
    }
}