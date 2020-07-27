// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

namespace Develappers.BillomatNet
{
    /// <summary>
    /// Model for the Configuration data.
    /// </summary>
    public class Configuration
    {
        public string BillomatId { get; set; }
        public string ApiKey { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }

        /// <summary>
        /// Creates a deep copy of the item.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>A new instance of the <see cref="Configuration"/>.</returns>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal static Configuration DeepCopy(Configuration other)
        {
            return new Configuration
            {
                BillomatId = other.BillomatId,
                AppId = other.AppId,
                AppSecret = other.AppSecret,
                ApiKey = other.ApiKey
            };
        }
    }
}
