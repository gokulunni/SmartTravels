using System;
using Newtonsoft.Json;

namespace TripRoutingApp.Models.Google
{
    public class Geometry
    {
        [JsonProperty(PropertyName = "location")]
        public Location location { get; set; }

        [JsonProperty(PropertyName = "location_type")]
        public string location_type { get; set; }

        [JsonProperty(PropertyName = "viewreport")]
        public Viewport viewport { get; set; }
    }
}
