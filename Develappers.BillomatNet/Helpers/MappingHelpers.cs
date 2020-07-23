using System;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Helpers
{
    internal static class MappingHelpers
    {
        /// <summary>
        /// Checks the PropertyType enum and returns if checked or not.
        /// </summary>
        /// <param name="type">The enum</param>
        /// <param name="value">The string</param>
        /// <returns>The object boolean</returns>
        public static object ParsePropertyValue(PropertyType type, string value)
        {
            if (type == PropertyType.Checkbox)
            {
                return value != "0";
            }

            return value;
        }

        public static string ParsePropertyValue(PropertyType type, object value)
        {
            if (type == PropertyType.Checkbox)
            {
                try
                {
                    var b = (bool)value;
                    if (b)
                    {
                        return "1";
                    }
                    return "0";
                }
                catch (InvalidCastException)
                {

                    return "0";
                }
            }
            return (string)value;
        }
    }
}