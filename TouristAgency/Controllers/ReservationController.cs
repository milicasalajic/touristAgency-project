using Microsoft.AspNetCore.Mvc;
using TouristAgency.DTO.Requests;
using TouristAgency.ResponseModel;
using TouristAgency.ServiceInterfaces;

namespace TouristAgency.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        // Konstruktor za injektovanje servisa
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // Kreiranje nove rezervacije
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationDTO reservationDTO)
        {
            try
            {
                // Kreiraj novu rezervaciju
                var reservation = await _reservationService.CreateReservationAsync(reservationDTO);

                // Kreiranje odgovora
                var response = new ReservationResponse
                {
                    Id = reservation.Id,
                    Name = reservation.Name,
                    LastName = reservation.LastName,
                    Email = reservation.Email,
                    PhoneNumber = reservation.PhoneNumber,
                    JMBG = reservation.JMBG,
                    BedCount = reservation.BedCount,
                    // FinalPrice = reservation.FinalPrice,
                    ReservationDate = reservation.ReservationDate,
                    TouristPackageId = reservation.TouristPackage.Id,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Vraćanje greške u slučaju problema
                return BadRequest(new { Message = ex.Message });
            }
        }
    }

    }
