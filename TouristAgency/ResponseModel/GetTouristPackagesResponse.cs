using TouristAgency.DTO.Responses;
using TouristAgency.Model;

namespace TouristAgency.ResponseModel
{
    public class GetTouristPackagesResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime ReturnDate { get; set; }
        public double BasePrice { get; set; }

    }
}
