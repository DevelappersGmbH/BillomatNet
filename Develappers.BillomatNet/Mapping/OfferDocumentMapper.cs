// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using Develappers.BillomatNet.Api;
using OfferDocument = Develappers.BillomatNet.Types.OfferDocument;

namespace Develappers.BillomatNet.Mapping
{
    internal class OfferDocumentMapper : IMapper<Api.OfferDocument, OfferDocument>
    {
        public OfferDocument ApiToDomain(Api.OfferDocument value)
        {
            if (value == null)
            {
                return null;
            }

            return new OfferDocument
            {
                Id = int.Parse(value.Id),
                Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture),
                FileName = value.FileName,
                FileSize = int.Parse(value.FileSize, CultureInfo.InvariantCulture),
                OfferId = int.Parse(value.OfferId, CultureInfo.InvariantCulture),
                MimeType = value.MimeType,
                FileContent = MappingHelpers.ToByteArray(value.Base64File)
            };
        }

        public Api.OfferDocument DomainToApi(OfferDocument value)
        {
            throw new NotImplementedException();
        }

        public OfferDocument ApiToDomain(OfferDocumentWrapper value)
        {
            return ApiToDomain(value?.Pdf);
        }
    }
}
