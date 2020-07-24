using System;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [Trait(Traits.Category, Traits.Categories.IntegrationTest)]
    public abstract class IntegrationTestBase<T>
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
