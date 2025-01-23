using TouristAgency.DTO.Requests;
using TouristAgency.Model;

namespace TouristAgency.RepositoryInterfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateReservationAsync(ReservationDTO reservationDTO);
        Task SaveAsync(Reservation reservation);
    }
}
