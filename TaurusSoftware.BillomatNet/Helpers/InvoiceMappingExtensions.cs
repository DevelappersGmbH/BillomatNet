using System.Linq;
using TaurusSoftware.BillomatNet.Api;
using Invoice = TaurusSoftware.BillomatNet.Types.Invoice;


namespace TaurusSoftware.BillomatNet.Helpers
{
    internal static class InvoiceMappingExtensions
    {
        internal static PagedList<Invoice> ToDomain(this InvoiceListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static PagedList<Invoice> ToDomain(this InvoiceList value)
        {
            if (value == null)
            {
                return null;
            }

            return new PagedList<Invoice>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ToDomain).ToList()
            };
        }

        private static Invoice ToDomain(this Api.Invoice value)
        {
            if (value == null)
            {
                return null;
            }

            return new Invoice
            {
                Id = int.Parse(value.Id),
            };
        }
    }
}