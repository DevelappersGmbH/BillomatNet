// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Develappers.BillomatNet.Api;
using ClientTag = Develappers.BillomatNet.Types.ClientTag;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet.Helpers
{
    internal static class ClientTagMappingExtension
    {
        internal static Types.PagedList<TagCloudItem> ToDomain(this ClientTagCloudItemListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static Types.PagedList<TagCloudItem> ToDomain(this ClientTagCloudItemList value)
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

        internal static Types.PagedList<ClientTag> ToDomain(this ClientTagListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static Types.PagedList<ClientTag> ToDomain(this ClientTagList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<ClientTag>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        internal static ClientTag ToDomain(this ClientTagWrapper value)
        {
            return value?.ClientTag.ToDomain();
        }

        private static ClientTag ToDomain(this Api.ClientTag value)
        {
            if (value == null)
            {
                return null;
            }

            return new ClientTag
            {
                Id = value.Id,
                ClientId = value.ClientId,
                Name = value.Name
            };
        }

        internal static Api.ClientTag ToApi(this ClientTag value)
        {
            if (value == null)
            {
                return null;
            }
            return new Api.ClientTag
            {
                Id = value.Id,
                ClientId = value.ClientId,
                Name = value.Name
            };
        }
    }
}
