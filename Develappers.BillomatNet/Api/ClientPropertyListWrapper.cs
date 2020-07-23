using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ClientPropertyListWrapper : PagedListWrapper<ClientPropertyList>
    {
        [JsonProperty("client-property-values")]
        public override ClientPropertyList Item { get; set; }
    }
}
