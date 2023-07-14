using ApiTestingFrameworkDemo.Support;
using ApiTestingFrameworkDemo.Models;
using ApiTestingFrameworkDemo.ResourceObjects;
using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ApiTestingFrameworkDemo.StepDefinitions
{
    [Binding]
    public class RoomsStepDefinitions
    {
        Room _room = new();
        RoomRequest _roomRequest = new();
        RoomRequest _updatedRoom = new();

        [Given(@"I send GET all rooms request")]
        public void GivenISendGETRoomsRequest()
        {
            _room.GetRooms();
        }

        [Then(@"I validate rooms are returned")]
        public void ThenIValidateRoomsAreReturned()
        {
            Assert.AreEqual(200, TestBase.Instance.testData.GetResponseStatusCode());
            var responseJson = TestBase.Instance.testData.GetResponseJson().ToString();
            RoomsResponse response = JsonSerializer.Deserialize<RoomsResponse>(responseJson);
            RoomPayload room = response.rooms[0];

            Assert.IsTrue(room.RoomId >= 1);
            Assert.IsNotNull(room.RoomName);
            Assert.IsNotNull(room.Type);
            Assert.IsTrue(room.Features.Count() > 0);
            Assert.IsTrue(room.RoomPrice > 0);
        }

        [When(@"I get a highest room id number")]
        public void WhenIGetAHighestRoomIdNumber()
        {
            var responseJson = TestBase.Instance.testData.GetResponseJson().ToString();
            RoomsResponse response = JsonSerializer.Deserialize<RoomsResponse>(responseJson);
            int maxRoomId = response.rooms.Max(room => room.RoomId);
            Assert.IsTrue(maxRoomId > 0);
            TestBase.Instance.testData.Add(nameof(maxRoomId), maxRoomId);
        }

        [Then(@"I send GET a room request")]
        public void ThenISendGETARoomRequest()
        {
            _room.GetRoom(TestBase.Instance.testData.Get("maxRoomId").ToString());
        }

        [Then(@"I validate room is returned")]
        public void ThenIValidateRoomIsReturned()
        {
            RoomPayload response = JsonSerializer.Deserialize<RoomPayload>(TestBase.Instance.testData.GetResponseJson().ToString());
            Assert.IsTrue(response.RoomId == (int)TestBase.Instance.testData.Get("maxRoomId"));
            Assert.IsNotNull(response.RoomName);
            Assert.IsNotNull(response.Type);
            Assert.IsTrue(response.Features.Count() > 0);
            Assert.IsTrue(response.RoomPrice > 0);
        }

        [Given(@"I create a new room")]
        public void GivenICreateANewRoom(Table table)
        {
            _roomRequest = table.CreateInstance<RoomRequest>();
        }

        [Given(@"I add room features")]
        [Then(@"I add room features")]
        public void ThenIAddRoomFeatures(Table table)
        {
            _roomRequest.features = new();
            foreach (var row in table.Rows)
            {
                string feature = row["features"];
                _roomRequest.features.Add(feature);
            }
        }

        [When(@"I send POST room request")]
        public void WhenISendPOSTRoomRequest()
        {
            _room.CreateRoom(_roomRequest);
        }

        [Then(@"I validate room is created and available")]
        public void ThenIValidateRoomIsCreatedAndAvailable()
        {
            Assert.AreEqual(201, TestBase.Instance.testData.GetResponseStatusCode());
            //check response payload
            RoomPayload createdRoom = JsonConvert.DeserializeObject<RoomPayload>(TestBase.Instance.testData.GetResponseJson().ToString());
            Assert.AreEqual(createdRoom.RoomName, _roomRequest.roomName);
            Assert.AreEqual(createdRoom.Type, _roomRequest.type);
            Assert.AreEqual(createdRoom.Accessible, _roomRequest.accessible);
            Assert.AreEqual(createdRoom.Image, _roomRequest.image);
            Assert.AreEqual(createdRoom.Description, _roomRequest.description);
            Assert.AreEqual(createdRoom.RoomPrice, _roomRequest.roomPrice);
            Assert.AreEqual(createdRoom.Features, _roomRequest.features);

            //check get message payload
            _room.GetRoom(createdRoom.RoomId.ToString());
            RoomPayload receivedRoom = JsonConvert.DeserializeObject<RoomPayload>(TestBase.Instance.testData.GetResponseJson().ToString());
            Assert.AreEqual(receivedRoom.RoomId, createdRoom.RoomId);
            Assert.AreEqual(receivedRoom.RoomName, createdRoom.RoomName);
            Assert.AreEqual(receivedRoom.Type, createdRoom.Type);
            Assert.AreEqual(receivedRoom.Accessible, createdRoom.Accessible);
            Assert.AreEqual(receivedRoom.Image, createdRoom.Image);
            Assert.AreEqual(receivedRoom.Description, createdRoom.Description);
            Assert.AreEqual(receivedRoom.Features, createdRoom.Features);
            Assert.AreEqual(receivedRoom.RoomPrice, createdRoom.RoomPrice);
        }

        [Then(@"I update the room")]
        public void ThenIUpdateTheRoom(Table table)
        {
            _updatedRoom = table.CreateInstance<RoomRequest>();
        }

        [Then(@"I add updated room features")]
        public void ThenIAddUpdatedRoomFeatures(Table table)
        {
            _updatedRoom.features = new();
            foreach (var row in table.Rows)
            {
                string feature = row["features"];
                _updatedRoom.features.Add(feature);
            }
        }

        [When(@"I send PUT room request")]
        public void WhenISendPUTRoomRequest()
        {
            RoomPayload receivedRoom = JsonConvert.DeserializeObject<RoomPayload>(TestBase.Instance.testData.GetResponseJson().ToString());
            _room.UpdateRoom(receivedRoom.RoomId, _updatedRoom);
        }

        [Then(@"I validate room is updated and available")]
        public void ThenIValidateRoomIsUpdatedAndAvailable()
        {
            Assert.AreEqual(202, TestBase.Instance.testData.GetResponseStatusCode());
            //check response payload
            RoomPayload updatedRoom = JsonConvert.DeserializeObject<RoomPayload>(TestBase.Instance.testData.GetResponseJson().ToString());
            Assert.AreEqual(updatedRoom.RoomName, _updatedRoom.roomName);
            Assert.AreEqual(updatedRoom.Type, _updatedRoom.type);
            Assert.AreEqual(updatedRoom.Accessible, _updatedRoom.accessible);
            Assert.AreEqual(updatedRoom.Image, _updatedRoom.image);
            Assert.AreEqual(updatedRoom.Description, _updatedRoom.description);
            Assert.AreEqual(updatedRoom.RoomPrice, _updatedRoom.roomPrice);
            Assert.AreEqual(updatedRoom.Features, _updatedRoom.features);

            //check get message payload
            _room.GetRoom(updatedRoom.RoomId.ToString());
            RoomPayload receivedRoom = JsonConvert.DeserializeObject<RoomPayload>(TestBase.Instance.testData.GetResponseJson().ToString());
            Assert.AreEqual(receivedRoom.RoomId, updatedRoom.RoomId);
            Assert.AreEqual(receivedRoom.RoomName, updatedRoom.RoomName);
            Assert.AreEqual(receivedRoom.Type, updatedRoom.Type);
            Assert.AreEqual(receivedRoom.Accessible, updatedRoom.Accessible);
            Assert.AreEqual(receivedRoom.Image, updatedRoom.Image);
            Assert.AreEqual(receivedRoom.Description, updatedRoom.Description);
            Assert.AreEqual(receivedRoom.Features, updatedRoom.Features);
            Assert.AreEqual(receivedRoom.RoomPrice, updatedRoom.RoomPrice);
        }

        [Then(@"I delete the updated room")]
        public void ThenIDeleteTheUpdatedRoom()
        {
            RoomPayload updatedRoom = JsonConvert.DeserializeObject<RoomPayload>(TestBase.Instance.testData.GetResponseJson().ToString());
            _room.DeleteRoom(updatedRoom.RoomId);
        }

    }
}
