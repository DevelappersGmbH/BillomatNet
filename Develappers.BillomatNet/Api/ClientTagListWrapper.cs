using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ClientTagListWrapper : PagedListWrapper<ClientTagList>
    {
        [JsonProperty("client-tags")]
        public override ClientTagList Item { get; set; }
    }
}
