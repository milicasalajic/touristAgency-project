namespace TouristAgency.Model
{
    public class TouristPackage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Capacity { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public string Schedule {  get; set; }
        public string PriceIncludes { get; set; }
        public string PriceDoesNotIncludes { get; set; }
        public Category Category { get; set; }
        public Destination Destination { get; set; }
        public ICollection<Trip> Trips { get; set; }
        public Organizer Organizer { get; set; }
        public ICollection<Tourist> Tourists { get; set; }
        public Transportation Transportation { get; set; }
        public double BasePrice { get; set; } // Osnovna cena paketa
        public TouristPackageStatus Status { get; set; }

        // Cena po vrsti sobe (index: 0 - dvokrevetna, 1 - trokrevetna, 2 - četvorokrevetna)
        public List<double> RoomPrices { get; set; } = new List<double>(); // Lista cena za različite sobe (po indeksima)
                                                                           // Metoda koja vraća cenu sobe na osnovu broja kreveta
        public double GetRoomPrice(int bedCount)
        {
            // Ako paket podržava samo dvokrevetnu sobu, uvek vrati cenu za dvokrevetnu sobu
            if (RoomPrices.Count > 0 && bedCount == 2)
                return RoomPrices[0]; // Dvokrevetna

            // Ako paket podržava trokrevetnu sobu, vrati cenu za trokrevetnu
            if (RoomPrices.Count > 1 && bedCount == 3)
                return RoomPrices[1]; // Trokrevetna

            // Ako paket podržava četvorokrevetnu sobu, vrati cenu za četvorokrevetnu
            if (RoomPrices.Count > 2 && bedCount == 4)
                return RoomPrices[2]; // Četvorokrevetna

            // Ako cena nije definisana za traženi broj kreveta, vrati osnovnu cenu
            return BasePrice;
        }



    }
}
