using TouristAgency.DTO.Responses;
using TouristAgency.RepositoryInterfaces;
using TouristAgency.ServiceInterfaces;

namespace TouristAgency.Services
{
    public class CategoryService : ICategoryService
    {
        //znaci categoryservice implementira interfejs
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<AllCategoriesDTO>> GetAllAsync()
        {
            var categoryDtos = await _categoryRepository.GetAllAsync();
            return categoryDtos;
        }
        public async Task<IEnumerable<AllTouristPackagesDTO>> GetTouristPackagesByCategoryIdAsync(Guid categoryId)
        {
           
            var packagesDto = await _categoryRepository.GetTouristPackagesByCategoryIdAsync(categoryId);
            return packagesDto;
        }
    }
}
