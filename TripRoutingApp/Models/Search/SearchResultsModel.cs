using System;
using System.Collections.Generic;
using TripRoutingApp.Models.Uber;
using TripRoutingApp.Models.Lyft;

namespace TripRoutingApp.Models
{
    public class SearchResultsModel
    {
        public PriceList UberCostEstimate { get; set; }
        public LyftPriceEstimatesList LyftCostEstimate { get; set; }
        public ArrivalTimesList UberArrivalTimeEstimate { get; set; }
        public LyftArrivalTimes LyftArrivalTimeEstimate { get; set; }
    }
}
