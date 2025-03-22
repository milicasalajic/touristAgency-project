using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.DTO.Requests;
using TouristAgency.RequestModel;
using TouristAgency.ResponseModel;
using TouristAgency.ServiceInterfaces;

namespace TouristAgency.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        //konstruktor koji se poziva kad program zatrazi kontroler
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //async-asihrono, dobro za rad s operacijama koje dugo traju, poput baze podataka
        //IActionResult - razlitice vrste http odgovora
        [HttpGet]
        public async Task<IActionResult> FindCategories()
        {
            //await se koristi kod asihronih metoda, za cekanje rezultata
            var categories = await _categoryService.FindAllAsync();
                            //prolazi kroz svaki odgovor iz baze i kreira novi objekat
            var response = categories.Select(p => new GetCategoriesResponse
            {
                Id = p.Id,
                Name = p.Name
            });
            return Ok(response);
        }

        [HttpGet("{id}/packages")]
        public async Task<IActionResult> GetTouristPackagesByCategoryId(Guid id)
        {
            var packages = await _categoryService.GetTouristPackagesByCategoryIdAsync(id);
            var response = packages.Select(p => new GetTouristPackagesResponse
            {
                Id = p.Id,
                Name = p.Name,
                DateOfDeparture = p.DateOfDeparture,
                ReturnDate = p.ReturnDate,
                BasePrice = p.BasePrice,
                 FirstImage= p.Images[0],
                Transportation = p.Transportation,
            });
            
            return Ok(response);
        }
        [HttpPost("add")]
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> AddCategory([FromBody] CreateNewCategoryRequest newCategoryRequest)
        {
            CategoryRequestDTO categoryRequestDTO = new CategoryRequestDTO()
            {
                Name = newCategoryRequest.Name,

            };
            var category = await _categoryService.AddCategoryAsync(categoryRequestDTO);
            CreateNewCategoryResponse response = new CreateNewCategoryResponse()
            {
                Name = category.Name,
            };
            return Ok(response);

        }
    }
}
