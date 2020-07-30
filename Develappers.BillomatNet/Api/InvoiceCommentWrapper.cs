using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceCommentWrapper
    {
        [JsonProperty("invoice-comment")]
        public InvoiceComment InvoiceComment { get; set; }
    }
}
