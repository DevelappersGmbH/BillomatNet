// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using InvoiceDocument = Develappers.BillomatNet.Types.InvoiceDocument;
using InvoiceMail = Develappers.BillomatNet.Types.InvoiceMail;

namespace Develappers.BillomatNet.Mapping
{
    internal class InvoiceDocumentMapper : IMapper<Api.InvoiceDocument, InvoiceDocument>
    {
        public InvoiceDocument ApiToDomain(Api.InvoiceDocument value)
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
                FileContent = Convert.FromBase64String(value.Base64File)
            };
        }

        public Api.InvoiceDocument DomainToApi(InvoiceDocument value)
        {
            throw new NotImplementedException();
        }

        public InvoiceDocument ApiToDomain(InvoiceDocumentWrapper value)
        {
            return ApiToDomain(value?.Pdf);
        }


        public Api.InvoiceMail DomainToApi(InvoiceMail value)
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

        public Api.Attachment DomainToApi(Types.Attachment value)
        {
            return new Api.Attachment
            {
                FileName = value.FileName,
                MimeType = value.MimeType,
                Base64File = Convert.ToBase64String(value.FileContent)
            };
        }
    }
}
