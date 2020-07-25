using System;

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
