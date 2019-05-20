using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TripRoutingApp.Models.Lyft
{
    public class LyftPriceEstimatesList
    {
        [JsonProperty(PropertyName = "cost_estimates")]
        public List<LyftCostEstimate> PriceEstimates { get; set; }
    }
}
