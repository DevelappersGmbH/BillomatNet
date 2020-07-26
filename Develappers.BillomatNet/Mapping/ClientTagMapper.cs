// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using ClientTag = Develappers.BillomatNet.Types.ClientTag;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet.Mapping
{
    internal class ClientTagMapper : IMapper<Api.ClientTag, ClientTag>
    {
        public ClientTag ApiToDomain(Api.ClientTag value)
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

        public Api.ClientTag DomainToApi(ClientTag value)
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

        public ClientTag ApiToDomain(ClientTagWrapper value)
        {
            return ApiToDomain(value?.ClientTag);
        }

        public Types.PagedList<ClientTag> ApiToDomain(ClientTagList value)
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
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Types.PagedList<ClientTag> ApiToDomain(ClientTagListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public Types.PagedList<TagCloudItem> ApiToDomain(ClientTagCloudItemListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        public Types.PagedList<TagCloudItem> ApiToDomain(ClientTagCloudItemList value)
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
    }
}
