// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Develappers.BillomatNet.Api;
using Unit = Develappers.BillomatNet.Types.Unit;

namespace Develappers.BillomatNet.Mapping
{
    internal class UnitMapper : IMapper<Api.Unit, Unit>
    {
        public Unit ApiToDomain(Api.Unit value)
        {
            if (value == null)
            {
                return null;
            }

            return new Unit
            {
                Id = int.Parse(value.Id),
                Created = value.Created,
                Updated = value.Updated,
                Name = value.Name
            };
        }

        public Api.Unit DomainToApi(Unit value)
        {
            return new Api.Unit
            {
                Name = value.Name
            };
        }

        public Types.PagedList<Unit> ApiToDomain(UnitListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public Types.PagedList<Unit> ApiToDomain(UnitList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<Unit>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Unit ApiToDomain(UnitWrapper value)
        {
            return ApiToDomain(value?.Unit);
        }
    }
}
