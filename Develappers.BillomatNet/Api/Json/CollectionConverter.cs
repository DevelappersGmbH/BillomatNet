// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Develappers.BillomatNet.Api.Json
{
    internal class CollectionConverter<T> : JsonConverter
    {
        /// <summary>
        /// Checks whether this item can be converted
        /// </summary>
        /// <param name="objectType">The Type of the object</param>
        /// <returns>The boolean, true if List.</returns>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            return token.Type == JTokenType.Array ? token.ToObject<List<T>>() : new List<T> { token.ToObject<T>() };
        }

        public override bool CanWrite => false;

        /// <summary>
        /// Writes the list to json - currently not implemented
        /// </summary>
        /// <param name="writer">The Json Writer</param>
        /// <param name="value">The object.</param>
        /// <param name="serializer">The Json Serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("writing objects is not supported");
        }
    }

}
