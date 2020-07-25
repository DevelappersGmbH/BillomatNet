// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents your account.
    /// </summary>
    public class Account : Client
    {
        public string Plan { get; set; }

        public List<Quota> Quotas { get; set; }
    }
}
