using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Api
{
    internal class TaxWrapper
    {
        [JsonProperty("tax")]
        public Tax Tax { get; set; }
    }
}
