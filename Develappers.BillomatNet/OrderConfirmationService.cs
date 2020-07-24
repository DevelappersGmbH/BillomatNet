using System;
using Develappers.BillomatNet.Api.Net;

namespace Develappers.BillomatNet
{
    public class OrderConfirmationService : ServiceBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="OrderConfirmationService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public OrderConfirmationService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="OrderConfirmationService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        internal OrderConfirmationService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
