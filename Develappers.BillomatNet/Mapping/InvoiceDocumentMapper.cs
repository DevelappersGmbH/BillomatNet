// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Develappers.BillomatNet.Api;
using InvoiceDocument = Develappers.BillomatNet.Types.InvoiceDocument;

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
                Id = value.Id.ToInt(),
                Created = value.Created.ToDateTime(),
                FileName = value.FileName,
                FileSize = value.FileSize.ToInt(),
                InvoiceId = value.InvoiceId.ToInt(),
                MimeType = value.MimeType,
                FileContent = MappingHelpers.ToByteArray(value.Base64File)
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
    }
}
