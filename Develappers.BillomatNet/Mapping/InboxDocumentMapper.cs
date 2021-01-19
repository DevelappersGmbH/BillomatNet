// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Develappers.BillomatNet.Api;
using InboxDocument = Develappers.BillomatNet.Types.InboxDocument;

namespace Develappers.BillomatNet.Mapping
{
    internal class InboxDocumentMapper : IMapper<Api.InboxDocument, InboxDocument>
    {
        public InboxDocument ApiToDomain(Api.InboxDocument value)
        {
            if (value == null)
            {
                return null;
            }

            return new InboxDocument
            {
                Id = value.Id.ToInt(),
                FileSize = value.FileSize.ToInt(),
                FileName = value.FileName.Sanitize(),
                MimeType = value.MimeType.Sanitize(),
                UserId = value.UserId.ToInt(),
                Created = value.Created.ToDateTime(),
                PageCount = value.PageCount.ToInt(),
                Updated = value.Updated.ToDateTime(),
                FileContent = Convert.FromBase64String(value.Base64File),
                Metadata = value.Metadata?.Data?.ToDictionary()
            };
        }

        public InboxDocument ApiToDomain(InboxDocumentWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public Api.InboxDocument DomainToApi(InboxDocument value)
        {
            throw new System.NotImplementedException();
        }
    }
}
