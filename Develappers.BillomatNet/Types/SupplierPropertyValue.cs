// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents a supplier property value.
    /// </summary>
    public class SupplierPropertyValue
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int SupplierPropertyId { get; set; }
        public PropertyType Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
