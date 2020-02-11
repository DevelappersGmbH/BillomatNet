using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ClientTagCloudItemListWrapper : PagedListWrapper<ClientTagCloudItemList>
    {
        [JsonProperty("client-tags")]
        public override ClientTagCloudItemList Item { get; set; }
    }
}