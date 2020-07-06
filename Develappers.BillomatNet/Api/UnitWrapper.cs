using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Api
{
    internal class UnitWrapper
    {
        [JsonProperty("unit")]
        public Unit Unit { get; set; }
    }
}
