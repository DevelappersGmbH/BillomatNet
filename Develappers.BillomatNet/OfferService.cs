using System;
using Develappers.BillomatNet.Api.Net;

namespace Develappers.BillomatNet
{
    public class OfferService : ServiceBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="OfferService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public OfferService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="OfferService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        internal OfferService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
