using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Types
{
    public class ExtraInvoiceModel
    {
        [JsonProperty("invoice")]
        public Invoice Invoice { get; set; }
    }
}
