using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api.Net;

namespace TaurusSoftware.BillomatNet
{
    public abstract class ServiceBase
    {
        protected Configuration Configuration { get; private set; }

        protected ServiceBase(Configuration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Executes an API call to retrieve one element and returns the item or null if not found.
        /// </summary>
        /// <typeparam name="T">The type to convert the response to</typeparam>
        /// <param name="resourceUrl">The relative url.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        protected async Task<T> GetItemByIdAsync<T>(string resourceUrl, CancellationToken token = default(CancellationToken)) where T : class 
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            string httpResponse;

            try
            {
                httpResponse = await httpClient.GetAsync(new Uri(resourceUrl, UriKind.Relative), token);
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                // NotFound
                return null;
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Unauthorized
                throw new NotAuthorizedException("You are not authorized to access this item.", wex);
            }

            return JsonConvert.DeserializeObject<T>(httpResponse);
        }

        /// <summary>
        /// Executes an API call to retrieve a list of elements.
        /// </summary>
        /// <typeparam name="T">The type to convert the response to</typeparam>
        /// <param name="resourceUrl">The relative url.</param>
        /// <param name="query"></param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        protected async Task<T> GetListAsync<T>(string resourceUrl, string query, CancellationToken token)
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri(resourceUrl, UriKind.Relative), query, token);
            return JsonConvert.DeserializeObject<T>(httpResponse);
        }
    }
}