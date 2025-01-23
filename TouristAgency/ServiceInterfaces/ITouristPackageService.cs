using TouristAgency.DTO.Responses;

namespace TouristAgency.ServiceInterfaces
{
    public interface ITouristPackageService
    {
        Task<IEnumerable<AllTouristPackagesDTO>> GetAllAsync();
        Task<OneTouristPackageDTO> FindByIdAsync(Guid Id);
        Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesOrderedByPriceAsync();
        Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesOrderedByPriceDescendingAsync();
        Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
