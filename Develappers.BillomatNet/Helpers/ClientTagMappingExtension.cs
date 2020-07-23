using System.Linq;
using Develappers.BillomatNet.Api;
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
                List = value.List?.Select(x => CommonMappingExtensions.ToDomain(x)).ToList()
            };
        }
    }
}