namespace TouristAgency.RequestModel
{
    public class UpdateDestination
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Hotel { get; set; }
        public List<string>? HotelImages { get; set; } = new List<string>();

    }
}
