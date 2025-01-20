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
    }
}
