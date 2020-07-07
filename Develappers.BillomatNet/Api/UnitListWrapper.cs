using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class UnitListWrapper : PagedListWrapper<UnitList>
    {
        [JsonProperty("units")]
        public override UnitList Item { get; set; }
    }
}
