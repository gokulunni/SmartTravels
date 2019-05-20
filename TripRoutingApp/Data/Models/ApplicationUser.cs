using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace TripRoutingApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public IEnumerable<Trip> SavedTrips { get; set; }
    }
}
