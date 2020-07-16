using System.Collections.Generic;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents your account.
    /// </summary>
    public class Account : Client
    {
        public string Plan { get; set; }

        public List<Quota> Quotas { get; set; }
    }
}
