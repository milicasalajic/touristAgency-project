using Microsoft.AspNetCore.Mvc;
using TouristAgency.DTO.Responses;
using TouristAgency.ResponseModel;
using TouristAgency.ServiceInterfaces;

namespace TouristAgency.Controllers
{
    [ApiController]
    [Route("api/TouristPackage")]
    public class TouristPackageController : ControllerBase
    {

        private readonly ITouristPackageService _touristPackageService;

        //konstruktor koji se poziva kad program zatrazi kontroler
        public TouristPackageController(ITouristPackageService touristPackageService)
        {
            _touristPackageService = touristPackageService;
        }

        [HttpGet("packages")]
        public async Task<IActionResult> GetPackages()
        {
            //await se koristi kod asihronih metoda, za cekanje rezultata
            var packages = await _touristPackageService.GetAllAsync();
            //prolazi kroz svaki odgovor iz baze i kreira novi objekat
            var response = packages.Select(p => new GetTouristPackagesResponse
            {
                Id = p.Id,
                Name = p.Name,
                DateOfDeparture = p.DateOfDeparture,
                ReturnDate = p.ReturnDate,
                BasePrice = p.BasePrice,

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
                Images = package.Images ?? new List<string>(), // Ako su slike null, koristi praznu listu
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
                }).ToList(),

                Transportation = package.Transportation,
                BasePrice = package.BasePrice,
                RoomPrices = package.RoomPrices ?? new List<double>() // Ako je null, koristi praznu listu
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
    }
}
