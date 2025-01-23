using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace TouristAgency.Model
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public TouristPackage TouristPackage { get; set; }
        public Tourist Tourist { get; set; }
        public int BedCount { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string JMBG { get; set; }
        public List<string> OtherEmails { get; set; } = new List<string>();
        public PaymentMethod PaymentMethod { get; set; }
        public string DiscountCode { get; set; }
        public double? FinalPrice { get; set; } // Krajnja cena
        public void CalculateFinalPrice()
        {
            double roomPrice = TouristPackage.GetRoomPrice(BedCount);
            FinalPrice = roomPrice;
        }



    }
}
