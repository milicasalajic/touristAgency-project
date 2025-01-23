using TouristAgency.DTO.Responses;
using TouristAgency.Model;

namespace TouristAgency.RepositoryInterfaces
{
    public interface ITouristPackageRepository
    {
        Task<IEnumerable<AllTouristPackagesDTO>> GetAllAsync();
        Task<TouristPackage> FindByIdAsync(Guid Id);
        Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesOrderedByPriceAsync();
        Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesOrderedByPriceDescendingAsync();
        Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
