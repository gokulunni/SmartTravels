using System;
using Newtonsoft.Json;

namespace TripRoutingApp.Models.Lyft
{
    public class LyftCostEstimate
    {
        [JsonProperty(PropertyName = "currency")]
        public string currency { get; set; }

        [JsonProperty(PropertyName = "ride_type")]
        public string ride_type { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string display_name { get; set; }

        [JsonProperty(PropertyName = "primetime_percentage")]
        public string primetime_percentage { get; set; }

        [JsonProperty(PropertyName = "primetime_confirmation_token")]
        public object primetime_confirmation_token { get; set; }

        [JsonProperty(PropertyName = "cost_token")]
        public object cost_token { get; set; }

        [JsonProperty(PropertyName = "price_quote_id")]
        public string price_quote_id { get; set; }

        [JsonProperty(PropertyName = "price_group_id")]
        public string price_group_id { get; set; }

        [JsonProperty(PropertyName = "is_scheduled_ride")]
        public bool is_scheduled_ride { get; set; }

        [JsonProperty(PropertyName = "can_request_ride")]
        public bool can_request_ride { get; set; }

        [JsonProperty(PropertyName = "is_valid_estimate")]
        public bool is_valid_estimate { get; set; }

        [JsonProperty(PropertyName = "estimated_duration_seconds")]
        public int estimated_duration_seconds { get; set; }

        [JsonProperty(PropertyName = "estimated_distance_miles")]
        public double estimated_distance_miles { get; set; }

        [JsonProperty(PropertyName = "estimated_cost_cents_min")]
        public int estimated_cost_cents_min { get; set; }

        [JsonProperty(PropertyName = "estimated_cost_cents_max")]
        public int estimated_cost_cents_max { get; set; }
    }
}
