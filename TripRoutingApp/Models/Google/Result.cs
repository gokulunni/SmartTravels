using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TripRoutingApp.Models.Google
{
    public class Result
    {
        [JsonProperty(PropertyName = "address_components")]
        public List<AddressComponents> address_components { get; set; }

        [JsonProperty(PropertyName = "formatted_address")]
        public string formatted_address { get; set; }

        [JsonProperty(PropertyName = "geometry")]
        public Geometry geometry { get; set; }

        [JsonProperty(PropertyName = "place_id")]
        public string place_id { get; set; }

        [JsonProperty(PropertyName = "types")]
        public List<string> types { get; set; }
    
    }
}
