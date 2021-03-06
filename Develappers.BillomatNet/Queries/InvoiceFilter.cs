﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    /// <summary>
    /// Represents the filter for the invoice.
    /// </summary>
    public class InvoiceFilter
    {
        public int? ClientId { get; set; }
        public int? ContactId { get; set; }
        public string InvoiceNumber { get; set; }
        public List<InvoiceStatus> Status { get; set; }
        public List<PaymentType> PaymentType { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Label { get; set; }
        public string Intro { get; set; }
        public string Note { get; set; }
        public List<string> Tags { get; set; }
        public int? ArticleId { get; set; }
    }
}
