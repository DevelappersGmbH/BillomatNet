﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class TemplateWrapper
    {
        [JsonProperty("template")]
        public Template Template { get; set; }
    }
}
