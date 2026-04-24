// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api.Json
{
    internal class CollectionConverter<T> : JsonConverter<List<T>>
    {
        /// <summary>
        /// Checks whether this item can be converted
        /// </summary>
        /// <param name="objectType">The Type of the object</param>
        /// <returns>The boolean, true if List.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<T>);
        }

        public override List<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                JsonValueKind kind = doc.RootElement.ValueKind;
                return kind == JsonValueKind.Array ? doc.Deserialize<List<T>>() : new List<T> { doc.Deserialize<T>() };
            }
        }

        public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (T item in value)
            {
                JsonSerializer.Serialize(writer, item);
            }
            writer.WriteEndArray();
        }
    }

}
