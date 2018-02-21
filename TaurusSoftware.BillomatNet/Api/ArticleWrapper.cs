using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ArticleWrapper
    {
        [JsonProperty("article")]
        public Article Article { get; set; }
    }
}