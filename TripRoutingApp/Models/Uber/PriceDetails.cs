using System;
namespace TripRoutingApp.Models
{
    public class PriceDetails
    {
        public ServiceFees[] service_fees;
        public int cost_per_minute { get; set; }
        public string distance_unit { get; set; }
        public int minimum { get; set; }
        public int cost_per_distance { get; set; }
        public int Base { get; set; }
        public int cancellation_fee { get; set; }
        public string currency_code { get; set; }
    }
}
