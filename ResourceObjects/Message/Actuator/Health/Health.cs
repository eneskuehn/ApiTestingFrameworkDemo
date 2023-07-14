using ApiTestingFrameworkDemo.Support;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace ApiTestingFrameworkDemo.ResourceObjects
{
    public class Health
    {
        private string resource = Endpoint.MessageHealth;

        public void CheckHealtStatusUp()
        {
            TestBase.Instance.httpClient.Get(resource);
            JObject jsonObject = JObject.Parse(TestBase.Instance.testData.GetResponseJson().ToString());
            Assert.AreEqual(jsonObject.GetValue("status").Value<string>(), "UP");
        }
    }
}
