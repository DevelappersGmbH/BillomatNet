using System;
using Develappers.BillomatNet.Api.Net;

namespace Develappers.BillomatNet
{
    public class ReminderService : ServiceBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="ReminderService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public ReminderService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="ReminderService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        internal ReminderService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
