namespace ApiTestingFrameworkDemo.Support
{
    public static class ApiClientExtension
    {
        public static string Get(this HttpClient client, string endpoint)
        {
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            TestBase.Instance.testData.AddResponseJson(response);
            return response.Content.ReadAsStringAsync().Result;
        }

        public static string Post(this HttpClient client, string endpoint, HttpContent content)
        {
            HttpResponseMessage response = client.PostAsync(endpoint, content).GetAwaiter().GetResult();
            TestBase.Instance.testData.AddResponseJson(response);
            return response.Content.ReadAsStringAsync().Result;
        }

        public static string Put(this HttpClient client, string endpoint, HttpContent content)
        {
            HttpResponseMessage response = client.PutAsync(endpoint, content).GetAwaiter().GetResult();
            TestBase.Instance.testData.AddResponseJson(response);
            return response.Content.ReadAsStringAsync().Result;
        }

        public static string Delete(this HttpClient client, string endpoint)
        {
            HttpResponseMessage response = client.DeleteAsync(endpoint).GetAwaiter().GetResult();
            TestBase.Instance.testData.AddResponseJson(response);
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
