using System;
using System.Linq;
using TaurusSoftware.BillomatNet.Api;
using Invoice = TaurusSoftware.BillomatNet.Types.Invoice;
using InvoiceDocument = TaurusSoftware.BillomatNet.Types.InvoiceDocument;

namespace TaurusSoftware.BillomatNet.Helpers
{
    internal static class InvoiceMappingExtensions
    {
        internal static InvoiceDocument ToDomain(this InvoiceDocumentWrapper value)
        {
            return value?.Pdf.ToDomain();
        }

        private static InvoiceDocument ToDomain(this Api.InvoiceDocument value)
        {
            if (value == null)
            {
                return null;
            }

            return new InvoiceDocument
            {
                Id = int.Parse(value.Id),
                Created = DateTime.Parse(value.Created),
                FileName = value.FileName,
                FileSize = int.Parse(value.FileSize),
                InvoiceId = int.Parse(value.InvoiceId),
                MimeType = value.MimeType,
                Bytes = Convert.FromBase64String(value.Base64File)
            };
        }


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