using ApiTestingFrameworkDemo.Support;
using ApiTestingFrameworkDemo.Models;
using ApiTestingFrameworkDemo.ResourceObjects;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ApiTestingFrameworkDemo.StepDefinitions
{
    [Binding]
    public sealed class MessagesStepsDefinition
    {
        #region resources
        Health _health = new();
        Message _message = new();
        #endregion

        #region models
        MessageRequest _messageRequest = new();
        #endregion

        [Given(@"Message service is UP and running")]
        public void GivenMessageServiceIsUPAndRunning()
        {
            _health.CheckHealtStatusUp();
        }

        [Given(@"I send GET message request")]
        public void GivenISendGETMessageRequest()
        {
            _message.GetMessages();
        }

        [Then(@"I validate messages are returned")]
        public void ThenIValidateMessagesAreReturned()
        {
            _message.VerifyMessageCountIsNotZero();
        }

        [Given(@"I create a new message")]
        public void GivenICreateANewMessage(Table table)
        {
            _messageRequest = table.CreateInstance<MessageRequest>();
        }

        [When(@"I send POST message request")]
        public void WhenISendPOSTMessageRequest()
        {
            _message.PostMessage(_messageRequest);

        }

        [Then(@"I verify message is received")]
        public void ThenIVerifyMessageIsReceived()
        {
            _message.VerifyMessageReceived(_messageRequest);
        }
        
        [Given(@"I create messages and I verify status code is <(.*)>")]
        public void GivenICreateMessagesAndIVerifyStatusCodeIs(int expectedStatusCode, Table table)
        {
            var messagesForSending = table.CreateSet<MessageRequest>();

            foreach (var message in messagesForSending)
            {
                _message.PostMessage(message);
                Assert.AreEqual(expectedStatusCode, TestBase.Instance.testData.GetResponseStatusCode());
                _message.VerifyMessageReceived(message);
                
            }
           
        }
    }
}