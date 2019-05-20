using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TripRoutingApp.Models.Uber
{
    public class PriceList
    {
        [JsonProperty(PropertyName = "prices")]
        public List<UberPriceEstimate> prices { get; set; }
    }
}
