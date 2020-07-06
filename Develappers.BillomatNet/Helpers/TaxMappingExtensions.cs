using System;
using System.Globalization;
using Develappers.BillomatNet.Api;
using Tax = Develappers.BillomatNet.Types.Tax;


namespace Develappers.BillomatNet.Helpers
{
    internal static class TaxMappingExtensions
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
                IsDefault = value.IsDefault.ToBoolean()
            };
        }
    }
}