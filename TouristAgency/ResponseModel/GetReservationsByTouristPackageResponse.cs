using TouristAgency.Model;

namespace TouristAgency.ResponseModel
{
    public class GetReservationsByTouristPackageResponse
    {
        public Guid Id { get; set; }
        public int BedCount { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JMBG { get; set; }
        public List<string> OtherEmails { get; set; } = new List<string>();
        public PaymentMethod PaymentMethod { get; set; }
        public string DiscountCode { get; set; }
        public double FinalPrice { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
