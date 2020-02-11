using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceList : PagedList<Invoice>
    {
        [JsonProperty("invoice")]
        [JsonConverter(typeof(CollectionConverter<Invoice>))]
        public override List<Invoice> List { get; set; }
    }
}