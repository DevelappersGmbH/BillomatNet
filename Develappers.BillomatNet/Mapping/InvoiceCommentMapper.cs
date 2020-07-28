// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using InvoiceComment = Develappers.BillomatNet.Types.InvoiceComment;

namespace Develappers.BillomatNet.Mapping
{
    internal class InvoiceCommentMapper : IMapper<Api.InvoiceComment, InvoiceComment>
    {
        private readonly InvoiceCommentMapper _commentMapper = new InvoiceCommentMapper();

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

        public InvoiceComment ApiToDomain(Api.InvoiceComment value)
        {
            if (value == null)
            {
                return null;
            }

            return new InvoiceComment
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                Created = value.List.Created,
                Comment = value.Comment,
                ActionKey = value.ActionKey.ToCommentType(),
                Public = value.Public != "0",
                ByClient = value.ByClient != "0",
                UserId = int.Parse(value.UserId, CultureInfo.InvariantCulture),
                EmailId = int.Parse(value.EmailId, CultureInfo.InvariantCulture),
                ClientId = int.Parse(value.ClientId, CultureInfo.InvariantCulture),
                InvoiceId = int.Parse(value.InvoiceId, CultureInfo.InvariantCulture)
            };
        }
    }
}
