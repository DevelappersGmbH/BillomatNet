// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    public class OfferFilter
    {
        public int? ClientId { get; set; }
        public int? ContactId { get; set; }
        public string OfferNumber { get; set; }
        public List<OfferStatus> Status { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Label { get; set; }
        public string Intro { get; set; }
        public string Note { get; set; }
        public List<string> Tags { get; set; }
        public int? ArticleId { get; set; }
    }
}
