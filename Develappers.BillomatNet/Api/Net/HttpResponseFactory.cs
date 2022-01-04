// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using StreamReader = System.IO.StreamReader;

namespace Develappers.BillomatNet.Api.Net
{
    internal static class HttpResponseFactory
    {
        /// <summary>
        /// A class containing all relevant information from the HttpWebResponse.
        /// </summary>
        /// <typeparam name="T">The type of the content property in result.</typeparam>
        /// <param name="value">The HttpWebResponse to evaluate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when the given argument is null.</exception>
        /// <exception cref="IOException">Thrown when the response stream is null.</exception>
        /// <exception cref="NotSupportedException">Thrown when the type is not supported.</exception>
        public static async Task<HttpResponse<T>> CreateFromWebResponseAsync<T>(HttpWebResponse value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var responseStream = value.GetResponseStream();
            if (responseStream == null)
            {
                throw new IOException("response stream was null!");
            }

            var result = new HttpResponse<T>();

            switch (typeof(T))
            {
                case Type byteArrayType when byteArrayType == typeof(byte[]):
                    var ms = new MemoryStream();
                    await responseStream.CopyToAsync(ms);
                    var byteArrayContent = ms.ToArray();

                    result.Content = (T)(object)byteArrayContent;
                    break;
                case Type stringType when stringType == typeof(string):
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        var stringContent = await streamReader.ReadToEndAsync();
                        result.Content = (T)(object)stringContent;
                    }
                    break;
                default:
                    throw new NotSupportedException("cannot create HttpResponse of the given type");
            }

            if (int.TryParse(value.GetResponseHeader(HeaderNames.LimitRemaining), out var lrem))
            {
                result.LimitRemaining = lrem;
            }

            if (int.TryParse(value.GetResponseHeader(HeaderNames.LimitReset), out var lres))
            {
                result.LimitReset = TimeSpan.FromSeconds(lres);
            }

            return result;

        }
    }
}
