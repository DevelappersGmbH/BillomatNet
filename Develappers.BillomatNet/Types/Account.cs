using System.Collections.Generic;

namespace Develappers.BillomatNet.Types
{
    public class Account : Client
    {
        public string Plan { get; set; }

        public List<Quota> Quotas { get; set; }
    }
}
