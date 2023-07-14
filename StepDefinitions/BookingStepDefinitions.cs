using ApiTestingFrameworkDemo.Support;
using ApiTestingFrameworkDemo.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ApiTestingFrameworkDemo.StepDefinitions
{
    [Binding]
    public class BookingStepDefinitions
    {
        ResourceObjects.Booking _booking = new();
        BookingRequest? _bookingRequest;
        BookingRequest? _updatedBookingRequest;

        [Given(@"I create a new booking")]
        public void GivenICreateANewBooking(Table table)
        {
            foreach (var row in table.Rows)
            {
                _bookingRequest = new BookingRequest
                {
                    roomid = Convert.ToInt32(row["roomid"]),
                    firstname = row["firstname"],
                    lastname = row["lastname"],
                    depositpaid = Convert.ToBoolean(row["depositpaid"]),
                    email = row["email"],
                    phone = row["phone"],
                    bookingdates = new Bookingdates
                    {
                        checkin = row["checkin"],
                        checkout = row["checkout"]
                    }
                };
            }
        }

        [When(@"I send POST booking request")]
        [Then(@"I send POST booking request")]
        public void WhenISendPOSTBookingRequest()
        {
            _booking.PostBooking(_bookingRequest);
        }

        [Then(@"I validate booking is created and valid")]
        public void ThenIValidateBookingIsCreatedAndValid()
        {
            //verify response
            Assert.AreEqual(201, TestBase.Instance.testData.GetResponseStatusCode());
            var responseJson = TestBase.Instance.testData.GetResponseJson().ToString();
            BookingResponse response = JsonSerializer.Deserialize<BookingResponse>(responseJson);

            Assert.True(response.bookingid >= 1);
            Assert.AreEqual(_bookingRequest.roomid, response.booking.roomid);
            Assert.AreEqual(_bookingRequest.firstname, response.booking.firstname);
            Assert.AreEqual(_bookingRequest.lastname, response.booking.lastname);
            Assert.AreEqual(_bookingRequest.depositpaid, response.booking.depositpaid);
            Assert.AreEqual(_bookingRequest.bookingdates.checkin, response.booking.bookingdates.checkin);
            Assert.AreEqual(_bookingRequest.bookingdates.checkout, response.booking.bookingdates.checkout);

            //verify get booking by id
            _booking.GetBooking(response.booking.bookingid.ToString());
            Assert.AreEqual(200, TestBase.Instance.testData.GetResponseStatusCode());

            responseJson = TestBase.Instance.testData.GetResponseJson().ToString();
            Models.Booking bookingDetailsResponse = JsonSerializer.Deserialize<Models.Booking>(responseJson);
            Assert.AreEqual(response.booking.bookingid, bookingDetailsResponse.bookingid);
            Assert.AreEqual(_bookingRequest.roomid, bookingDetailsResponse.roomid);
            Assert.AreEqual(_bookingRequest.firstname, bookingDetailsResponse.firstname);
            Assert.AreEqual(_bookingRequest.lastname, bookingDetailsResponse.lastname);
            Assert.AreEqual(_bookingRequest.depositpaid, bookingDetailsResponse.depositpaid);
            Assert.AreEqual(_bookingRequest.bookingdates.checkin, bookingDetailsResponse.bookingdates.checkin);
            Assert.AreEqual(_bookingRequest.bookingdates.checkout, bookingDetailsResponse.bookingdates.checkout);

            //store param for later stages
            TestBase.Instance.testData.AddOrReplace("bookingId", bookingDetailsResponse.bookingid);
            TestBase.Instance.testData.Add("ID", "<id value>");
            TestBase.Instance.testData.Get("ID");
        }

        [Then(@"I update the booking")]
        public void ThenIUpdateTheBooking(Table table)
        {
            foreach (var row in table.Rows)
            {
                _updatedBookingRequest = new BookingRequest
                {
                    roomid = Convert.ToInt32(row["roomid"]),
                    firstname = row["firstname"],
                    lastname = row["lastname"],
                    depositpaid = Convert.ToBoolean(row["depositpaid"]),
                    email = row["email"],
                    phone = row["phone"],
                    bookingdates = new Bookingdates
                    {
                        checkin = row["checkin"],
                        checkout = row["checkout"]
                    }
                };
            }
        }

        [When(@"I send PUT booking request")]
        public void WhenISendPUTBookingRequest()
        {
             var receivedBooking = JsonConvert.DeserializeObject<Models.Booking>(TestBase.Instance.testData.GetResponseJson().ToString());
            _booking.UpdateBooking(receivedBooking.bookingid, _updatedBookingRequest);
        }

        [Then(@"I validate booking is updated")]
        public void ThenIValidateBookingIsUpdated()
        {
            Assert.AreEqual(200, TestBase.Instance.testData.GetResponseStatusCode());

            var responseJson = TestBase.Instance.testData.GetResponseJson().ToString();
            BookingResponse response = JsonSerializer.Deserialize<BookingResponse>(responseJson);

            Assert.AreEqual(_updatedBookingRequest.roomid, response.booking.roomid);
            Assert.AreEqual(_updatedBookingRequest.firstname, response.booking.firstname);
            Assert.AreEqual(_updatedBookingRequest.lastname, response.booking.lastname);
            Assert.AreEqual(_updatedBookingRequest.depositpaid, response.booking.depositpaid);
            Assert.AreEqual(_updatedBookingRequest.bookingdates.checkin, response.booking.bookingdates.checkin);
            Assert.AreEqual(_updatedBookingRequest.bookingdates.checkout, response.booking.bookingdates.checkout);
        }

        [Then(@"I delete the updated booking")]
        public void ThenIDeleteTheUpdatedBooking()
        {
            Models.Booking repsonse = JsonConvert.DeserializeObject<Models.Booking>(TestBase.Instance.testData.GetResponseJson().ToString());
            _booking.DeleteBooking(repsonse.bookingid);
        }

        [Then(@"I delete created booking")]
        [When(@"I delete created booking")]
        public void ThenIDeleteCreatedBooking()
        {
            _booking.DeleteBooking(TestBase.Instance.testData.Get<int>("bookingId"));
        }

        [Then(@"I verify booking summary dates")]
        public void ThenIVerifyBookingSummaryDates()
        {
            _booking.GetBookingSummary(_bookingRequest.roomid.ToString());
            var responseJson = TestBase.Instance.testData.GetResponseJson().ToString();
            var response = JsonSerializer.Deserialize<BookingSummaryResponse>(responseJson);
            Assert.AreEqual(200, TestBase.Instance.testData.GetResponseStatusCode());
            Assert.AreEqual(_bookingRequest.bookingdates.checkin, response.bookings.Last().bookingdates.checkin);
            Assert.AreEqual(_bookingRequest.bookingdates.checkout, response.bookings.Last().bookingdates.checkout);
        }

        [Given(@"I create a new booking with autogenerated used data for days ""([^""]*)"" and ""([^""]*)""")]
        public void GivenICreateANewBookingWithAutogeneratedUsedDataForDaysAnd(string checkin, string checkout)
        {
            _bookingRequest = new BookingRequest
            {
                roomid = 1,
                firstname = Faker.Name.FullName(),
                lastname = Faker.Name.Last(),
                depositpaid = Faker.Boolean.Random(),
                email = Faker.Internet.Email(),
                phone = Faker.Phone.Number(),
                bookingdates = new Bookingdates
                {
                    checkin = checkin,
                    checkout = checkout
                }
            };
        }
    }
}
