using System;
namespace TripRoutingApp.Models.Search
{
    public class ResultsDataFilteredModel
    {
        public string Name { get; set; }
        public string PriceEstimate { get; set; }
        public int? ArrivalTimeEstimate { get; set; }
    }
}
