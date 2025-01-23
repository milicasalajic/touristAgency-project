using TouristAgency.DTO.Requests;
using TouristAgency.Model;

namespace TouristAgency.ServiceInterfaces
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(ReservationDTO reservationDTO);
    }
}
