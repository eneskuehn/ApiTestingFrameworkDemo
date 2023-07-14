using ApiTestingFrameworkDemo.Support;
using ApiTestingFrameworkDemo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Text;
using System.Web;

namespace ApiTestingFrameworkDemo.ResourceObjects
{
    public class Booking
    {
        private string resource = Endpoint.Booking;

        public void GetBookings()
        {
            TestBase.Instance.httpClient.Get(resource);
        }
        public void GetBooking(string id)
        {
            TestBase.Instance.httpClient.Get(resource + id);
        }
        public void GetBookingSummary(string roomId)
        {
            var queryParameters = HttpUtility.ParseQueryString(string.Empty);
            // Add query parameters
            queryParameters["roomid"] = roomId;
            //queryParameters["param2"] = "value2";
            TestBase.Instance.httpClient.Get($"{resource}?{queryParameters.ToString()}");
        }
        public void PostBooking(BookingRequest payload)
        {
            HttpContent content = new StringContent(payload.PreparePayload(), Encoding.UTF8, "application/json");
            TestBase.Instance.httpClient.Post(resource, content);
        }
        public void UpdateBooking(int bookingId, BookingRequest payload)
        {
            HttpContent content = new StringContent(payload.PreparePayload(), Encoding.UTF8, "application/json");
            TestBase.Instance.httpClient.Put(resource + bookingId.ToString(), content);
        }
        public void DeleteBooking(int bookingId)
        {
            TestBase.Instance.httpClient.Delete(resource + bookingId);
        }
    }
}
