using Newtonsoft.Json;

namespace Develappers.BillomatNet.Types
{
    public enum NumberRangeModeType
    {
        [JsonProperty("IGNORE_PREFIX")]
        IgnorePrefix,
        [JsonProperty("CONSIDER_PREFIX")]
        ConsiderPrefix
    }
}
