namespace TouristAgency.Model
{
    public class Tourist : User
    {
        public ICollection<Reservation> reservations { get; set; }
    }
}
