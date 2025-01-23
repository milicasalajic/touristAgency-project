using TouristAgency.DTO.Responses;
using TouristAgency.Exceptions;
using TouristAgency.Model;
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
        public async Task<OneTouristPackageDTO> FindByIdAsync(Guid Id)
        {
            var package = await _touristPackageRepository.FindByIdAsync(Id);
            if (package == null)
            {
                throw new EntityNotFoundException<TouristPackage>();
            }
            var touristPackageDTO = new OneTouristPackageDTO()
            {
                Id = package.Id,
                Name = package.Name,
                Description = package.Description,
                Duration = package.Duration,
                DateOfDeparture = package.DateOfDeparture,
                ReturnDate = package.ReturnDate,
                Images = package.Images, // assuming Images is a List<string> in your TouristPackage model
                Schedule = package.Schedule,
                PriceIncludes = package.PriceIncludes,
                PriceDoesNotIncludes = package.PriceDoesNotIncludes,
                Category = new CategoryForTorusitPackageDTO
                {
                    Id = package.Category.Id,
                    Name = package.Category.Name
                },
                Trips = package.Trips?.Select(trip => new TripForTouristPackageDTO
                {
                    Id = trip.Id,
                    Name = trip.Name,
                    Description = trip.Description,
                }).ToList(),
                // Category = package.Category, // assuming Category is an object with necessary details
                Destination = package.Destination, // assuming Destination is an object with necessary details
                // Trips = package.Trips, // assuming Trips is an ICollection of trips
             
              //  Organizer = package.Organizer, // assuming Organizer is an object with necessary details
                //Tourists = package.Tourists, // assuming Tourists is an ICollection of tourists
                Transportation = package.Transportation, // assuming Transportation is an object with necessary details
                BasePrice = package.BasePrice,
                RoomPrices = package.RoomPrices // assuming RoomPrices is a List<double>
            };
            return touristPackageDTO;
        }
        public async Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesOrderedByPriceAsync()
        {
            return await _touristPackageRepository.GetPackagesOrderedByPriceAsync();
        }
        public async Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesOrderedByPriceDescendingAsync()
        {
            return await _touristPackageRepository.GetPackagesOrderedByPriceDescendingAsync();
        }
        public async Task<IEnumerable<AllTouristPackagesDTO>> GetPackagesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _touristPackageRepository.GetPackagesByDateRangeAsync(startDate, endDate);
        }

    }
}
