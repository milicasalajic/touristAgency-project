using TouristAgency.DTO.Requests;
using TouristAgency.Model;
using TouristAgency.RepositoryInterfaces;
using TouristAgency.ServiceInterfaces;

namespace TouristAgency.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ITouristPackageRepository _touristPackageRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(
            ITouristPackageRepository touristPackageRepository,
            IReservationRepository reservationRepository)
        {
            _touristPackageRepository = touristPackageRepository;
            _reservationRepository = reservationRepository;
        }
        public async Task<Reservation> CreateReservationAsync(ReservationDTO reservationDTO)
        {
            // Pretraga turističkog paketa na osnovu ID-a
            var touristPackage = await _touristPackageRepository.FindByIdAsync(reservationDTO.TouristPackageId);
            if (touristPackage == null)
            {
                throw new Exception("Tourist package not found.");
            }

            // Kreiranje nove rezervacije
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                TouristPackage = touristPackage,
                Name = reservationDTO.Name,
                LastName = reservationDTO.LastName,
                Email = reservationDTO.Email,
                PhoneNumber = reservationDTO.PhoneNumber,
                JMBG = reservationDTO.JMBG,
                OtherEmails = reservationDTO.OtherEmails,
                BedCount = reservationDTO.BedCount,
                ReservationDate = DateTime.Now,
                PaymentMethod = reservationDTO.PaymentMethod,
                DiscountCode = reservationDTO.DiscountCode
            };

            // Izračunavanje finalne cene
            reservation.CalculateFinalPrice();

            // Spremanje rezervacije u bazu
            await _reservationRepository.SaveAsync(reservation);

            return reservation;
        }

    }
}
