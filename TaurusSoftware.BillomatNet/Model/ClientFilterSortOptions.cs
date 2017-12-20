namespace TaurusSoftware.BillomatNet.Model
{
    public class ClientFilterSortOptions
    {
        public ClientFilterSortOptions()
        {
            Paging = new PagingSettings();
        }

        public ClientFilter Filter { get; set; }

        public ClientSortSettings Sort { get; set; }

        public PagingSettings Paging { get; }
    }
}