// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Develappers.BillomatNet.Tests
{
    public static class AssertionExtensions
    {
        public static AndConstraint<GenericCollectionAssertions<T>> ContainItemsInOrderUsingComparer<T>(
            this GenericCollectionAssertions<T> assertions,
            IEnumerable<T> expected,
            IEqualityComparer<T> comparer,
            string because = "",
            params object[] becauseArgs)
        {
            if (assertions?.Subject == null)
            {
                Execute.Assertion.BecauseOf(because, becauseArgs).FailWith(
                    "Expected {context:collection} to contain {0}{reason}, but found <null>.",
                    (object)expected);
                return new AndConstraint<GenericCollectionAssertions<T>>(assertions);
            }

            if (expected == null)
            {
                Execute.Assertion.BecauseOf(because, becauseArgs).FailWith(
                    "Expected parameter to contain items, but found <null>.");
                return new AndConstraint<GenericCollectionAssertions<T>>(assertions);
            }

            var expectedList = new List<T>(expected);
            var actualList = new List<T>(assertions.Subject);

            for (var i = 0; i < actualList.Count; i++)
            {
                var expectedItem = expectedList[i];
                var actualItem = actualList[i];
                if (!comparer.Equals(expectedItem, actualItem))
                {
                    Execute.Assertion.BecauseOf(because, becauseArgs).FailWith(
                        "Expected {context:collection} to contain equal items at index {0}, but found {1} instead of {2}.", i, actualItem, expectedItem);
                }
            }

            return new AndConstraint<GenericCollectionAssertions<T>>(assertions);
        }

        public static AndConstraint<ObjectAssertions> BeEquivalentUsingComparerTo<T>(
            this ObjectAssertions assertions,
            T expected,
            IEqualityComparer<T> comparer,
            string because = "",
            params object[] becauseArgs)
        {
            if (assertions?.Subject == null)
            {
                Execute.Assertion.BecauseOf(because, becauseArgs).FailWith(
                    "Expected {context} to contain {0}{reason}, but found <null>.",
                    (object)expected);
                return new AndConstraint<ObjectAssertions>(assertions);
            }

            if (expected == null)
            {
                Execute.Assertion.BecauseOf(because, becauseArgs).FailWith(
                    "Expected parameter to contain object, but found <null>.");
                return new AndConstraint<ObjectAssertions>(assertions);
            }

            if (!comparer.Equals(expected, (T)assertions.Subject))
            {
                Execute.Assertion.BecauseOf(because, becauseArgs).FailWith(
                    "Expected {context} to be equal but found {1} instead of {0}.", expected, assertions.Subject);
            }
            return new AndConstraint<ObjectAssertions>(assertions);
        }
    }
}
