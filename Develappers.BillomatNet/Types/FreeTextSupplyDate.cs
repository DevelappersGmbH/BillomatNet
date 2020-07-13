namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents the supply date as an text type.
    /// </summary>
    public class FreeTextSupplyDate : ISupplyDate
    {
        public string Text { get; set; }
    }
}