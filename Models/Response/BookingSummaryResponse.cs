namespace ApiTestingFrameworkDemo.Models
{
    public class BookingResponse
    {
        public Booking booking { get; set; }
        public int bookingid { get; set; }

    }
    public class Booking
    {
        public Bookingdates bookingdates { get; set; }
        public int bookingid { get; set; }
        public bool depositpaid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int roomid { get; set; }
    }

}
