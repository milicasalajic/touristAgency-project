using System.Text.Json.Serialization;
using TouristAgency.Model;

namespace TouristAgency.DTO.Requests
{
    public class ReservationDTO
    {
        public Guid TouristPackageId { get; set; }
        public int BedCount { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JMBG { get; set; }
        public List<string> OtherEmails { get; set; } = new List<string>();

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }
        public string DiscountCode { get; set; }
        

    }
}
