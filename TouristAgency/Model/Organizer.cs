namespace TouristAgency.Model
{
    public class Organizer : User
    {
        public ICollection<TouristPackage> touristPackages { get; set; }
    }
}
