using System;
using Newtonsoft.Json;

namespace TripRoutingApp.Models.Google
{
    public class Southwest
    {
        [JsonProperty(PropertyName = "lat")]
        public double lat { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public double lng { get; set; }
    }
}
