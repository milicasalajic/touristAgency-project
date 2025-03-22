using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.DTO.Requests;
using TouristAgency.DTO.Responses;
using TouristAgency.Exceptions;
using TouristAgency.Model;
using TouristAgency.RepositoryInterfaces;
using TouristAgency.RequestModel;
using TouristAgency.ResponseModel;
using TouristAgency.ServiceInterfaces;
using TouristAgency.Services;

namespace TouristAgency.Controllers
{
    [ApiController]
    [Route("api/TouristPackage")]
    public class TouristPackageController : ControllerBase
    {

        private readonly ITouristPackageService _touristPackageService;
        private readonly IUserService _userService;
        private readonly ITouristPackageRepository _touristPackageRepository;
       
        public TouristPackageController(ITouristPackageService touristPackageService, IUserService userService, ITouristPackageRepository touristPackageRepository)
        {
            _userService = userService;
            _touristPackageService = touristPackageService;
            _touristPackageRepository = touristPackageRepository;
        }

        [HttpGet("packages")]
        public async Task<IActionResult> GetPackages()
        {
           
            var packages = await _touristPackageService.GetAllAsync();
           
            var response = packages.Select(p => new GetTouristPackagesResponse
            {
                Id = p.Id,
                Name = p.Name,
                DateOfDeparture = p.DateOfDeparture,
                ReturnDate = p.ReturnDate,
                BasePrice = p.BasePrice,
                FirstImage= p.Images[0],
                Transportation=  p.Transportation,

            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTouristPackageById(Guid id)
        {
            var package = await _touristPackageService.FindByIdAsync(id);
            GetSingleTouristPackageResponse packageResponse = new GetSingleTouristPackageResponse()
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
                Category = package.Category,
                Destination = package.Destination,
                // Trips = package.Trips ?? new List<Trip>(),
                Trips = package.Trips?.Select(trip => new TripForTouristPackageDTO
                {
                    Id = trip.Id,
                    Name = trip.Name,
                    Description = trip.Description,
                    Price = trip.Price
                }).ToList(),

                Transportation = package.Transportation,
                BasePrice = package.BasePrice,
                RoomPrices = package.RoomPrices ?? new List<double>()
            };
            return Ok(packageResponse);
        }

        [HttpGet("filter-by-price-ascending")]
        public async Task<IActionResult> GetPackagesByPriceAscending()
        {
            var packages = await _touristPackageService.GetPackagesOrderedByPriceAsync();

            var response = packages.Select(p => new GetTouristPackagesResponse
            {
                Id = p.Id,
                Name = p.Name,
                DateOfDeparture = p.DateOfDeparture,
                ReturnDate = p.ReturnDate,
                BasePrice = p.BasePrice,
                FirstImage = p.Images[0],
                Transportation = p.Transportation,
            });

            return Ok(response);
        }

        [HttpGet("filter-by-price-descending")]
        public async Task<IActionResult> GetPackagesByPriceDescending()
        {
            var packages = await _touristPackageService.GetPackagesOrderedByPriceDescendingAsync();

            var response = packages.Select(p => new GetTouristPackagesResponse
            {
                Id = p.Id,
                Name = p.Name,
                DateOfDeparture = p.DateOfDeparture,
                ReturnDate = p.ReturnDate,
                BasePrice = p.BasePrice,
                FirstImage = p.Images[0],
                Transportation = p.Transportation,
            });

            return Ok(response);
        }

        [HttpGet("filter-by-date-range")]
        public async Task<IActionResult> GetPackagesByDateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Start date cannot be later than end date.");
            }

            var packages = await _touristPackageService.GetPackagesByDateRangeAsync(startDate, endDate);
            return Ok(packages);
        }
        
        [HttpPost("add")]
        [Authorize(Roles ="Organizer")]
        public async Task<IActionResult> AddTouristPackage([FromBody] InsertTouristPackageRequest newTouristPackage)
        {

          
           var userId = User.FindFirstValue("id");
         
            if (userId != null && Guid.TryParse(userId, out Guid id))
            {
                Console.WriteLine($"User ID from token: {userId}");

                // Pronalazak korisnika iz baze
                var currentUser = await _userService.GetUserByIdAsync(id);

                
                InsertTouristPackageRequestDTO touristPackageRequestDTO = new InsertTouristPackageRequestDTO()
                {
                    Name = newTouristPackage.Name,
                    Description = newTouristPackage.Description,
                    Duration = newTouristPackage.Duration,
                    DateOfDeparture = newTouristPackage.DateOfDeparture,
                    ReturnDate = newTouristPackage.ReturnDate,
                    Capacity = newTouristPackage.Capacity,
                    Images = newTouristPackage.Images,
                    Schedule = newTouristPackage.Schedule,
                    PriceIncludes = newTouristPackage.PriceIncludes,
                    PriceDoesNotIncludes = newTouristPackage.PriceDoesNotIncludes,
                    CategoryId = newTouristPackage.CategoryId,
                    Destination    = newTouristPackage.Destination,
                    Tourists = new List<Tourist>(),
                    Transportation = newTouristPackage.Transportation,
                    BasePrice = newTouristPackage.BasePrice,
                    RoomPrices = newTouristPackage.RoomPrices,
                    Status = Model.TouristPackageStatus.Pending,

                };
                var touristPackage = await _touristPackageService.AddTouristPackageAsync(touristPackageRequestDTO, id);
                InsertTouristPackageResponse response = new InsertTouristPackageResponse()
                {
                    Name = touristPackage.Name,
                    Description = touristPackage.Description,
                    Duration = touristPackage.Duration,
                    DateOfDeparture = touristPackage.DateOfDeparture,
                    ReturnDate = touristPackage.ReturnDate,
                    Capacity = touristPackage.Capacity,
                    Images = touristPackage.Images,
                    Schedule = touristPackage.Schedule,
                    PriceIncludes = touristPackage.PriceIncludes,
                    PriceDoesNotIncludes = touristPackage.PriceDoesNotIncludes,
                    CategoryId = touristPackage.CategoryId,
                    Organizer = touristPackage.Organizer,
                    Tourists = touristPackage.Tourists,
                    Transportation = touristPackage.Transportation,
                    Destination = touristPackage.Destination,
                    BasePrice = touristPackage.BasePrice,
                    RoomPrices = touristPackage.RoomPrices,
                   Status = Model.TouristPackageStatus.Pending,
                };
                return Ok(response);
            }
            throw new InvalidUserIdException();
        }

