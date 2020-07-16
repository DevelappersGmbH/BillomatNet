using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class TaxListWrapper : PagedListWrapper<TaxList>
    {
        [JsonProperty("taxes")]
        public override TaxList Item { get; set; }

    }
}