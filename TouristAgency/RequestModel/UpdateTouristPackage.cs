using TouristAgency.Model;

namespace TouristAgency.RequestModel
{
    public class UpdateTouristPackage
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public DateTime? DateOfDeparture { get; set; }
        public DateTime? ReturnDate { get; set; }
        public List<string>? Images { get; set; } = new List<string>();
        public string? Schedule { get; set; }
        public string? PriceIncludes { get; set; }
        public string? PriceDoesNotIncludes { get; set; }
        public Transportation? Transportation { get; set; }

    }
}
