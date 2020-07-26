using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using ClientProperty = Develappers.BillomatNet.Types.ClientProperty;

namespace Develappers.BillomatNet.Mapping
{
    internal class ClientPropertyMapper : IMapper<Api.ClientProperty, ClientProperty>
    {
        public ClientProperty ApiToDomain(Api.ClientProperty value)
        {
            if (value == null)
            {
                return null;
            }

            var type = value.Type.ToPropertyType();
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

        public Api.ClientProperty DomainToApi(ClientProperty value)
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
                Type = value.Type.ToApiValue(),
                Name = value.Name,
                Value = MappingHelpers.ParsePropertyValue(value.Type, value.Value)
            };
        }

        public ClientProperty ApiToDomain(ClientPropertyWrapper value)
        {
            return ApiToDomain(value?.ClientProperty);
        }

        public Types.PagedList<ClientProperty> ApiToDomain(ClientPropertyList value)
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
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Types.PagedList<ClientProperty> ApiToDomain(ClientPropertyListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }
    }
}
