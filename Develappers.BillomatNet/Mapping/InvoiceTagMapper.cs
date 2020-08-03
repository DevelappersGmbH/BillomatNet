// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using Develappers.BillomatNet.Api;
using InvoiceTag = Develappers.BillomatNet.Types.InvoiceTag;

namespace Develappers.BillomatNet.Mapping
{
    internal class InvoiceTagMapper : IMapper<Api.InvoiceTag, InvoiceTag>
    {
        public InvoiceTag ApiToDomain(InvoiceTagWrapper value)
        {
            return ApiToDomain(value?.InvoiceTag);
        }

        public InvoiceTag ApiToDomain(Api.InvoiceTag value)
        {
            if (value == null)
            {
                return null;
            }

            return new InvoiceTag
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                InvoiceId = int.Parse(value.InvoiceId, CultureInfo.InvariantCulture),
                Name = value.Name
            };
        }

        public Api.InvoiceTag DomainToApi(InvoiceTag value)
        {
            throw new NotImplementedException();
        }
    }
}
