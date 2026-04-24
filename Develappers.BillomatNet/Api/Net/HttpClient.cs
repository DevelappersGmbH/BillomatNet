// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Http = System.Net.Http;

namespace Develappers.BillomatNet.Api.Net
{
    internal sealed class HttpClient : IHttpClient
    {
        public string ApiKey { get; }

        public string BillomatId { get; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string BaseUrl => _httpClient.BaseAddress.ToString();

        public event EventHandler ApiCallLimitUpdated;

        public int ApiRequestLimitRemaining { get; private set; }

        public DateTime ApiRequestLimitResetsAt { get; private set; }

        private readonly Http.HttpClient _httpClient;

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

            _httpClient = new() { BaseAddress = new Uri($"https://{BillomatId}.billomat.net/") };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.ApiKey, ApiKey);
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.AppId, AppId);
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.AppSecret, AppSecret);

            UpdateLimits(null, null);
        }

        private void UpdateLimits(int? limit, DateTime? resetsAt)
        {
            ApiRequestLimitRemaining = limit ?? int.MaxValue;
            ApiRequestLimitResetsAt = resetsAt ?? DateTime.UtcNow;
            ApiCallLimitUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateLimits(Http.HttpResponseMessage response)
        {
            int? limitRemaing = null;
            DateTime? limitReset = null;

            if (response.Content.Headers.TryGetValues(HeaderNames.LimitRemaining, out var limitHeader))
            {
                if (int.TryParse(limitHeader.SingleOrDefault() ?? "", out var lrem))
                {
                    limitRemaing = lrem;
                }
            }
            if (response.Content.Headers.TryGetValues(HeaderNames.LimitReset, out var resetHeader))
            {
                if (DateTime.TryParse(resetHeader.SingleOrDefault() ?? "", out var lres))
                {
                    limitReset = lres;
                }
            }

            UpdateLimits(limitRemaing, limitReset);
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
        public async Task<string> GetAsync(Uri relativeUri, CancellationToken token = default)
        {
            var response = await _httpClient.GetAsync(relativeUri, token);
            UpdateLimits(response);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(token);
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

            return await GetAsync(builder.Uri, token);
        }

        /// <summary>
        /// <inheritdoc cref="Http.HttpClient.GetByteArrayAsync(Uri?, CancellationToken)"/>
        /// </summary>
        /// <returns>
        /// <inheritdoc cref="Http.HttpClient.GetByteArrayAsync(Uri?, CancellationToken)"/>
        /// </returns>
        /// <exception cref="IOException"> Throws when the response was null.</exception>
        public async Task<byte[]> GetBytesAsync(Uri relativeUri, CancellationToken token = default)
        {
            var response = await _httpClient.GetAsync(relativeUri, token);
            UpdateLimits(response);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync(token);
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
            var response = await _httpClient.DeleteAsync(relativeUri, token);
            UpdateLimits(response);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(token);
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
            var response = await _httpClient.PutAsync(relativeUri, new Http.StringContent(data), token);
            UpdateLimits(response);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(token);
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
            var response = await _httpClient.PostAsync(relativeUri, new Http.StringContent(data), token);
            UpdateLimits(response);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(token);
        }
    }
}
