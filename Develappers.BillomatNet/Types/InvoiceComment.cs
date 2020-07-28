using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Develappers.BillomatNet.Types
{
    public class InvoiceComment
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Comment { get; set; }
        public CommentType ActionKey { get; set; }
        public bool Public { get; set; }
        public bool ByClient { get; set; }
        public int UserId { get; set; }
        public int EmailId { get; set; }
        public int ClientId { get; set; }
        public int InvoiceId { get; set; }
    }
}
