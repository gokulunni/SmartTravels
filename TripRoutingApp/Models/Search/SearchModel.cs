using System;
namespace TripRoutingApp.Models
{
    public class SearchModel
    {
        public DateTime date { get; set; }
        public string departure { get; set; }
        public string arrival { get; set; }
        public string startingPoint { get; set; }
        public string destination { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
