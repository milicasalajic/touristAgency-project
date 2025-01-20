using TouristAgency.DTO.Responses;
using TouristAgency.Repositories;
using TouristAgency.RepositoryInterfaces;
using TouristAgency.ServiceInterfaces;

namespace TouristAgency.Services
{
    public class TouristPackageService : ITouristPackageService
    {
        private readonly ITouristPackageRepository _touristPackageRepository;
        public TouristPackageService(ITouristPackageRepository touristPackageRepository)
        {
            _touristPackageRepository = touristPackageRepository;
        }
        public async Task<IEnumerable<AllTouristPackagesDTO>> GetAllAsync()
        {
            var packagesDTO = await _touristPackageRepository.GetAllAsync();
            return packagesDTO;
        }
    }
}
