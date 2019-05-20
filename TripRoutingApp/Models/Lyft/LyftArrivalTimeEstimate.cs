using System;
using Newtonsoft.Json;

namespace TripRoutingApp.Models.Lyft
{
    public class LyftArrivalTimeEstimate
    {
        [JsonProperty(PropertyName = "ride_type")]
        public string ride_type { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string display_name { get; set; }

        [JsonProperty(PropertyName = "eta_seconds")]
        public int? eta_seconds { get; set; }

        [JsonProperty(PropertyName = "is_valid_estimate")]
        public bool is_valid_estimate { get; set; }
    }
}
