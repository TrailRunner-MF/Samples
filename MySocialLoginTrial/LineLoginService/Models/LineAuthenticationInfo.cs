using Newtonsoft.Json;

namespace LineLoginService.Models
{

    [JsonObject("LineAuthenticationInfo")]
    public class LineAuthenticationInfo
    {
        [JsonProperty("accesstoken")]
        public string AccessToken { get; set; }

        [JsonProperty("isauthenticated")]
        public bool IsAuthenticated { get; set; }

        [JsonProperty("providerid")]
        public string ProviderID { get; set; }

        [JsonProperty("providerkey")]
        public string ProviderKey { get; set; }

        [JsonProperty("userid")]
        public string UserID { get; set; }

        [JsonProperty("returnurl")]
        public string ReturnUrl { get; set; }

    }

}
