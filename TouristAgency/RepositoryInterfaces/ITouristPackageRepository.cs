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
        Task<TouristPackage> FindByNameAsync(string name);
        Task<TouristPackage> AddAsync(TouristPackage package);
        Task<TouristPackage> TouristPackageExsist(string name);
        Task<List<TouristPackage>> GetPackagesByCreatorIdAsync(Guid creatorId);
        Task DeletePackageAsync(Guid packageId, Guid organizerId);
        Task UpdateAsync(TouristPackage package);
        Task AddTripAsync(Trip trip);
        Task UpdateTouristPackageAsync(TouristPackage package);
        Task UpdateDestinationAsync(Destination destination);
        Task<Destination> FindByDestinationIdAsync(Guid destinationId);
        Task<Trip> FindTripByIdAsync(Guid tripId);
        Task DeleteTripAsync(Guid tripId);
    }
}
