using System.Collections.Generic;

namespace TaurusSoftware.BillomatNet.Model
{
    public class Account : Client
    {
        public string Plan { get; set; }

        public List<Quota> Quotas { get; set; }
    }
}
