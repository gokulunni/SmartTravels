using System;
namespace TripRoutingApp.Exceptions
{
    public class ApiException: Exception
    {
        public int StatusCode { get; set; }
        public string ExceptionContent { get; set; }
    }
}
