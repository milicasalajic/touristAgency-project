using TouristAgency.Model;

namespace TouristAgency.DTO.Responses
{
    public class OneTouristPackageDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime ReturnDate { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public string Schedule { get; set; }
        public string PriceIncludes { get; set; }
        public string PriceDoesNotIncludes { get; set; }
        //public Category Category { get; set; }
        public CategoryForTorusitPackageDTO Category { get; set; }
        public Destination Destination { get; set; }
        public List<TripForTouristPackageDTO> Trips { get; set; }
       
        public Transportation Transportation { get; set; }
        public double BasePrice { get; set; } // Osnovna cena paketa

        // Cena po vrsti sobe (index: 0 - dvokrevetna, 1 - trokrevetna, 2 - četvorokrevetna)
        public List<double> RoomPrices { get; set; } = new List<double>(); // Lista cena za različite sobe (po indeksima)

    }
}
