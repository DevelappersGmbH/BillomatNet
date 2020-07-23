using System;

namespace Develappers.BillomatNet.Tests
{
    public abstract class TestBase<T>
    {
        private readonly Func<Configuration, T> _sutFactoryMethod;

        protected TestBase(Func<Configuration, T> sutFactoryMethod)
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