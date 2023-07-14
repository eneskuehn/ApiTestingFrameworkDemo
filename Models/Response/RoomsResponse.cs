using System.Text.Json.Serialization;

namespace ApiTestingFrameworkDemo.Models
{
    public class RoomsResponse
    {
        public RoomPayload[] rooms { get; set; }
    }

    public class RoomPayload
    {
        [JsonPropertyName("roomid")]
        public int RoomId { get; set; }

        [JsonPropertyName("roomName")]
        public string RoomName { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("accessible")]
        public bool Accessible { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("features")]
        public string[] Features { get; set; }

        [JsonPropertyName("roomPrice")]
        public decimal RoomPrice { get; set; }
    }
}
