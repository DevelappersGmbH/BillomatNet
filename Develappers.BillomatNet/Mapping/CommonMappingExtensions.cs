// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Develappers.BillomatNet.Mapping
{
    internal static class CommonMappingExtensions
    {
        /// <summary>
        /// Converts a string to a nullable integer.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The nullable integer or null if not found</returns>
        internal static int? ToOptionalInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return value.ToInt();
        }

        /// <summary>
        /// Converts a string to a integer.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The integer.</returns>
        internal static int ToInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException();
            }

            return int.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a string to a nullable float.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The nullable float or null if not found</returns>
        internal static float? ToOptionalFloat(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return value.ToFloat();
        }

        /// <summary>
        /// Converts a string to a float.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The float.</returns>
        internal static float ToFloat(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException();
            }

            return float.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a string to a nullable DateTime.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The nullable DateTime or null if not found</returns>
        internal static DateTime? ToOptionalDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return value.ToDateTime();
        }

        /// <summary>
        /// Converts a string to a DateTime.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The DateTime.</returns>
        internal static DateTime ToDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException();
            }

            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a nullable DateTime to a string.
        /// </summary>
        /// <param name="value">The nullable DateTime which gets converted.</param>
        /// <returns>The string.</returns>
        internal static string ToApiDateTime(this DateTime? value)
        {
            return value?.ToString("o", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a DateTime to a string.
        /// </summary>
        /// <param name="value">The DateTime which gets converted.</param>
        /// <returns>The string.</returns>
        internal static string ToApiDateTime(this DateTime value)
        {
            return ((DateTime?)value).ToApiDateTime();
        }

        /// <summary>
        /// Converts a nullable DateTime to a string.
        /// </summary>
        /// <param name="value">The nullable DateTime which gets converted.</param>
        /// <returns>The string.</returns>
        internal static string ToApiDate(this DateTime? value)
        {
            return value?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a DateTime to a string.
        /// </summary>
        /// <param name="value">The DateTime which gets converted.</param>
        /// <returns>The string.</returns>
        internal static string ToApiDate(this DateTime value)
        {
            return ((DateTime?)value).ToApiDate();
        }

        /// <summary>
        /// Converts a string to a boolean.
        /// </summary>
        /// <param name="value">The string which gets converted.</param>
        /// <returns>The boolean.</returns>
        internal static bool ToBoolean(this string value)
        {
            return value == "1";
        }

        /// <summary>
        /// Converts a string to a list of strings.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The list of strings</returns>
        internal static List<string> ToStringList(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new List<string>();
            }

            return value.Split(',').Select(x => x.Trim()).ToList();
        }

        /// <summary>
        /// Treats empty stings as null.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The string.</returns>
        internal static string Sanitize(this string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }

        /// <summary>
        /// Converts a string to a list of integer.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The list of integer</returns>
        internal static List<int> ToIntList(this string value)
        {
            if (value == null)
            {
                return new List<int>();
            }

            return value.Split(',').Select(x => int.Parse(x.Trim())).ToList();
        }


    }
}
