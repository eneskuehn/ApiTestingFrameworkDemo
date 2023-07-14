namespace ApiTestingFrameworkDemo.Models
{
    public class MessageRequest
    {
        public int? id { get; set; }
        public int? messageid { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
    }
}
