namespace TouristAgency.Model
{
    public class Destination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Hotel { get; set; }
        public List<string> HotelImages { get; set; } = new List<string>();
        public TouristPackage TouristPackage { get; set; }

    }
}
