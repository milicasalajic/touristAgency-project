using TouristAgency.DTO.Requests;
using TouristAgency.DTO.Responses;
using TouristAgency.Model;

namespace TouristAgency.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<AllCategoriesDTO>> FindAllAsync();
        Task<IEnumerable<AllTouristPackagesDTO>> GetTouristPackagesByCategoryIdAsync(Guid categoryId);
        Task<CategoryResponseDTO> AddCategoryAsync(CategoryRequestDTO categoryRequestDTO);
        Task<Category> GetCategoryByNameAsync(string name);
    }
}
