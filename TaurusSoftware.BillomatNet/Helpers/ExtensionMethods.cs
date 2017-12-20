using System.Collections.Generic;
using System.Linq;

namespace TaurusSoftware.BillomatNet.Helpers
{
    internal static class ExtensionMethods
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
    }
}
