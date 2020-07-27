using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Recipients
    {
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("cc")]
        public string Cc { get; set; }
        [JsonProperty("bc")]
        public string Bc { get; set; }
    }
}
