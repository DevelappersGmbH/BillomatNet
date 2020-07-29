// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Types
{
    public abstract class File
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] FileContent { get; set; }
    }
}
