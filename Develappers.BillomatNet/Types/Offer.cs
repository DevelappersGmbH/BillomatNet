// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Types
{
    public class Offer
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public int? ContactId { get; set; }

        public int ClientId { get; set; }

        public string OfferNumber { get; set; }

        public int? Number { get; set; }

        public string NumberPre { get; set; }

        public int NumberLength { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Address { get; set; }

        public OfferStatus Status { get; set; }

        public string Label { get; set; }

        public string Intro { get; set; }

        public string Note { get; set; }

        public float TotalGross { get; set; }

        public float TotalNet { get; set; }

        public string CurrencyCode { get; set; }

        public float Quote { get; set; }

        public NetGrossType NetGross { get; set; }

        public IReduction Reduction { get; set; }

        public float TotalGrossUnreduced { get; set; }

        public float TotalNetUnreduced { get; set; }

        public string CustomerPortalUrl { get; set; }

        public int? TemplateId { get; set; }
    }
}
