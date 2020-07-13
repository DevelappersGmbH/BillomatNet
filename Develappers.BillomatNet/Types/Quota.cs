namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents the quota.
    /// </summary>
    public class Quota
    {
        public QuotaType Entity { get; set; }
        public int Available { get; set; }
        public int Used { get; set; }
    }
}