namespace TouristAgency.Model
{
    public class Organizer : User
    {
               //kad zelimo fleskibilnsot s tipom kolekcije
        public ICollection<TouristPackage> touristPackages { get; set; }
    }
}
