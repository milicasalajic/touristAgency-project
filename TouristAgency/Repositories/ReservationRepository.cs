using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TouristAgency.Data;
using TouristAgency.DTO.Requests;
using TouristAgency.Exceptions;
using TouristAgency.Model;
using TouristAgency.RepositoryInterfaces;

namespace TouristAgency.Repositories
{

    public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        private readonly ITouristPackageRepository _touristPackageRepository;

        public ReservationRepository(DataContext dataContext, ITouristPackageRepository touristPackageRepository) : base(dataContext)
        {
            _touristPackageRepository = touristPackageRepository;
        }

        public async Task SaveAsync(Reservation reservation)
        {
            try
            {
                _table.Add(reservation);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GRESKA PRILIKOM CUVANJA: {ex.Message}");
                throw new EntityInsertException<Reservation>();
            }
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
                DiscountCode = string.IsNullOrWhiteSpace(reservationDTO.DiscountCode) ? "NO_DISCOUNT" : reservationDTO.DiscountCode

            };

            reservation.CalculateFinalPrice();
            
            await SaveAsync(reservation);

            return reservation;
        }

        public async Task<List<Reservation>> GetReservationsByUserIdAsync(Guid touristId)
        {
            return await _dataContext.Reservations
                                 .Where(r => r.Tourist.UserId == touristId)
                                 .ToListAsync();
        }

        public async Task<List<Reservation>> GetReservationsByPackageIdAsync(Guid packageId, Guid organizerId)
        {
            return await _dataContext.Reservations
                .Where(r => r.TouristPackage.Id == packageId && r.TouristPackage.Organizer.UserId == organizerId)
                .ToListAsync();
        }

    } 
}
