using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using Unit = Develappers.BillomatNet.Types.Unit;
using System.Security.Cryptography.X509Certificates;
using System.Security;

namespace Develappers.BillomatNet.Helpers
{
    internal static class UnitMappingExtensions
    {
        internal static Unit ToDomain(this UnitWrapper value)
        {
            return value?.Unit.ToDomain();
        }

        private static Unit ToDomain(this Api.Unit value)
        {
            if (value == null)
            {
                return null;
            }

            return new Unit
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                Created = Convert.ToDateTime(value.Created),
                Updated = Convert.ToDateTime(value.Updated),
                Name = value.Name
            };
        }
    }
}
