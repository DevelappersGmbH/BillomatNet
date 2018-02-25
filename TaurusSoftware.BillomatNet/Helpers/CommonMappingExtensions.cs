using System.Collections.Generic;
using System.Linq;
using TagCloudItem = TaurusSoftware.BillomatNet.Types.TagCloudItem;

namespace TaurusSoftware.BillomatNet.Helpers
{
    internal static class CommonMappingExtensions
    {
        public static List<string> ToStringList(this string values)
        {
            if (values == null)
            {
                return new List<string>();
            }

            return values.Split(',').Select(x => x.Trim()).ToList();
        }

        public static List<int> ToIntList(this string values)
        {
            if (values == null)
            {
                return new List<int>();
            }

            return values.Split(',').Select(x => int.Parse(x.Trim())).ToList();
        }

        internal static TagCloudItem ToDomain(this Api.TagCloudItem value)
        {
            if (value == null)
            {
                return null;
            }

            return new TagCloudItem
            {
                Id = value.Id,
                Count = value.Count,
                Name = value.Name
            };
        }
       
    }
}
