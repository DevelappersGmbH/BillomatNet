using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ClientTagCloudItemListWrapper : PagedListWrapper<ClientTagCloudItemList>
    {
        [JsonProperty("client-tags")]
        public override ClientTagCloudItemList Item { get; set; }
    }
}