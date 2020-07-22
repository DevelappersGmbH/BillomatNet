namespace Develappers.BillomatNet.Queries
{
    /// <summary>
    /// Represents the filter for the article property.
    /// </summary>
    public class ClientPropertyFilter
    {
        public int? ClientId { get; set; }
        public int? ClientPropertyId { get; set; }
        public object Value { get; set; }
    }
}
