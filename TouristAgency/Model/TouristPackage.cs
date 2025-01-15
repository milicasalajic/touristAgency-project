namespace TouristAgency.Model
{
    public class TouristPackage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime ReturnDate { get; set; }
        public double Price { get; set; }
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
        public Reservation Reservation { get; set; }








    }
}
