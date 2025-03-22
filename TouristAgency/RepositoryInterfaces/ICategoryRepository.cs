using TouristAgency.DTO.Responses;
using TouristAgency.Model;

namespace TouristAgency.RepositoryInterfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> FindByNameAsync(string name);
        Task<Category?> FindByIdAsync(Guid id);
        Task<Category> AddAsync(Category category);
        Task<IEnumerable<AllCategoriesDTO>> FindAllAsync();
        Task<IEnumerable<AllTouristPackagesDTO>> GetTouristPackagesByCategoryIdAsync(Guid categoryId);

    }
}
