using Microsoft.AspNetCore.Mvc;
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
    }
}
