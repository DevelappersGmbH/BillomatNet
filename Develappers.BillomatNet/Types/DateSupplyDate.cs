using System;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents the sypply date for an invoice
    /// </summary>
    public class DateSupplyDate : ISupplyDate
    {
        public DateTime? Date { get; set; }
    }
}