// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api.Json
{
    internal class StringToIntConverter : JsonConverter<int>
    {
        /// <summary>
        /// Checks whether this item can be converted
        /// </summary>
        /// <param name="objectType">The Type of the object</param>
        /// <returns>The boolean, true if List.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int);
        }

        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
                return int.Parse(reader.GetString());

            if (reader.TokenType == JsonTokenType.Number)
                return reader.GetInt32();

            throw new JsonException($"Cannot convert {reader.TokenType} to int.");
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

}
