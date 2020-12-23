﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class OfferList : PagedList<Offer>
    {
        [JsonProperty("offer")]
        [JsonConverter(typeof(CollectionConverter<Offer>))]
        public override List<Offer> List { get; set; }
    }
}
