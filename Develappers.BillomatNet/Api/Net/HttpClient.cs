// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Develappers.BillomatNet.Api.Net
{
    internal sealed class HttpClient : IHttpClient
    {
        /// <summary>
        /// Sets the Billomat ID and the API-key
        /// </summary>
        /// <param name="billomatId">The Billomat ID</param>
        /// <param name="apiKey">The API-key</param>
        public HttpClient(string billomatId, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("invalid api key", nameof(apiKey));
            }

            if (string.IsNullOrWhiteSpace(billomatId))
            {
                throw new ArgumentException("invalid billomat id", nameof(billomatId));
            }

            BillomatId = billomatId;
            ApiKey = apiKey;

            UpdateLimits(null, null);
        }

        public string ApiKey { get; }

        public string BillomatId { get; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string BaseUrl => $"https://{BillomatId}.billomat.net/";

        public event EventHandler ApiCallLimitUpdated;

        public int ApiRequestLimitRemaining { get; private set; }

        public DateTime ApiRequestLimitResetsAt { get; private set; }

        private void UpdateLimits(int? limit, DateTime? resetsAt)
        {
            ApiRequestLimitRemaining = limit ?? int.MaxValue;
            ApiRequestLimitResetsAt = resetsAt ?? DateTime.UtcNow;
            ApiCallLimitUpdated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Makes GET web request to specific URL.
        /// </summary>
        /// <param name="relativeUri">The specific URI.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the request result from the stream.
        /// </returns>
        /// <exception cref="IOException"> Throws when the response was null.</exception>
        public async Task<byte[]> GetBytesAsync(Uri relativeUri, CancellationToken token = default)
        {
            var builder = new UriBuilder(new Uri(new Uri(BaseUrl), relativeUri));
            var uri = builder.ToString();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add(HeaderNames.ApiKey, ApiKey);

            if (!string.IsNullOrWhiteSpace(AppId))
            {
                httpWebRequest.Headers.Add(HeaderNames.AppId, AppId);
            }
            if (!string.IsNullOrWhiteSpace(AppSecret))
            {
                httpWebRequest.Headers.Add(HeaderNames.AppSecret, AppSecret);
            }

            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            var result = await HttpResponseFactory.CreateFromWebResponseAsync<byte[]>(httpResponse);

            UpdateLimits(result.LimitRemaining, result.LimitReset);

            return result.Content;
        }

        /// <summary>
        /// Makes a GET web request to specific URL.
        /// </summary>
        /// <param name="relativeUri">The specific URI.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the request result from the stream.
        /// </returns>
        public Task<string> GetAsync(Uri relativeUri, CancellationToken token = default)
        {
            return GetAsync(relativeUri, null, token);
        }

        /// <summary>
        /// Makes GET web request with filter to specific URL
        /// </summary>
        /// <param name="relativeUri">The specific URI.</param>
        /// <param name="query">The filter.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the request result from the stream.
        /// </returns>
        /// <exception cref="IOException"> Throws when the response was null.</exception>
        public async Task<string> GetAsync(Uri relativeUri, string query, CancellationToken token = default)
        {
            var builder = new UriBuilder(new Uri(new Uri(BaseUrl), relativeUri));
            if (!string.IsNullOrEmpty(query))
            {
                builder.Query = query;
            }
            var uri = builder.ToString();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "GET";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Headers.Add(HeaderNames.ApiKey, ApiKey);

            if (!string.IsNullOrWhiteSpace(AppId))
            {
                httpWebRequest.Headers.Add(HeaderNames.AppId, AppId);
            }
            if (!string.IsNullOrWhiteSpace(AppSecret))
            {
                httpWebRequest.Headers.Add(HeaderNames.AppSecret, AppSecret);
            }

            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            var result = await HttpResponseFactory.CreateFromWebResponseAsync<string>(httpResponse);
            UpdateLimits(result.LimitRemaining, result.LimitReset);
            return result.Content;
        }

        /// <summary>
        /// Makes DELETE web request.
        /// </summary>
        /// <param name="relativeUri">The specific URI.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the request from the stream.
        /// </returns>
        /// <exception cref="IOException"> Throws when the response was null.</exception>
        public async Task<string> DeleteAsync(Uri relativeUri, CancellationToken token)
        {
            var builder = new UriBuilder(new Uri(new Uri(BaseUrl), relativeUri));
            var uri = builder.ToString();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "DELETE";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Headers.Add(HeaderNames.ApiKey, ApiKey);

            if (!string.IsNullOrWhiteSpace(AppId))
            {
                httpWebRequest.Headers.Add(HeaderNames.AppId, AppId);
            }
            if (!string.IsNullOrWhiteSpace(AppSecret))
            {
                httpWebRequest.Headers.Add(HeaderNames.AppSecret, AppSecret);
            }


            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            var result = await HttpResponseFactory.CreateFromWebResponseAsync<string>(httpResponse);
            UpdateLimits(result.LimitRemaining, result.LimitReset);
            return result.Content;
        }

        /// <summary>
        /// Makes PUT web request.
        /// </summary>
        /// <param name="relativeUri">The specific URI.</param>
        /// <param name="data">The data to be sent to the server.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the request result from the stream.
        /// </returns>
        /// <exception cref="IOException"> Throws when the response was null.</exception>
        public async Task<string> PutAsync(Uri relativeUri, string data, CancellationToken token)
        {
            var builder = new UriBuilder(new Uri(new Uri(BaseUrl), relativeUri));
            var uri = builder.ToString();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "PUT";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HeaderNames.ApiKey, ApiKey);

            if (!string.IsNullOrWhiteSpace(AppId))
            {
                httpWebRequest.Headers.Add(HeaderNames.AppId, AppId);
            }
            if (!string.IsNullOrWhiteSpace(AppSecret))
            {
                httpWebRequest.Headers.Add(HeaderNames.AppSecret, AppSecret);
            }

            var reqStream = httpWebRequest.GetRequestStream();
            var bytes = Encoding.UTF8.GetBytes(data);
            await reqStream.WriteAsync(bytes, 0, bytes.Length, token).ConfigureAwait(false);
            reqStream.Close();

            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            var result = await HttpResponseFactory.CreateFromWebResponseAsync<string>(httpResponse);
            UpdateLimits(result.LimitRemaining, result.LimitReset);
            return result.Content;
        }

        /// <summary>
        /// Makes POST web request.
        /// </summary>
        /// <param name="relativeUri">The specific URI.</param>
        /// <param name="data">The data to be sent to the server.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the request result from the stream.
        /// </returns>
        /// <exception cref="IOException">Thrown when the response was null.</exception>
        public async Task<string> PostAsync(Uri relativeUri, string data, CancellationToken token)
        {
            var builder = new UriBuilder(new Uri(new Uri(BaseUrl), relativeUri));
            var uri = builder.ToString();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HeaderNames.ApiKey, ApiKey);

            if (!string.IsNullOrWhiteSpace(AppId))
            {
                httpWebRequest.Headers.Add(HeaderNames.AppId, AppId);
            }
            if (!string.IsNullOrWhiteSpace(AppSecret))
            {
                httpWebRequest.Headers.Add(HeaderNames.AppSecret, AppSecret);
            }

            using (var reqStream = httpWebRequest.GetRequestStream())
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                await reqStream.WriteAsync(bytes, 0, bytes.Length, token).ConfigureAwait(false);
                reqStream.Close();
            }

            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            var result = await HttpResponseFactory.CreateFromWebResponseAsync<string>(httpResponse);
            UpdateLimits(result.LimitRemaining, result.LimitReset);
            return result.Content;
        }
    }
}
