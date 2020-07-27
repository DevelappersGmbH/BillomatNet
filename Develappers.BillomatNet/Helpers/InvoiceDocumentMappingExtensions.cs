using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Develappers.BillomatNet.Api;
using InvoiceDocument = Develappers.BillomatNet.Types.InvoiceDocument;
using InvoiceMail = Develappers.BillomatNet.Types.InvoiceMail;

namespace Develappers.BillomatNet.Helpers
{
    internal static class InvoiceDocumentMappingExtensions
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
                Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture),
                FileName = value.FileName,
                FileSize = int.Parse(value.FileSize, CultureInfo.InvariantCulture),
                InvoiceId = int.Parse(value.InvoiceId, CultureInfo.InvariantCulture),
                MimeType = value.MimeType,
                Bytes = Convert.FromBase64String(value.Base64File)
            };
        }


        internal static Api.InvoiceMail ToApi(this InvoiceMail value)
        {
            if (value == null)
            {
                return null;
            }

            var recipients = new Api.Recipients
            {
                To = value.Recipients.To,
                Cc = value.Recipients.Cc,
                Bc = value.Recipients.Bc
            };

            var attachmentList = new AttachmentsWrapper
            {
                List = value.Attachments.Select(x => x.ToApi()).ToList()
            };

            return new Api.InvoiceMail
            {
                From = value.From,
                Recipients = recipients,
                Subject = value.Subject,
                Body = value.Body,
                Attachments = attachmentList
            };
        }

        internal static Api.Attachment ToApi(this Types.Attachment value)
        {
            return new Api.Attachment
            {
                Filename = value.Filename,
                Mimetype = value.Mimetype,
                Base64File = value.Base64File
            };
        }
    }
}
