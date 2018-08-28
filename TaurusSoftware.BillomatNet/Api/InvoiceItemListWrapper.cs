using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class InvoiceItemListWrapper : PagedListWrapper<InvoiceItemList>
    {
        [JsonProperty("invoice-items")]
        public override InvoiceItemList Item { get; set; }
    }
}