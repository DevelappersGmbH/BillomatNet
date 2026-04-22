// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Reflection;
using Develappers.BillomatNet.Api.Net;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    public static class Helpers
    {
        public static Configuration GetTestConfiguration()
        {
            return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(@"..\..\..\..\config.json"));
        }

        public static bool TrySetHttpClientApiKey(ServiceBase service, string value)
        {
            if (typeof(ServiceBase).GetField("_httpClient", BindingFlags.NonPublic | BindingFlags.Instance) is FieldInfo httpField)
            {
                if (httpField.GetValue(service) is HttpClient http)
                {
                    if (typeof(HttpClient).GetField("<ApiKey>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic) is FieldInfo keyField)
                    {
                        keyField.SetValue(http, value);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
