using TouristAgency.DTO.Responses;

namespace TouristAgency.ServiceInterfaces
{
    public interface ITouristPackageService
    {
        Task<IEnumerable<AllTouristPackagesDTO>> GetAllAsync();
    }
}
