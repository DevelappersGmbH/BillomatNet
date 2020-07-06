using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using Tax = Develappers.BillomatNet.Types.Tax;
using System.Security.Cryptography.X509Certificates;
using System.Security;


namespace Develappers.BillomatNet.Helpers
{
    internal static class TaxMappeingExtensions
    {
        internal static Tax ToDomain(this TaxWrapper value)
        {
            return value?.Tax.ToDomain();
        }

        private static Tax ToDomain(this Api.Tax value)
        {
            if (value == null)
            {
                return null;
            }

            return new Tax
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                Created = Convert.ToDateTime(value.Created),
                Updated = Convert.ToDateTime(value.Updated),
                Name = value.Name,
                Rate = float.Parse(value.Rate),
                IsDefault = Convert.ToBoolean(value.IsDefault)
            };
        }
    }
}
