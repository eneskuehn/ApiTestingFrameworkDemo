using Newtonsoft.Json;

namespace ApiTestingFrameworkDemo.Models
{
    public class BookingRequest
    {
        public int roomid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public bool depositpaid { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Bookingdates bookingdates { get; set; }

    }
    public class Bookingdates
    {
        public string checkin { get; set; }
        public string checkout { get; set; }
    }
}
