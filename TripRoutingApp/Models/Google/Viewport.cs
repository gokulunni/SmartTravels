using System;
using Newtonsoft.Json;

namespace TripRoutingApp.Models.Google
{
    public class Viewport
    {
        [JsonProperty(PropertyName = "northeast")]
        public Northeast northeast { get; set; }

        [JsonProperty(PropertyName = "southwest")]
        public Southwest southwest { get; set; }
    }
}
