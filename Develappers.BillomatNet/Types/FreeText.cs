// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Types
{
    public class FreeText
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public FreeTextType FreeTextType { get; set; }

        public string Title { get; set; }

        public string Intro { get; set; }

        public string Note { get; set; }

        public int IsDefault { get; set; }
    }
}
