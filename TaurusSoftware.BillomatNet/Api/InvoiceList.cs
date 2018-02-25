using System.Collections.Generic;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class InvoiceList : PagedList<Invoice>
    {
        [JsonProperty("invoice")]
        [JsonConverter(typeof(CollectionConverter<Invoice>))]
        public override List<Invoice> List { get; set; }
    }
}