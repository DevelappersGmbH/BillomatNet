// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
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
    }
}
