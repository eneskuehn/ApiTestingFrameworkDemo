using ApiTestingFrameworkDemo.Support;
using System.Net;
using System.Text;

namespace ApiTestingFrameworkDemo.ResourceObjects.Auth
{
    public class Auth
    {
        private string resource = Endpoint.Auth.Login;

        public void CreateToken()
        {
            var loginPayload = new
            {
                username = TestBase.Instance.GetAppParameter("AdminUsername"),
                password = TestBase.Instance.GetAppParameter("AdminPassword")

            };

            HttpContent content = new StringContent(loginPayload.PreparePayload(), Encoding.UTF8, "application/json");
            TestBase.Instance.httpClient.Post(resource, content);

            HttpResponseMessage response = TestBase.Instance.httpClient.PostAsync(resource, content).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                // Get the 'Set-Cookie' header value
                if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
                {
                    foreach (var cookieValue in cookieValues)
                    {
                        // Extract the token from the cookie value
                        string token = ExtractTokenFromCookie(cookieValue);
                        Console.WriteLine($"Token: {token}");
                    }
                }
                else
                {
                    Console.WriteLine("No 'Set-Cookie' header found in the response");
                }
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            }
        }

        private string ExtractTokenFromCookie(string cookieValue)
        {
            // Extract the token from the cookie value
            // Here, we assume that the token is between 'token=' and the next ';' character
            int startIndex = cookieValue.IndexOf("token=") + 6; // Length of "token="
            int endIndex = cookieValue.IndexOf(';', startIndex);
            if (endIndex == -1)
            {
                endIndex = cookieValue.Length;
            }

            return cookieValue.Substring(startIndex, endIndex - startIndex);
        }
    }
}
