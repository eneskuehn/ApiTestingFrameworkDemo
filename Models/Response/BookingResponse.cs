namespace ApiTestingFrameworkDemo.Models
{
    public class BookingSummaryResponse
    {
        public List<Booking> bookings { get; set; }

    }
    public class BookingDates
    {
        public string checkin { get; set; }
        public string checkout { get; set; }
    }
}
