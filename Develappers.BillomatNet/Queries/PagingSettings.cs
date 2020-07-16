namespace Develappers.BillomatNet.Queries
{
    /// <summary>
    /// Represents the page settings.
    /// </summary>
    public class PagingSettings
    {
        public PagingSettings()
        {
            Page = 1;
            ItemsPerPage = 100;
        }

        /// <summary>
        /// number of the requested page (1 based)
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// items per page
        /// </summary>
        public int ItemsPerPage { get; set; }
    }
}