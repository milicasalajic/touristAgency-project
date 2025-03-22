using TouristAgency.DTO.Requests;
using TouristAgency.DTO.Responses;
using TouristAgency.Model;
using TouristAgency.RequestModel;
using TouristAgency.ResponseModel;

namespace TouristAgency.ServiceInterfaces
{
    public interface ITouristPackageService
    {
        Task<IEnumerable<AllTouristPackagesDTO>> GetAllAsync();
        Task<OneTouristPackageDTO> FindByIdAsync(Guid Id);
        Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesOrderedByPriceAsync();
        Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesOrderedByPriceDescendingAsync();
        Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<InsertTouristPackageResponseDTO> AddTouristPackageAsync(InsertTouristPackageRequestDTO torustPackageRequestDTO, Guid userId);
        Task<List<OneTouristPackageDTO>> GetPackagesByCreatorIdAsync(Guid creatorId);
        Task DeletePackageAsync(Guid packageId, Guid organizerId);
        Task<bool> UpdateTouristPackageStatusAsync(Guid packageId, TouristPackageStatus status);
        Task AddTripAsync(Trip trip);
        Task<bool> UpdateTouristPackageAsync(Guid packageId, UpdateTouristPackage request);
        Task<bool> UpdateDestinationAsync(Guid destinationId, UpdateDestination request);
        Task DeleteTripAsync(Guid tripId);
    }
}
