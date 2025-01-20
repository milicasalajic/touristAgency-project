using TouristAgency.DTO.Responses;

namespace TouristAgency.RepositoryInterfaces
{
    public interface ITouristPackageRepository
    {
        Task<IEnumerable<AllTouristPackagesDTO>> GetAllAsync();
    }
}
