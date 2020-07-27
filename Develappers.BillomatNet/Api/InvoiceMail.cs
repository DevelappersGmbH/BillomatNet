using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceMail
    {
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("recipients")]
        public Recipients Recipients { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("attachments")]
        public AttachmentsWrapper Attachments { get; set; }
    }
}
