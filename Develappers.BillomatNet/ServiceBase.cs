using System;
using System.IO; // new
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Newtonsoft.Json;

namespace Develappers.BillomatNet
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

        /// <summary>
        /// Executes an API call to delete an entity.
        /// </summary>
        /// <param name="resourceUrl">The relative url.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        protected async Task DeleteAsync(string resourceUrl, CancellationToken token)
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            try
            {
                await httpClient.DeleteAsync(new Uri(resourceUrl, UriKind.Relative), token);
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                // NotFound
                return;
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Unauthorized
                throw new NotAuthorizedException("You are not authorized to delete this item.", wex);
            }
        }

        protected async Task PutAsync<TIn>(string resourceUrl, TIn model, CancellationToken token) 
            where TIn : class
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);

            try
            {
                var requestData = model == null ? "" : JsonConvert.SerializeObject(model);
                await httpClient.PutAsync(new Uri(resourceUrl, UriKind.Relative), requestData, token);
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                // NotFound
                return;
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Unauthorized
                throw new NotAuthorizedException("You are not authorized to change this item.", wex);
            }
        }

        protected async Task<string> PostAsync<TIn>(string resourceUrl, TIn model, CancellationToken token)
            where TIn : class
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            string httpResponse;

            try
            {
                var requestData = model == null ? "" : JsonConvert.SerializeObject(model);
                httpResponse = await httpClient.PostAsync(new Uri(resourceUrl, UriKind.Relative), requestData, token);
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                // NotFound
                return "";//maybe return string
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Unauthorized
                throw new NotAuthorizedException("You are not authorized to change this item.", wex);
            }
            return httpResponse;
        }
    }
}