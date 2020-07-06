using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Develappers.BillomatNet.Api
{
    internal class TaxListWrapper : PagedListWrapper<TaxList>
    {
        [JsonProperty("taxes")]
        public override TaxList Item { get; set; }

    }
    internal class TaxList : PagedList<Tax>
    {
        [JsonProperty("tax")]
        [JsonConverter(typeof(CollectionConverter<Tax>))]
        public override List<Tax> List { get; set; }
    }
}