        [HttpGet("my-created-packages")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetCreatedPackages()
        {
            var userIdString = User.FindFirst("id")?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized(new { Message = "Neuspešno prepoznavanje korisnika." });
            }

            var packages = await _touristPackageService.GetPackagesByCreatorIdAsync(userId);

            var response = packages.Select(package => new GetSingleTouristPackageResponse
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

            return Ok(response);
        }

        [HttpDelete("delete/{packageId}")]
        [Authorize(Roles = "Organizer, Admin")]
        public async Task<IActionResult> DeletePackage(Guid packageId)
        {
            var userIdString = User.FindFirst("id")?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out Guid organizerId))
            {
                throw new EntityNotFoundException<User>();
            }

            try
            {
                await _touristPackageService.DeletePackageAsync(packageId, organizerId);
                return Ok(new { Message = "Paket je uspešno obrisan." });
            }
            catch (EntityNotFoundException<TouristPackage>)
            {
                throw new EntityNotFoundException<TouristPackage>();
            }
        }

        [HttpPut("update-status/{packageId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTouristPackageStatus(Guid packageId, [FromBody] UpdateTouristPackageStatusRequest request)
        {
          
            if (!Enum.IsDefined(typeof(TouristPackageStatus), request.Status))
            {
                return BadRequest("Invalid status value.");
            }

            var success = await _touristPackageService.UpdateTouristPackageStatusAsync(packageId, request.Status);

            if (!success)
            {
                throw new EntityNotFoundException<TouristPackage>();
            }
            return Ok("Tourist package status updated successfully.");
        }

        [HttpPost("addTrips/{touristPackageId}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> AddTripsToPackage(Guid touristPackageId, [FromBody] List<InsertTripsRequest> tripsRequest)
        {
            Console.WriteLine($"Tourist Package ID: {touristPackageId}");
            var userId = User.FindFirstValue("id");

            if (userId != null && Guid.TryParse(userId, out Guid id))
            {

                var currentUser = await _userService.GetUserByIdAsync(id);

                var touristPackage = await _touristPackageRepository.FindByIdAsync(touristPackageId);

                if (touristPackage == null)
                {
                    throw new EntityNotFoundException<TouristPackage>();
                }

                foreach (var tripRequest in tripsRequest)
                {
                    var trip = new Trip
                    {
                        Name = tripRequest.Name,
                        Description = tripRequest.Description,
                        Price = tripRequest.Price,
                        TouristPackage = touristPackage 
                    };

                    await _touristPackageService.AddTripAsync(trip); 
                }

                return Ok("Trips added successfully.");
            }
            throw new InvalidUserIdException();
        }

        [HttpPut("update-package/{packageId}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> UpdateTouristPackage(Guid packageId, [FromBody] UpdateTouristPackage request)
        {

     
            var success = await _touristPackageService.UpdateTouristPackageAsync(packageId, request);

            if (!success)
            {
                throw new EntityNotFoundException<TouristPackage>();
            }
            return Ok("Tourist package updated successfully.");
        }

        [HttpPut("update-destination/{destinationId}")]
        [Authorize(Roles = "Organizer, Admin")]
        public async Task<IActionResult> UpdateDestination(Guid destinationId, [FromBody] UpdateDestination request)
        {


            var success = await _touristPackageService.UpdateDestinationAsync(destinationId, request);

            if (!success)
            {
                throw new EntityNotFoundException<Destination>();
            }
            return Ok("Tourist package updated successfully.");
        }

        [HttpDelete("delete-trip/{tripId}")]
        [Authorize(Roles = "Organizer, Admin")]
        public async Task<IActionResult> DeleteTrip(Guid tripId)
        {
            var userIdString = User.FindFirst("id")?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out Guid organizerId))
            {
                throw new EntityNotFoundException<User>();
            }

            try
            {
                // Pronalazak Trip-a na osnovu ID-a
                var trip = await _touristPackageRepository.FindTripByIdAsync(tripId);

                if (trip == null)
                {
                    throw new EntityNotFoundException<Trip>();
                }

                // Brisanje Trip-a
                await _touristPackageService.DeleteTripAsync(tripId);

                return Ok(new { Message = "Trip je uspešno obrisan." });
            }
            catch (EntityNotFoundException<Trip>)
            {
                throw new EntityNotFoundException<Trip>();
            }
        }

    }
}
