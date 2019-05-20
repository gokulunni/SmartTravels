using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace TripRoutingApp.Models.Lyft
{
    public class LyftArrivalTimes
    {
        [JsonProperty(PropertyName = "eta_estimates")]
        public List<LyftArrivalTimeEstimate> times { get; set; }

        public string timezone { get; set; }
    }
}
