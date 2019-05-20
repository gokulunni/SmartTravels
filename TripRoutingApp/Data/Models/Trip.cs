using System;

namespace TripRoutingApp.Data
{
    public class Trip
    {
        public string startingPoint { get; set; }
        public string Destination { get; set; }
        public DateTime Departure { get; set; }
        public int cost { get; set; }
        public TransportationMethod transportationMethod { get; set; }

    }
}