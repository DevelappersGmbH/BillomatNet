using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceMail
    {
        public string From { get; set; }
        public RecipientsWrapper Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public AttachmentsWrapper Attachments { get; set; }
    }
}
