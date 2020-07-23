namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents a client property.
    /// </summary>
    public class ClientProperty
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ClientPropertyId { get; set; }
        public PropertyType Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
