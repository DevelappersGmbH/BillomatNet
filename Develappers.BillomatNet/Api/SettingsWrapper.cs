using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Api
{
    internal class SettingsWrapper
    {
        [JsonProperty("settings")]
        public Settings Settings { get; set; }
    }
}
