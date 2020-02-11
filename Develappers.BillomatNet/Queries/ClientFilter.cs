using System.Collections.Generic;

namespace Develappers.BillomatNet.Queries
{
    public class ClientFilter
    {
        public string Name { get; set; }
        public string ClientNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
        public string Note { get; set; }
        public List<int> InvoiceIds { get; set; }
        public List<string> Tags { get; set; }
    }
}