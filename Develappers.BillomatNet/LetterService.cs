using System;
using Develappers.BillomatNet.Api.Net;

namespace Develappers.BillomatNet
{
    public class LetterService : ServiceBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="LetterService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public LetterService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="LetterService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        internal LetterService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
