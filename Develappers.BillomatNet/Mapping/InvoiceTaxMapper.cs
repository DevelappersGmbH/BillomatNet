using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using InvoiceTax = Develappers.BillomatNet.Types.InvoiceTax;

namespace Develappers.BillomatNet.Mapping
{
    internal class InvoiceTaxMapper : IMapper<Api.InvoiceTax, InvoiceTax>
    {
        public InvoiceTax ApiToDomain(Api.InvoiceTax value)
        {
            if (value == null)
            {
                return null;
            }

            return new InvoiceTax
            {
                Name = value.Name,
                Amount = float.Parse(value.Amount, CultureInfo.InvariantCulture),
                AmountGross = float.Parse(value.AmountGross, CultureInfo.InvariantCulture),
                AmountGrossPlain = float.Parse(value.AmountGrossPlain, CultureInfo.InvariantCulture),
                AmountGrossRounded = float.Parse(value.AmountGrossRounded, CultureInfo.InvariantCulture),
                AmountNet = float.Parse(value.AmountNet, CultureInfo.InvariantCulture),
                AmountNetPlain = float.Parse(value.AmountNetPlain, CultureInfo.InvariantCulture),
                AmountNetRounded = float.Parse(value.AmountNetRounded, CultureInfo.InvariantCulture),
                AmountPlain = float.Parse(value.AmountPlain, CultureInfo.InvariantCulture),
                AmountRounded = float.Parse(value.AmountRounded, CultureInfo.InvariantCulture),
                Rate = float.Parse(value.Rate, CultureInfo.InvariantCulture),
            };
        }

        public Api.InvoiceTax DomainToApi(InvoiceTax value)
        {
            throw new NotImplementedException();
        }

        public List<InvoiceTax> ApiToDomain(InvoiceTaxWrapper value)
        {
            return value?.List?.Select(ApiToDomain).ToList();
        }
    }
}
