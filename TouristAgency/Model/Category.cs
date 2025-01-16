namespace TouristAgency.Model
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Kolekcija turističkih paketa koji pripadaju kategoriji
        public ICollection<TouristPackage> TouristPackages { get; set; }

    }
}
