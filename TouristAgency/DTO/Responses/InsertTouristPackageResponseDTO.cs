using TouristAgency.Model;

namespace TouristAgency.DTO.Responses
{
    public class InsertTouristPackageResponseDTO
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Capacity { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public string Schedule { get; set; }
        public string PriceIncludes { get; set; }
        public string PriceDoesNotIncludes { get; set; }
        public Guid CategoryId { get; set; }

        public Organizer Organizer { get; set; }
        public ICollection<Tourist> Tourists { get; set; }
        public Transportation Transportation { get; set; }
        public Destination Destination { get; set; }
        public double BasePrice { get; set; } // Osnovna cena paketa
        public TouristPackageStatus Status { get; set; }

        // Cena po vrsti sobe (index: 0 - dvokrevetna, 1 - trokrevetna, 2 - četvorokrevetna)
        public List<double> RoomPrices { get; set; } = new List<double>();
    }
}
