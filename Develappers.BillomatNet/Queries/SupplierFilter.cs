// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Develappers.BillomatNet.Queries
{
    public class SupplierFilter
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
        public string CreditorIdentifier { get; set; }
        public string Note { get; set; }
        public string ClientNumber { get; set; }
        public List<int> IncomingIds { get; set; }
        public List<string> Tags { get; set; }
    }
}
