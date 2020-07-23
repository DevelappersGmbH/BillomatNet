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
    }
}