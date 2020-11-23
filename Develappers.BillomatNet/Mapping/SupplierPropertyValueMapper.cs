// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using Develappers.BillomatNet.Api;
using SupplierPropertyValue = Develappers.BillomatNet.Types.SupplierPropertyValue;

namespace Develappers.BillomatNet.Mapping
{
    internal class SupplierPropertyValueMapper
    {
        public List<SupplierPropertyValue> ApiToDomain(SupplierPropertyValuesWrapper value)
        {
            return value.List?.Select(ApiToDomain).ToList();
        }

        public SupplierPropertyValue ApiToDomain(Api.SupplierPropertyValue value)
        {
            if (value == null)
            {
                return null;
            }

            var type = value.Type.ToPropertyType();

            return new SupplierPropertyValue
            {
                Id = int.Parse(value.Id),
                SupplierId = int.Parse(value.SupplierId),
                SupplierPropertyId = int.Parse(value.SupplierPropertyId),
                Type = type,
                Name = value.Name,
                Value = MappingHelpers.ParsePropertyValue(type, value.Value)
            };
        }
    }
}
