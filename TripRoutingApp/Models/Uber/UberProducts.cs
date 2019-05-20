using System;
using System.Runtime.Serialization;

namespace TripRoutingApp.Models
{
    [DataContract]
    public class UberProduct
    {
        [DataMember]
        public int capacity { get; set; }

        [DataMember]
        public string product_id { get; set; }

        [DataMember]
        public PriceDetails price_details { get; set; }

        [DataMember]
        public string image { get; set; }

        [DataMember]
        public bool cash_enabled { get; set; }

        [DataMember]
        public bool shared { get; set; }

        [DataMember]
        public string short_description { get; set; }

        [DataMember]
        public string display_name { get; set; }

        [DataMember]
        public string product_group { get; set; }

        [DataMember]
        public string description { get; set; }


    }
}
