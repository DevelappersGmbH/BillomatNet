// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [Trait(Traits.Category, Traits.Categories.IntegrationTest)]
    public abstract class IntegrationTestBase<T> where T : ServiceBase
    {
        private readonly Func<Configuration, T> _sutFactoryMethod;

        protected IntegrationTestBase(Func<Configuration, T> sutFactoryMethod)
        {
            _sutFactoryMethod = sutFactoryMethod;
            Configuration = Helpers.GetTestConfiguration();
        }

        private T _sut;

        protected Configuration Configuration { get; }

        protected T SystemUnderTest
        {
            get
            {
                if (_sut == null)
                {
                    _sut = _sutFactoryMethod.Invoke(Configuration);
                }

                return _sut;
            }
        }
    }
}
