// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents the unit.
    /// </summary>
    public class Unit
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; }
    }
}
