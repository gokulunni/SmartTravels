using System;
using Newtonsoft.Json;

namespace TripRoutingApp.Models.Lyft
{
    public class LyftToken
    {
        [JsonProperty(PropertyName = "token_type")]
        public string token_type { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string access_token { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int expires_in { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string scope { get; set; }
    }
}
