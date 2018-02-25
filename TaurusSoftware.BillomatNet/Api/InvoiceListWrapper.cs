using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class InvoiceListWrapper : PagedListWrapper<InvoiceList>
    {
        
        [JsonProperty("invoices")]
        public override InvoiceList Item { get; set; }
    }
}