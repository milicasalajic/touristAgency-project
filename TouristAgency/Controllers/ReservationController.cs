using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.DTO.Requests;
using TouristAgency.ResponseModel;
using TouristAgency.ServiceInterfaces;
using TouristAgency.Exceptions;
using TouristAgency.Model;

namespace TouristAgency.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

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
                bool isAuthenticated = User.Identity.IsAuthenticated;
                bool isTourist = isAuthenticated && User.IsInRole("Tourist");

                if (isTourist)
                {
                    // Ako je korisnik prijavljen i ima ulogu "Tourist", dodeli mu DiscountCode
                    reservationDTO.DiscountCode = "DISCOUNT2024"; // Ovde možeš dodati logiku za generisanje ili dobijanje koda
                }
                else
                {
                    // Ako nije prijavljen, DiscountCode ostaje null
                    reservationDTO.DiscountCode = "NO_DISCOUNT";
                }
                var reservation = await _reservationService.CreateReservationAsync(reservationDTO);

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
                    PaymentMethod = reservation.PaymentMethod,
                    DiscountCode = reservation.DiscountCode,
                    OtherEmails = reservation.OtherEmails,
                    FinalPrice = (double)reservation.FinalPrice
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("my-reservations")]
        [Authorize(Roles = "Tourist")]
        public async Task<IActionResult> GetUserReservations()
        {
            var userIdString = User.FindFirst("id")?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized(new { Message = "Neuspešno prepoznavanje korisnika." });
            }
            
            var reservations = await _reservationService.GetReservationsByUserIdAsync(userId);

               var response = reservations.Select(reservation => new ReservationResponse
                {
                    Id = reservation.Id,
                    Name = reservation.Name,
                    LastName = reservation.LastName,
                    Email = reservation.Email,
                    PhoneNumber = reservation.PhoneNumber,
                    JMBG = reservation.JMBG,
                    BedCount = reservation.BedCount,
                    ReservationDate = reservation.ReservationDate,
                    TouristPackageId = reservation.TouristPackage.Id,
                    PaymentMethod = reservation.PaymentMethod,
                    DiscountCode = reservation.DiscountCode,
                    OtherEmails = reservation.OtherEmails,
                    FinalPrice = (double)reservation.FinalPrice
                }).ToList();

                return Ok(response);
        }

        [HttpGet("reservations-by-packages/{packageId}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetReservationsForPackage(Guid packageId)
        {
            var userIdString = User.FindFirst("id")?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out Guid organizerId))
            {
                return Unauthorized(new { Message = "Neuspešno prepoznavanje korisnika." });
            }

            var reservations = await _reservationService.GetReservationsByPackageIdAsync(packageId, organizerId);

            if (reservations == null || !reservations.Any())
            {
                throw new EntityNotFoundException<Reservation>();
            }
            var response = reservations.Select(reservation => new GetReservationsByTouristPackageResponse
            {
                Id = reservation.Id,
                Name = reservation.Name,
                LastName = reservation.LastName,
                Email = reservation.Email,
                PhoneNumber = reservation.PhoneNumber,
                JMBG = reservation.JMBG,
                BedCount = reservation.BedCount,
                ReservationDate = reservation.ReservationDate,
                PaymentMethod = reservation.PaymentMethod,
                DiscountCode = reservation.DiscountCode,
                OtherEmails = reservation.OtherEmails,
                FinalPrice = (double)reservation.FinalPrice
            }).ToList();

            return Ok(response);
        }


    }
}
