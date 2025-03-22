using TouristAgency.DTO.Requests;
using TouristAgency.DTO.Responses;
using TouristAgency.Exceptions;
using TouristAgency.Model;
using TouristAgency.RepositoryInterfaces;
using TouristAgency.ServiceInterfaces;

namespace TouristAgency.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<AllCategoriesDTO>> FindAllAsync()
        {
            var categoryDtos = await _categoryRepository.FindAllAsync();
            return categoryDtos;
        }

        public async Task<IEnumerable<AllTouristPackagesDTO>> GetTouristPackagesByCategoryIdAsync(Guid categoryId)
        {
           
            var packagesDto = await _categoryRepository.GetTouristPackagesByCategoryIdAsync(categoryId);
            return packagesDto;
        }

        public async Task<CategoryResponseDTO> AddCategoryAsync(CategoryRequestDTO categoryRequestDTO)
        {
           
            var existingCategory = await _categoryRepository.FindByNameAsync(categoryRequestDTO.Name);
            if (existingCategory != null)
            {
                throw new EntityAlreadyExistsException<Category>("name");
            }
            var category = new Category
            {
                Name = categoryRequestDTO.Name,
             
            };
            var createdCategory = await _categoryRepository.AddAsync(category);

            CategoryResponseDTO categoryDto = new CategoryResponseDTO
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name
             
            };

            return categoryDto;
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _categoryRepository.FindByNameAsync(name);
        }

    }
}
