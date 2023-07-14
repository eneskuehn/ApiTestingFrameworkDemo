namespace ApiTestingFrameworkDemo.Models
{
    public class RoomRequest
    {
        public string roomName { get; set; }
        public string type { get; set; }
        public bool accessible { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public int roomPrice { get; set; }
        public List<string> features { get; set; }
    }
}
