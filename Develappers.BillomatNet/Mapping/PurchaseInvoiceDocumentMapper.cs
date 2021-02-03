// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Mapping
{
    internal class PurchaseInvoiceDocumentMapper : IMapper<IncomingDocument, PurchaseInvoiceDocument>
    {
        public PurchaseInvoiceDocument ApiToDomain(IncomingDocument value)
        {
            if (value == null)
            {
                return null;
            }

            return new PurchaseInvoiceDocument
            {
                Id = value.Id.ToInt(),
                Created = value.Created.ToDateTime(),
                FileName = value.FileName,
                FileSize = value.FileSize.ToInt(),
                IncomingId = value.IncomingId.ToInt(),
                MimeType = value.MimeType,
                FileContent = MappingHelpers.ToByteArray(value.Base64File)
            };
        }

        public IncomingDocument DomainToApi(PurchaseInvoiceDocument value)
        {
            throw new NotImplementedException();
        }

        public PurchaseInvoiceDocument ApiToDomain(IncomingDocumentWrapper value)
        {
            return ApiToDomain(value?.Pdf);
        }
    }
}
