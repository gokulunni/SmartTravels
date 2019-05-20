using System;
using Newtonsoft.Json;

namespace TripRoutingApp.Models
{
    public class UberPriceEstimate
    {
        [JsonProperty(PropertyName ="localized_display_name")]
        public string localized_display_name { get; set; }

        [JsonProperty(PropertyName = "distance")]
        public float distance { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string display_name { get; set; }

        [JsonProperty(PropertyName = "product_id")]
        public string product_id { get; set; }

        [JsonProperty(PropertyName = "high_estimate")]
        public float? high_estimate { get; set; }

        [JsonProperty(PropertyName = "low_estimate")]
        public float? low_estimate { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public int duration { get; set; }

        [JsonProperty(PropertyName = "estimate")]
        public string estimate { get; set; }

        [JsonProperty(PropertyName = "currency_code")]
        public string currency_code { get; set; }

        //[JsonProperty(PropertyName = "minimum")]
        //public int minimum { get; set; }

        //[JsonProperty(PropertyName = "surge_multiplier")]
        //public float surge_multiplier { get; set; }


    }
}
