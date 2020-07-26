// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    /// <summary>
    /// Base class for all integration tests.
    /// </summary>
    /// <typeparam name="T">The service to test.</typeparam>
    /// <remarks>
    /// This kind of tests establishes a real connection to Billomat. So be sure to what you do before you execute them. 
    /// </remarks>
    [Trait(Traits.Category, Traits.Categories.IntegrationTest)]
    public abstract class IntegrationTestBase<T> where T : ServiceBase
    {
        protected IntegrationTestBase(Func<Configuration, T> sutFactoryMethod)
        {
            Configuration = Helpers.GetTestConfiguration();
            _sut = new Lazy<T>(sutFactoryMethod.Invoke(Configuration));
        }

        private readonly Lazy<T> _sut;

        protected Configuration Configuration { get; }

        protected T SystemUnderTest => _sut.Value;
    }
}
