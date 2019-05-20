using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TripRoutingApp.Models.Google
{
    public class AddressComponents
    {
        [JsonProperty(PropertyName = "long_name")]
        public string long_name { get; set; }

        [JsonProperty(PropertyName = "short_name")]
        public string short_name { get; set; }

        [JsonProperty(PropertyName = "types")]
        public List<string> types { get; set; }
    }
}
