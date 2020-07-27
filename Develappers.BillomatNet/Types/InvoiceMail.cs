using System.Collections.Generic;
using Develappers.BillomatNet.Api;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Types
{
    public class InvoiceMail
    {
        public string From { get; set; }
        public Recipients Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
