using System;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using Tax = Develappers.BillomatNet.Types.Tax;

namespace Develappers.BillomatNet.Mapping
{
    internal class TaxMapper : IMapper<Api.Tax, Tax>
    {
        public Tax ApiToDomain(Api.Tax value)
        {
            if (value == null)
            {
                return null;
            }

            return new Tax
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                Created = Convert.ToDateTime(value.Created, CultureInfo.InvariantCulture),
                Updated = Convert.ToDateTime(value.Updated, CultureInfo.InvariantCulture),
                Name = value.Name,
                Rate = float.Parse(value.Rate),
                IsDefault = value.IsDefault.ToBoolean()
            };
        }

        public Api.Tax DomainToApi(Tax value)
        {
            if (value == null)
            {
                return null;
            }

            return new Api.Tax
            {
                Name = value.Name,
                Rate = value.Rate.ToString(CultureInfo.InvariantCulture),
                IsDefault = value.IsDefault.BoolToString()
            };
        }

        public Tax ApiToDomain(TaxWrapper value)
        {
            return ApiToDomain(value?.Tax);
        }

        public Types.PagedList<Tax> ApiToDomain(TaxListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        private Types.PagedList<Tax> ApiToDomain(TaxList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<Tax>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }
    }
}
