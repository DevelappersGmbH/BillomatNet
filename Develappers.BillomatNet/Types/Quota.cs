﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents the quota.
    /// </summary>
    public class Quota
    {
        public QuotaType Entity { get; set; }
        public long Available { get; set; }
        public long Used { get; set; }
    }
}
