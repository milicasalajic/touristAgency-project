using TouristAgency.Model;

namespace TouristAgency.DTO.Responses
{
    public class AllTouristPackagesDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime ReturnDate { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public double BasePrice { get; set; }
        public Transportation Transportation { get; set; }
    }
}
