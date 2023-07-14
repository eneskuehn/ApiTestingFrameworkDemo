namespace ApiTestingFrameworkDemo.Support
{
    public static class Endpoint
    {
        public static string Message => "/message/";
        public static string MessageHealth => "/message/actuator/health";
        public static string Room => "/room/";
        public static string Booking => "/booking/";
        public static string BookingSummary => "/booking/summary";
        public class Auth
        {
            public static string Login => "/auth/login";
        }
        
    }
}
