using ApiTestingFrameworkDemo.Support;
using ApiTestingFrameworkDemo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Text;

namespace ApiTestingFrameworkDemo.ResourceObjects
{
    public class Message
    {
        private string resource = Endpoint.Message;

        public void GetMessages()
        {
            TestBase.Instance.httpClient.Get(resource);
        }
        public void GetMessage(string id)
        {
            TestBase.Instance.httpClient.Get(resource + id);
        }
        public void PostMessage(MessageRequest payload)
        {
            HttpContent content = new StringContent(payload.PreparePayload(), Encoding.UTF8, "application/json");
            TestBase.Instance.httpClient.Post(resource, content);
        }
        public void VerifyMessageCountIsNotZero()
        {
            JObject jsonObject = JObject.Parse(TestBase.Instance.testData.GetResponseJson().ToString());
            var messages = jsonObject.SelectToken("messages");

            if (messages != null && messages.Type != JTokenType.Null)
            {
                Console.WriteLine("The 'messages' property exists and is not null.");
            }
            else
            {
                Assert.Fail("ERROR: Messages not displayed in the result!");
            }
        }
        public void VerifyMessageReceived(MessageRequest request)
        {
            //check response payload
            MessageRequest cratedMessage = JsonConvert.DeserializeObject<MessageRequest>(TestBase.Instance.testData.GetResponseJson().ToString());
            Assert.AreEqual(request.name, cratedMessage.name);
            Assert.AreEqual(request.email, cratedMessage.email);
            Assert.AreEqual(request.phone, cratedMessage.phone);
            Assert.AreEqual(request.subject, cratedMessage.subject);
            Assert.AreEqual(request.description, cratedMessage.description);

            //check get message payload
            GetMessage(cratedMessage.messageid.ToString());
            MessageRequest receivedMessage = JsonConvert.DeserializeObject<MessageRequest>(TestBase.Instance.testData.GetResponseJson().ToString());
            Assert.AreEqual(receivedMessage.messageid, cratedMessage.messageid);
            Assert.AreEqual(receivedMessage.name, cratedMessage.name);
            Assert.AreEqual(receivedMessage.email, cratedMessage.email);
            Assert.AreEqual(receivedMessage.phone, cratedMessage.phone);
            Assert.AreEqual(receivedMessage.subject, cratedMessage.subject);
            Assert.AreEqual(receivedMessage.description, cratedMessage.description);

        }
    }
}
