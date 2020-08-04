// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using InvoiceTag = Develappers.BillomatNet.Types.InvoiceTag;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet.Mapping
{
    internal class InvoiceTagMapper : IMapper<Api.InvoiceTag, InvoiceTag>
    {
        public Types.PagedList<InvoiceTag> ApiToDomain(InvoiceTagListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        internal Types.PagedList<InvoiceTag> ApiToDomain(InvoiceTagList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<InvoiceTag>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Types.PagedList<TagCloudItem> ApiToDomain(InvoiceTagCloudItemListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        public Types.PagedList<TagCloudItem> ApiToDomain(InvoiceTagCloudItemList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<TagCloudItem>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        public InvoiceTag ApiToDomain(InvoiceTagWrapper value)
        {
            return ApiToDomain(value?.InvoiceTag);
        }

        public InvoiceTag ApiToDomain(Api.InvoiceTag value)
        {
            if (value == null)
            {
                return null;
            }

            return new InvoiceTag
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                InvoiceId = int.Parse(value.InvoiceId, CultureInfo.InvariantCulture),
                Name = value.Name
            };
        }

        public Api.InvoiceTag DomainToApi(InvoiceTag value)
        {
            throw new NotImplementedException();
        }
    }
}
