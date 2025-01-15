namespace TouristAgency.Model
{
    public class Tourist : User
    {
        public List<TouristPackage>  ReservedTouristPackages{ get; set; } = new List<TouristPackage>();
    }
}
