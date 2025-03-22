using TouristAgency.DTO.Requests;
using TouristAgency.DTO.Responses;
using TouristAgency.Exceptions;
using TouristAgency.Model;
using TouristAgency.Repositories;
using TouristAgency.RepositoryInterfaces;
using TouristAgency.RequestModel;
using TouristAgency.ServiceInterfaces;

namespace TouristAgency.Services
{
    public class TouristPackageService : ITouristPackageService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITouristPackageRepository _touristPackageRepository;
        private readonly ICategoryRepository _categoryRepository;
        public TouristPackageService(ITouristPackageRepository touristPackageRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _touristPackageRepository = touristPackageRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
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
                Images = package.Images, 
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
                    Price= trip.Price
                }).ToList(),
                Destination = package.Destination,
                Transportation = package.Transportation, 
                BasePrice = package.BasePrice,
                RoomPrices = package.RoomPrices 
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
  
        public async Task<InsertTouristPackageResponseDTO> AddTouristPackageAsync(InsertTouristPackageRequestDTO touristPackageRequestDTO, Guid userId)
        {
            var existingTouristPackage = await _touristPackageRepository.TouristPackageExsist(touristPackageRequestDTO.Name);
            if (existingTouristPackage != null)
            {
                throw new EntityAlreadyExistsException<TouristPackage>("name");
            }
            var currentUser = await _userRepository.GetUserByIdAsync(userId);

            Category existingCategory = await _categoryRepository.FindByIdAsync(touristPackageRequestDTO.CategoryId);
            if (existingCategory == null)
            {
                throw new EntityNotFoundException<Category>();
            }
           
            var package = new TouristPackage
            {
                Name = touristPackageRequestDTO.Name,
                Description = touristPackageRequestDTO.Description,
                Duration = touristPackageRequestDTO.Duration,
                DateOfDeparture = touristPackageRequestDTO.DateOfDeparture,
                ReturnDate = touristPackageRequestDTO.ReturnDate,
                Capacity = touristPackageRequestDTO.Capacity,
                Images = touristPackageRequestDTO.Images,
                Schedule = touristPackageRequestDTO.Schedule,
                PriceIncludes = touristPackageRequestDTO.PriceIncludes,
                PriceDoesNotIncludes = touristPackageRequestDTO.PriceDoesNotIncludes,
                Category = existingCategory,
                Organizer = (Organizer)currentUser,
                Tourists = touristPackageRequestDTO.Tourists,
                Transportation = touristPackageRequestDTO.Transportation,
                BasePrice = touristPackageRequestDTO.BasePrice,
                RoomPrices = touristPackageRequestDTO.RoomPrices,
                Destination=    touristPackageRequestDTO.Destination,
                Status = Model.TouristPackageStatus.Pending,
            };

            var createdPackage = await _touristPackageRepository.AddAsync(package);

            var packageDto = new InsertTouristPackageResponseDTO
            {
                Name = createdPackage.Name,
                Description = createdPackage.Description,
                Duration = createdPackage.Duration,
                DateOfDeparture = createdPackage.DateOfDeparture,
                ReturnDate = createdPackage.ReturnDate,
                Capacity = createdPackage.Capacity,
                Images = createdPackage.Images,
                Schedule = createdPackage.Schedule,
                PriceIncludes = createdPackage.PriceIncludes,
                PriceDoesNotIncludes = createdPackage.PriceDoesNotIncludes,
                CategoryId=existingCategory.Id,
                Organizer = createdPackage.Organizer,
                Destination = createdPackage.Destination,
                Tourists = createdPackage.Tourists,
                Transportation = createdPackage.Transportation,
                BasePrice = createdPackage.BasePrice,
                RoomPrices = createdPackage.RoomPrices,
                Status = createdPackage.Status
                
            };
            return packageDto;
        
        }
         public async Task<List<OneTouristPackageDTO>> GetPackagesByCreatorIdAsync(Guid creatorId)
         {
            var packages = await _touristPackageRepository.GetPackagesByCreatorIdAsync(creatorId);

            return packages.Select(package => new OneTouristPackageDTO
            {
                Id = package.Id,
                Name = package.Name,
                Description = package.Description,
                Duration = package.Duration,
                DateOfDeparture = package.DateOfDeparture,
                ReturnDate = package.ReturnDate,
                Images = package.Images ?? new List<string>(),
                Schedule = package.Schedule,
                PriceIncludes = package.PriceIncludes,
                PriceDoesNotIncludes = package.PriceDoesNotIncludes,
                Category = new CategoryForTorusitPackageDTO
                {
                    Id = package.Category.Id,
                    Name = package.Category.Name
                },
                Destination = package.Destination,
                Trips = package.Trips.Select(trip => new TripForTouristPackageDTO
                {
                    Id = trip.Id,
                    Name = trip.Name,
                    Description = trip.Description,
                    Price = trip.Price,
                }).ToList(),
                Transportation = package.Transportation,
                BasePrice = package.BasePrice,
                RoomPrices = package.RoomPrices ?? new List<double>()
            }).ToList();
         }

        public async Task DeletePackageAsync(Guid packageId, Guid organizerId)
        {
            
         
            await _touristPackageRepository.DeletePackageAsync(packageId, organizerId);
        }

        public async Task<bool> UpdateTouristPackageStatusAsync(Guid packageId, TouristPackageStatus newStatus)
        {
            var package = await _touristPackageRepository.FindByIdAsync(packageId);

            if (package == null)
            {
                return false;
            }

            package.Status = newStatus;
            await _touristPackageRepository.UpdateAsync(package);

            return true;
        }

        public async Task AddTripAsync(Trip trip)
        {
            if (trip == null)
            {
                throw new ArgumentNullException(nameof(trip), "Trip cannot be null.");
            }

            await _touristPackageRepository.AddTripAsync(trip);
        }

        public async Task<bool> UpdateTouristPackageAsync(Guid packageId, UpdateTouristPackage request)
        {
            var package = await _touristPackageRepository.FindByIdAsync(packageId);
            if (package == null)
            {
                return false;
            }

            // Ažuriranje samo onih polja koja su poslata
            if (!string.IsNullOrEmpty(request.Name))
                package.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Description))
                package.Description = request.Description;

            if (request.Duration.HasValue)
                package.Duration = request.Duration.Value;

            if (request.DateOfDeparture.HasValue)
                package.DateOfDeparture = request.DateOfDeparture.Value;

            if (request.ReturnDate.HasValue)
                package.ReturnDate = request.ReturnDate.Value;

            if (request.Transportation.HasValue)
                package.Transportation = (Transportation)request.Transportation;

            if (request.Images != null && request.Images.Count > 0)
                package.Images = request.Images;

            if (!string.IsNullOrEmpty(request.Schedule))
                package.Schedule = request.Schedule;

            if (!string.IsNullOrEmpty(request.PriceIncludes))
                package.PriceIncludes = request.PriceIncludes;

            if (!string.IsNullOrEmpty(request.PriceDoesNotIncludes))
                package.PriceDoesNotIncludes = request.PriceDoesNotIncludes;

            await _touristPackageRepository.UpdateTouristPackageAsync(package);
            return true;
        }
        public async Task<bool> UpdateDestinationAsync(Guid destinationId, UpdateDestination request)
        {
            var destination = await _touristPackageRepository.FindByDestinationIdAsync(destinationId);
            if (destination == null)
            {
                return false;
            }

            // Ažuriranje samo onih polja koja su poslata
            if (!string.IsNullOrEmpty(request.Name))
                destination.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Description))
                destination.Description = request.Description;

            if (!string.IsNullOrEmpty(request.Hotel))
                destination.Hotel = request.Hotel;

            if (request.HotelImages != null && request.HotelImages.Any())
                destination.HotelImages = request.HotelImages;

            await _touristPackageRepository.UpdateDestinationAsync(destination);
            return true;
        }

        public async Task<Trip> FindTripByIdAsync(Guid tripId)
        {
            return await _touristPackageRepository.FindTripByIdAsync(tripId);
        }

        public async Task DeleteTripAsync(Guid tripId)
        {
            await _touristPackageRepository.DeleteTripAsync(tripId);
        }



    }
}
