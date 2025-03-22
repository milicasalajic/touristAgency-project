using System.Text.Json.Serialization;
using TouristAgency.Model;

namespace TouristAgency.RequestModel
{
    public class RegistrationRequest
    {

        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}
