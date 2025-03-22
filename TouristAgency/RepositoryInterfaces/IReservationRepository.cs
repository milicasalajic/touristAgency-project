using TouristAgency.DTO.Requests;
using TouristAgency.DTO.Responses;
using TouristAgency.Model;
using TouristAgency.ResponseModel;

namespace TouristAgency.RepositoryInterfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateReservationAsync(ReservationDTO reservationDTO);
        Task SaveAsync(Reservation reservation);
        Task<List<Reservation>> GetReservationsByUserIdAsync(Guid userId);
        Task<List<Reservation>> GetReservationsByPackageIdAsync(Guid packageId, Guid organizerId);
    }
}
