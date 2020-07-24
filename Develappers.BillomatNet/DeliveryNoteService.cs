using System;
using Develappers.BillomatNet.Api.Net;

namespace Develappers.BillomatNet
{
    public class DeliveryNoteService : ServiceBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="DeliveryNoteService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public DeliveryNoteService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="DeliveryNoteService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        internal DeliveryNoteService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
