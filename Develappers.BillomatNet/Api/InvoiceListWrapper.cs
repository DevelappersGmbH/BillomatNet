using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceListWrapper : PagedListWrapper<InvoiceList>
    {
        
        [JsonProperty("invoices")]
        public override InvoiceList Item { get; set; }
    }
}