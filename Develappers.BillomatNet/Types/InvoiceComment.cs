// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Develappers.BillomatNet.Types
{
    public class InvoiceComment
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Comment { get; set; }
        public CommentType ActionKey { get; set; }
        public bool Public { get; set; }
        public bool ByClient { get; set; }
        public int? UserId { get; set; }
        public int? EmailId { get; set; }
        public int? ClientId { get; set; }
        public int InvoiceId { get; set; }
    }
}
