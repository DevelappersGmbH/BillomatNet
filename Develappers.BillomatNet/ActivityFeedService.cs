using System;
using Develappers.BillomatNet.Api.Net;

namespace Develappers.BillomatNet
{
    public class ActivityFeedService : ServiceBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="ActivityFeedService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public ActivityFeedService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="ActivityFeedService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        internal ActivityFeedService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
