using System.Linq;
using Develappers.BillomatNet.Api;
using ClientProperty = Develappers.BillomatNet.Types.ClientProperty;

namespace Develappers.BillomatNet.Helpers
{
    internal static class ClientPropertyMappingExtensions
    {
        internal static Types.PagedList<ClientProperty> ToDomain(this ClientPropertyListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static Types.PagedList<ClientProperty> ToDomain(this ClientPropertyList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<ClientProperty>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => ToDomain((Api.ClientProperty) x)).ToList()
            };
        }

        internal static ClientProperty ToDomain(this ClientPropertyWrapper value)
        {
            return value?.ClientProperty.ToDomain();
        }

        private static ClientProperty ToDomain(this Api.ClientProperty value)
        {
            if (value == null)
            {
                return null;
            }

            var type = MappingHelpers.ParsePropertyType(value.Type);
            return new ClientProperty
            {
                Id = value.Id,
                ClientPropertyId = value.ClientPropertyId,
                Type = type,
                ClientId = value.ClientId,
                Name = value.Name,
                Value = MappingHelpers.ParsePropertyValue(type, value.Value)
            };
        }

        internal static Api.ClientProperty ToApi(this ClientProperty value)
        {
            if (value == null)
            {
                return null;
            }
            return new Api.ClientProperty
            {
                Id = value.Id,
                ClientId = value.ClientId,
                ClientPropertyId = value.ClientPropertyId,
                Type = MappingHelpers.PropertyTypeToString(value.Type),
                Name = value.Name,
                Value = MappingHelpers.ParsePropertyValue(value.Type, value.Value)
            };
        }

    }
}