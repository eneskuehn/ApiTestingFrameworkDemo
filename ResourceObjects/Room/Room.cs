using ApiTestingFrameworkDemo.Support;
using ApiTestingFrameworkDemo.Models;
using System.Text;

namespace ApiTestingFrameworkDemo.ResourceObjects
{
    public class Room
    {
        private string resource = Endpoint.Room;

        public void GetRooms()
        {
            TestBase.Instance.httpClient.Get(resource);
        }

        public void GetRoom(string id)
        {
            TestBase.Instance.httpClient.Get(resource + id);
        }

        public void CreateRoom(RoomRequest roomRequest)
        {
            HttpContent content = new StringContent(roomRequest.PreparePayload(), Encoding.UTF8, "application/json");
            TestBase.Instance.httpClient.Post(resource, content);
        }

        public void UpdateRoom(int roomID, RoomRequest roomRequest)
        {
            HttpContent content = new StringContent(roomRequest.PreparePayload(), Encoding.UTF8, "application/json");
            TestBase.Instance.httpClient.Put(resource + roomID, content);
        }

        public void DeleteRoom(int roomID)
        {
            TestBase.Instance.httpClient.Delete(resource + roomID);
        }
    }
}
