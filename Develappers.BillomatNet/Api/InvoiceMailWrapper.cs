using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceMailWrapper
    {
        [JsonProperty("email")]
        public InvoiceMail InvoiceMail { get; set; }
    }
}
