using ApiTestingFrameworkDemo.Support;
using ApiTestingFrameworkDemo.ResourceObjects.Auth;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ApiTestingFrameworkDemo.StepDefinitions
{
    [Binding]
    public sealed class CommonStepsDefinition
    {
        private Auth _auth = new Auth();

        [Given(@"User is authenticated as administrator")]
        public void GivenUserIsAuthenticatedAsAdministrator()
        {
            _auth.CreateToken();
        }

        [Then(@"veirify status code is (.*)")]
        public void ThenVeirifyStatusCodeIs(int p0)
        {
            Assert.AreEqual(p0, TestBase.Instance.testData.GetResponseStatusCode());
        }
    }
}