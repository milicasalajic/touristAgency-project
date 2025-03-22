using TouristAgency.DTO.Requests;
using TouristAgency.DTO.Responses;
using TouristAgency.Model;
using TouristAgency.ResponseModel;

namespace TouristAgency.ServiceInterfaces
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(ReservationDTO reservationDTO);
        Task<List<Reservation>> GetReservationsByUserIdAsync(Guid userId);
        Task<List<ReservationsByTouristPackageResponseDTO>> GetReservationsByPackageIdAsync(Guid packageId, Guid organizerId);
    }

}
