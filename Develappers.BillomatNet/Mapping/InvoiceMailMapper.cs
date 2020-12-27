// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Develappers.BillomatNet.Api;
using Attachment = Develappers.BillomatNet.Types.Attachment;
using InvoiceMail = Develappers.BillomatNet.Types.InvoiceMail;

namespace Develappers.BillomatNet.Mapping
{
    internal class InvoiceMailMapper : IMapper<Api.InvoiceMail, InvoiceMail>
    {
        public InvoiceMail ApiToDomain(Api.InvoiceMail value)
        {
            throw new NotImplementedException();
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
                Bcc = value.Recipients.Bcc
            };

            var attachmentList = new AttachmentsWrapper
            {
                List = value.Attachments.Select(x => MappingExtensions.ToApi((Attachment) x)).ToList()
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
