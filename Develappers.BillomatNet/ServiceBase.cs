﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Newtonsoft.Json;

namespace Develappers.BillomatNet
{
    public abstract class ServiceBase
    {
        protected readonly Func<IHttpClient> HttpClientFactory;

        protected ServiceBase(Configuration configuration) : this(() =>
            new HttpClient(configuration.BillomatId, configuration.ApiKey)
            {
                AppId = configuration.AppId,
                AppSecret = configuration.AppSecret
            })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        /// <remarks>
        /// Used to create a new instance for tests. Should be exposed as internal constructor to create unit tests.
        /// </remarks>
        protected ServiceBase(Func<IHttpClient> httpClientFactory)
        {
            HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Executes an API call to retrieve one element and returns the item or null if not found.
        /// </summary>
        /// <typeparam name="T">The type to convert the response to</typeparam>
        /// <param name="resourceUrl">The relative url.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the entity.
        /// </returns>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        protected async Task<T> GetItemByIdAsync<T>(string resourceUrl, CancellationToken token = default) where T : class
        {
            var httpClient = HttpClientFactory.Invoke();
            string httpResponse;

            try
            {
                httpResponse = await httpClient.GetAsync(new Uri(resourceUrl, UriKind.Relative), token).ConfigureAwait(false);
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
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of entities.
        /// </returns>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        protected async Task<T> GetListAsync<T>(string resourceUrl, string query, CancellationToken token)
        {
            var httpClient = HttpClientFactory.Invoke();
            string httpResponse;
            try
            {
                httpResponse = await httpClient.GetAsync(new Uri(resourceUrl, UriKind.Relative), query, token).ConfigureAwait(false);
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                // NotFound
                throw new NotFoundException($"The resource at {resourceUrl} could not be found.", wex);
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
        /// Executes an API call to delete an entity.
        /// </summary>
        /// <param name="resourceUrl">The relative url.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        protected async Task DeleteAsync(string resourceUrl, CancellationToken token)
        {
            var httpClient = HttpClientFactory.Invoke();
            try
            {
                await httpClient.DeleteAsync(new Uri(resourceUrl, UriKind.Relative), token).ConfigureAwait(false);
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                // NotFound
                throw new NotFoundException($"The resource at {resourceUrl} could not be found.", wex);
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Unauthorized
                throw new NotAuthorizedException("You are not authorized to delete this item.", wex);
            }
        }

        /// <summary>
        /// Executes an API call to update an entity.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="resourceUrl">The resource URL.</param>
        /// <param name="model">The model.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the updated entity.
        /// </returns>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        protected Task<T> PutAsync<T>(string resourceUrl, T model, CancellationToken token)
            where T : class
        {
            return PutAsync<T, T>(resourceUrl, model, token);
        }

        /// <summary>
        /// Executes an API call to update an entity.
        /// </summary>
        /// <typeparam name="TOut">The result type.</typeparam>
        /// <typeparam name="TIn">The input type in.</typeparam>
        /// <param name="resourceUrl">The resource URL.</param>
        /// <param name="model">The model.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the result.
        /// </returns>
        /// <exception cref="NotFoundException">The resource at {resourceUrl} could not be found.</exception>
        /// <exception cref="NotAuthorizedException">You are not authorized to change this item.</exception>
        protected async Task<TOut> PutAsync<TOut, TIn>(string resourceUrl, TIn model, CancellationToken token)
            where TIn : class
            where TOut : class
        {
            var httpClient = HttpClientFactory.Invoke();

            try
            {
                var requestData = model == null ? "" : JsonConvert.SerializeObject(model);
                var result = await httpClient.PutAsync(new Uri(resourceUrl, UriKind.Relative), requestData, token).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<TOut>(result);
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException($"The resource at {resourceUrl} could not be found.", wex);
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Unauthorized
                throw new NotAuthorizedException("You are not authorized to change this item.", wex);
            }
        }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="resourceUrl">The resource URL.</param>
        /// <param name="model">The model.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the newly created entity.
        /// </returns>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        protected Task<T> PostAsync<T>(string resourceUrl, T model, CancellationToken token)
            where T : class
        {
            return PostAsync<T, T>(resourceUrl, model, token);
        }


        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <typeparam name="TOut">The output type.</typeparam>
        /// <typeparam name="TIn">The type input type.</typeparam>
        /// <param name="resourceUrl">The resource URL.</param>
        /// <param name="model">The model.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the output entity.
        /// </returns>
        /// <exception cref="NotFoundException">The resource at {resourceUrl} could not be found.</exception>
        /// <exception cref="NotAuthorizedException">You are not authorized to change this item.</exception>
        private async Task<TOut> PostAsync<TOut, TIn>(string resourceUrl, TIn model, CancellationToken token)
            where TIn : class
            where TOut : class
        {
            var httpClient = HttpClientFactory.Invoke();

            try
            {
                var requestData = model == null ? "" : JsonConvert.SerializeObject(model, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                var responseData = await httpClient.PostAsync(new Uri(resourceUrl, UriKind.Relative), requestData, token);
                return JsonConvert.DeserializeObject<TOut>(responseData);
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException($"The resource at {resourceUrl} could not be found.", wex);
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Unauthorized
                throw new NotAuthorizedException("You are not authorized to change this item.", wex);
            }
        }
    }
}
