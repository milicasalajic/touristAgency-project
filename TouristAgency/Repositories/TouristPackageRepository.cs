using Microsoft.EntityFrameworkCore;
using TouristAgency.Data;
using TouristAgency.DTO.Responses;
using TouristAgency.Exceptions;
using TouristAgency.Model;
using TouristAgency.RepositoryInterfaces;

namespace TouristAgency.Repositories
{
    public class TouristPackageRepository : RepositoryBase<TouristPackage>, ITouristPackageRepository
    {
        public TouristPackageRepository(DataContext dataContext) : base(dataContext)
        {
        }
        public async Task<IEnumerable<AllTouristPackagesDTO>> GetAllAsync()
        {
            try
            {                               //sekejtuj sve stavke iz tabele i za njih kreiraj dto objekat
                return await _table.Select(item => new AllTouristPackagesDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    DateOfDeparture=item.DateOfDeparture,
                    ReturnDate = item.ReturnDate,
                    BasePrice = item.BasePrice, 
                    Images = item.Images,
                    Transportation = item.Transportation,
                }).ToListAsync();
            }
            catch (Exception)
            {
                throw new DataRetrievalException<TouristPackage>();
            }
        }

        public async Task<TouristPackage> FindByIdAsync(Guid id)
        {
            try
            {
                return await _table
                     .Include(tp => tp.Category)
                     .Include(tp => tp.Destination)
                     .Include(tp => tp.Trips)
                     .Include(tp=> tp.Organizer)//ovo je dodato pri brisanju paketa, ako dodje do gresaka oguce da je zbog ove
                     .FirstOrDefaultAsync(tp => tp.Id == id);
            }
            catch (Exception)
            {
                throw new DataRetrievalException<TouristPackage>();
            }
        }

        public async Task<Destination> FindByDestinationIdAsync(Guid id)
        {
            try
            {
                return await _table
                    .OfType<Destination>()  // Ako je _table kolekcija različitih entiteta, koristi ovu liniju za filtriranje
                    .FirstOrDefaultAsync(tp => tp.Id == id);
            }
            catch (Exception)
            {
                throw new DataRetrievalException<Destination>(); // Koristi Destination umesto TouristPackage
            }
        }
        public async Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesOrderedByPriceAsync()
        {
            try
            {
                return await _table
                    .OrderBy(p => p.BasePrice) 
                    .Select(item => new AllTouristPackagesDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        DateOfDeparture = item.DateOfDeparture,
                        ReturnDate = item.ReturnDate,
                        BasePrice = item.BasePrice,
                        Images = item.Images,
                        Transportation = item.Transportation,
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw new DataRetrievalException<TouristPackage>();
            }
        }
        
        public async Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesOrderedByPriceDescendingAsync()
        {
            return await _table
                .OrderByDescending(p => p.BasePrice)
                .Select(item => new AllTouristPackagesDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    DateOfDeparture = item.DateOfDeparture,
                    ReturnDate = item.ReturnDate,
                    BasePrice = item.BasePrice,
                    Images = item.Images,
                    Transportation = item.Transportation,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _table
                .Where(p => p.DateOfDeparture >= startDate && p.ReturnDate <= endDate) // Filtriraj po opsegu datuma
                .Select(item => new AllTouristPackagesDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    DateOfDeparture = item.DateOfDeparture,
                    ReturnDate = item.ReturnDate,
                    BasePrice = item.BasePrice,
                    Images = item.Images,
                    Transportation = item.Transportation,
                })
                .ToListAsync();
        }
        public async Task<TouristPackage> FindByNameAsync(string name)
        {
            try
            {
                return await _table.FirstOrDefaultAsync(x => x.Name == name);
            }
            catch (Exception)
            {
                throw new SingleEntityRetrievalException<TouristPackage>();
            }
        }
        public async Task<TouristPackage> TouristPackageExsist(string name)
        {
            
                return await _table.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<TouristPackage> AddAsync(TouristPackage package)
        {
            
                await _table.AddAsync(package);
                await _dataContext.SaveChangesAsync();
                return package;
            
        }
        public async Task<List<TouristPackage>> GetPackagesByCreatorIdAsync(Guid creatorId)
        {
            return await _dataContext.TouristPackages
                .Include(p => p.Category)
                .Include(p => p.Destination)
                .Include(p => p.Trips)
                .Where(p => p.Organizer.UserId == creatorId)
                .ToListAsync();
        }
        public async Task DeletePackageAsync(Guid packageId, Guid organizerId)
        {
            var package = await FindByIdAsync(packageId);

            if (package == null)
            {
                throw new EntityNotFoundException<TouristPackage>();
            }

            // Proveravamo da li je organizator vlasnik paketa
            if (package.Organizer.UserId != organizerId)
            {
                throw new UnauthorizedAccessException("Nemate pravo da obrišete ovaj paket.");
            }

            _dataContext.TouristPackages.Remove(package);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TouristPackage package)
        {
            _dataContext.TouristPackages.Update(package);
            await _dataContext.SaveChangesAsync();
        }

        public async Task AddTripAsync(Trip trip)
        {
            if (trip == null)
            {
                throw new ArgumentNullException(nameof(trip), "Trip cannot be null.");
            }

            // Dodavanje trip objekta u DbContext
            await _dataContext.Trips.AddAsync(trip);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateTouristPackageAsync(TouristPackage package)
        {
            _dataContext.TouristPackages.Update(package);
            await _dataContext.SaveChangesAsync();

        }
        public async Task UpdateDestinationAsync(Destination destination)
        {
            _dataContext.Destinations.Update(destination);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Trip> FindTripByIdAsync(Guid tripId)
        {
            return await _dataContext.Trips.FindAsync(tripId);
        }

        public async Task DeleteTripAsync(Guid tripId)
        {
            var trip = await _dataContext.Trips.FindAsync(tripId);
            if (trip != null)
            {
                _dataContext.Trips.Remove(trip);
                await _dataContext.SaveChangesAsync();
            }
        }

    }
}
