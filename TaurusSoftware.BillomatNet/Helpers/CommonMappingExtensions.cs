using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TagCloudItem = TaurusSoftware.BillomatNet.Types.TagCloudItem;

namespace TaurusSoftware.BillomatNet.Helpers
{
    internal static class CommonMappingExtensions
    {
        public static int? ToOptionalInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return int.Parse(value, CultureInfo.InvariantCulture);
        }

        public static float? ToOptionalFloat(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return float.Parse(value, CultureInfo.InvariantCulture);
        }

        public static DateTime? ToOptionalDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

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
