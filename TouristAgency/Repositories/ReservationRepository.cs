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
                // Dodavanje rezervacije u kontekst baze
                // await _dataContext.Reservations.AddAsync(reservation);
                _table.Add(reservation);
                // Čuvanje promena u bazi
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EntityInsertException<Reservation>();
            }
        }

        public async Task<Reservation> CreateReservationAsync(ReservationDTO reservationDTO)
        {
            // Pronalaženje turističkog paketa na osnovu ID-a
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

            // Dodavanje rezervacije u bazu
            await SaveAsync(reservation);

            return reservation;
        }
    }
   
}
