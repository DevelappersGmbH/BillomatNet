// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Types
{
    public class Template
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Type { get; set; }
        public string TemplateType { get; set; }
        public string Name { get; set; }
        public bool IsBackgroundAvailable { get; set; }
        public bool IsDefault { get; set; }
    }
}


