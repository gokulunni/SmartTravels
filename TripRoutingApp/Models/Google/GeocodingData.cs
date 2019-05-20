using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TripRoutingApp.Models.Google
{
    public class GeocodingData
    {
        [JsonProperty(PropertyName = "results")]
        public List<Result> results { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string status { get; set; }
    }
}
