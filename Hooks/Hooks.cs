using ApiTestingFrameworkDemo.Support;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ApiTestingFrameworkDemo.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        
        [BeforeScenario]
        public void BeforeScenario()
        {
            TestBase.Instance.testData.Add("TestName", TestContext.CurrentContext.Test.FullName);
        }


        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Test Data used:");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            foreach (KeyValuePair<object, object> kvp in TestBase.Instance.testData)
            {
                Console.WriteLine($"Key: {kvp.Key.ToString()}, Value: {kvp.Value.ToString()}");
            }
            Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            TestBase.Instance.testData.Clear();
            TestBase.ClearInstance();
        }
    }
}