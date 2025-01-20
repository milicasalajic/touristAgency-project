using TouristAgency.DTO.Responses;
using TouristAgency.Model;

namespace TouristAgency.RepositoryInterfaces
{
    public interface ITouristPackageRepository
    {
        Task<IEnumerable<AllTouristPackagesDTO>> GetAllAsync();
        Task<TouristPackage> FindByIdAsync(Guid Id);
    }
}
