using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace TouristAgency.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public TouristPackage TouristPackage { get; set; }
        public User User { get; set; }
        public int NumberOfTourists { get; set; }
        public DateTime ReservationDate { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string JMBG { get; set; }
        public string PlaceOfEntryIntoTheBus { get; set; }
        public List<string> otherEmails { get; set; } = new List<string>();
        public PaymentMethod PaymentMethod { get; set; }
        public string DiscountCode { get; set; }



    }
}
