using System;
using Newtonsoft.Json;
namespace TripRoutingApp.Models.Uber
{
    public class EstimatedArrivalTime
    {
        [JsonProperty(PropertyName = "localized_display_name")]
        public string localized_display_name { get; set; }

        [JsonProperty(PropertyName = "estimate")]
        public int estimate { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string display_name { get; set; }

        [JsonProperty(PropertyName = "product_id")]
        public string product_id { get; set; }
    }
}
