using System;
using System.Collections.Generic;
namespace TripRoutingApp.Models.Search
{
    public class ResultsPresentationModel
    {
        public List<ResultsDataFilteredModel> UberServices { get; set; }
        public List<ResultsDataFilteredModel> LyftServices { get; set; }
    }
}
