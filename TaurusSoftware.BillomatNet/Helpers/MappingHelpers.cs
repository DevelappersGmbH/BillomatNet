using TaurusSoftware.BillomatNet.Types;

namespace TaurusSoftware.BillomatNet.Helpers
{
    internal static class MappingHelpers
    {
        public static PropertyType ParsePropertyType(string value)
        {
            switch (value)
            {
                case "CHECKBOX":
                    return PropertyType.Checkbox;
                case "TEXTAREA":
                    return PropertyType.Textarea;
                default:
                    return PropertyType.Textfield;
            }
        }

        public static object ParsePropertyValue(PropertyType type, string value)
        {
            if (type == PropertyType.Checkbox)
            {
                return value != "0";
            }

            return value;
        }
    }
}