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
                    // .Include(tp => tp.Organizer)
                     //.Include(tp => tp.Tourists) // Ovo ostaje, iako lista može biti prazna
                     //.Include(tp => tp.Transportation)
                     .FirstOrDefaultAsync(tp => tp.Id == id);
            }
            catch (Exception)
            {
                throw new DataRetrievalException<TouristPackage>();
            }
        }
        public async Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesOrderedByPriceAsync()
        {
            try
            {
                return await _table
                    .OrderBy(p => p.BasePrice) // Sortiranje po osnovnoj ceni (rastuce)
                    .Select(item => new AllTouristPackagesDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        DateOfDeparture = item.DateOfDeparture,
                        ReturnDate = item.ReturnDate,
                        BasePrice = item.BasePrice,
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
                .OrderByDescending(p => p.BasePrice) // Sortiranje po ceni opadajuće
                .Select(item => new AllTouristPackagesDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    DateOfDeparture = item.DateOfDeparture,
                    ReturnDate = item.ReturnDate,
                    BasePrice = item.BasePrice,
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
                })
                .ToListAsync();
        }
    }
}
