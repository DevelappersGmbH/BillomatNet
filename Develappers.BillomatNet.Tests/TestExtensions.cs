// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Develappers.BillomatNet.Tests
{
    public static class TestExtensions
    {
        public static void AssertWith<TExpected, TActual>(this IEnumerable<TActual> actual, IEnumerable<TExpected> expected, Action<TExpected, TActual> inspector)
        {
            Assert.Collection(actual, expected.Select(e => (Action<TActual>)(a => inspector(e, a))).ToArray());
        }
    }
}
