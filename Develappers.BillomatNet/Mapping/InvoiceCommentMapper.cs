// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using InvoiceComment = Develappers.BillomatNet.Types.InvoiceComment;

namespace Develappers.BillomatNet.Mapping
{
    internal class InvoiceCommentMapper : IMapper<Api.InvoiceComment, InvoiceComment>
    {
        public Types.PagedList<InvoiceComment> ApiToDomain(InvoiceCommentListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        internal Types.PagedList<InvoiceComment> ApiToDomain(InvoiceCommentList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<InvoiceComment>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public InvoiceComment ApiToDomain(InvoiceCommentWrapper value)
        {
            return ApiToDomain(value?.InvoiceComment);
        }

        public InvoiceComment ApiToDomain(Api.InvoiceComment value)
        {
            if (value == null)
            {
                return null;
            }

            return new InvoiceComment
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture),
                Comment = value.Comment,
                ActionKey = value.ActionKey.ToCommentType(),
                Public = value.Public != "0",
                ByClient = value.ByClient != "0",
                UserId = value.UserId.ToOptionalInt(),
                EmailId = value.EmailId.ToOptionalInt(),
                ClientId = value.ClientId.ToOptionalInt(),
                InvoiceId = int.Parse(value.InvoiceId, CultureInfo.InvariantCulture)
            };
        }

        public Api.InvoiceComment DomainToApi(InvoiceComment value)
        {
            if (value ==null)
            {
                return null;
            }
            return new Api.InvoiceComment
            {
                Id = value.Id.ToString(),
                Created = value.Created.ToApiDateTime(),
                Comment = value.Comment,
                ActionKey = value.ActionKey.ToApiValue(),
                Public = value.Public.ToString(),
                ByClient = value.ByClient.ToString(),
                UserId = value.UserId.ToString(),
                EmailId = value.EmailId.ToString(),
                ClientId = value.ClientId.ToString(),
                InvoiceId = value.InvoiceId.ToString()
            };
        }
    }
}
