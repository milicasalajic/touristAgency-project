namespace TouristAgency.Model
{
    public class Organizer : User
    {
        public List<TouristPackage> PostedPackages { get; set; } = new List<TouristPackage>();
    }
}
