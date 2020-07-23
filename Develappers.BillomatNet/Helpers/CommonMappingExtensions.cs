using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet.Helpers
{
    internal static class CommonMappingExtensions
    {
        /// <summary>
        /// Converts a string to a nullable integer.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The nullable integer or null if not found</returns>
        public static int? ToOptionalInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return int.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a string to a nullable float.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The nullable float or null if not found</returns>
        public static float? ToOptionalFloat(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return float.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a string to a nullable DateTime.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The nullable DateTime or null if not found</returns>
        public static DateTime? ToOptionalDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a nullable DateTime to a string.
        /// </summary>
        /// <param name="value">The nullable DateTime which gets converted.</param>
        /// <returns>The string.</returns>
        public static string ToApiDate (this DateTime? value)
        {
            return value?.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Converts a DateTime to a string.
        /// </summary>
        /// <param name="value">The DateTime which gets converted.</param>
        /// <returns>The string.</returns>
        public static string ToApiDate(this DateTime value)
        {
            return ((DateTime?)value).ToApiDate();
        }

        /// <summary>
        /// Converts a string to a boolean.
        /// </summary>
        /// <param name="value">The string which gets converted.</param>
        /// <returns>The boolean.</returns>
        public static bool ToBoolean(this string value)
        {
            return value == "1";
        }

        /// <summary>
        /// Converts a string to a list of strings.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The the list of strings</returns>
        public static List<string> ToStringList(this string value)
        {
            if (value == null)
            {
                return new List<string>();
            }

            return value.Split(',').Select(x => x.Trim()).ToList();
        }

        /// <summary>
        /// Converts a string to a list of integer.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The the list of integer</returns>
        public static List<int> ToIntList(this string value)
        {
            if (value == null)
            {
                return new List<int>();
            }

            return value.Split(',').Select(x => int.Parse(x.Trim())).ToList();
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

        internal static string ToInvariantString(this float? value)
        {
            return value?.ToString(CultureInfo.InvariantCulture);
        }
    }
}
