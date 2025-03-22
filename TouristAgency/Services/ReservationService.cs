using TouristAgency.DTO.Requests;
using TouristAgency.DTO.Responses;
using TouristAgency.Model;
using TouristAgency.RepositoryInterfaces;
using TouristAgency.ResponseModel;
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
            var touristPackage = await _touristPackageRepository.FindByIdAsync(reservationDTO.TouristPackageId);
            if (touristPackage == null)
            {
                throw new Exception("Tourist package not found.");
            }

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

            reservation.CalculateFinalPrice();

            await _reservationRepository.SaveAsync(reservation);

            return reservation;
        }

        public async Task<List<Reservation>> GetReservationsByUserIdAsync(Guid touristId)
        {
            return await _reservationRepository.GetReservationsByUserIdAsync(touristId);
        }

        public async Task<List<ReservationsByTouristPackageResponseDTO>> GetReservationsByPackageIdAsync(Guid packageId, Guid organizerId)
        {
            var reservations = await _reservationRepository.GetReservationsByPackageIdAsync(packageId, organizerId);

            if (reservations == null || !reservations.Any())
            {
                return null;
            }
  
            var response = reservations.Select(reservation => new ReservationsByTouristPackageResponseDTO
            {
                Id = reservation.Id,
                Name = reservation.Name,
                LastName = reservation.LastName,
                Email = reservation.Email,
                PhoneNumber = reservation.PhoneNumber,
                JMBG = reservation.JMBG,
                OtherEmails = reservation.OtherEmails,
                PaymentMethod = reservation.PaymentMethod,
                DiscountCode = reservation.DiscountCode,
                FinalPrice = (double)reservation.FinalPrice,
                ReservationDate = reservation.ReservationDate,
                BedCount = reservation.BedCount
            }).ToList();

            return response;
        }
    
    }
}
