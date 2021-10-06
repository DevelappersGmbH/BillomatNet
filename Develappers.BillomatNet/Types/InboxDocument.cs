// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Develappers.BillomatNet.Types
{
    public class InboxDocument : Document
    {
        public int UserId { get; set; }

        public DateTime Updated { get; set; }

        public int PageCount { get; set; }

        public Dictionary<string, string> Metadata { get; set; }

        public InboxDocumentType DocumentType { get; set; }
    }
}
