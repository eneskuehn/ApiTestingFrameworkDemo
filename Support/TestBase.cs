using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net.Http.Headers;

namespace ApiTestingFrameworkDemo.Support
{
    public class TestBase
    {
        private IConfiguration configuration;
        private Dictionary<string, string> appSettings;
        public Dictionary<object, object> testData;
        public HttpResponseMessage Response { get; set; }
        public HttpClient httpClient;
        #region Singleton
        private static TestBase instance;
        private static readonly object lockObject = new object();

        private TestBase()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true);
            testData = new Dictionary<object, object> ();
            configuration = configBuilder.Build();
            appSettings = DictionaryExtension.GetDictionary(configuration, "AppSettings");
            InitializeHttpClient();
            testData.Add("responseCounter", 0);
        }

        public static TestBase Instance
        {
            get
            {
                // Double-checked locking for thread safety
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new TestBase();
                        }
                    }
                }

                return instance;
            }
        }
        #endregion
        private void InitializeHttpClient()
        {
            if (httpClient == null)
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appSettings["BaseUrl"]);
                httpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public static void ClearInstance()
        {
            lock (lockObject)
            {
                instance = null;
            }
        }

        public string GetAppParameter(string appSettingsParam)
        {
            return appSettings.GetValueOrDefault<string, string>(appSettingsParam);
        }

    }
}
