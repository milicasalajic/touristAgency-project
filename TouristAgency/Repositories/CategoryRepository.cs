using Microsoft.EntityFrameworkCore;
using TouristAgency.Data;
using TouristAgency.DTO.Responses;
using TouristAgency.Exceptions;
using TouristAgency.Model;
using TouristAgency.RepositoryInterfaces;

namespace TouristAgency.Repositories
{

                            //nasledjuje rad s entitetima iz RepositoryBase(u ovom slucaju Category)
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
         // za pristup bazi podataka
        public CategoryRepository(DataContext dataContext) : base(dataContext)
        {
        }
        public async Task<IEnumerable<AllCategoriesDTO>> FindAllAsync()
        {
            try
            {                               //sekejtuj sve stavke iz tabele i za njih kreiraj dto objekat
                return await _table.Select(item => new AllCategoriesDTO
                {
                    Id = item.Id,
                    Name = item.Name
                }).ToListAsync();
            }
            catch (Exception)
            {
                throw new DataRetrievalException<Category>();
            }
        }

        public async Task<IEnumerable<AllTouristPackagesDTO>> GetTouristPackagesByCategoryIdAsync(Guid categoryId)
        {
            try
            {
                return await _table
                    .Where(c => c.Id == categoryId)
                    .SelectMany(c => c.TouristPackages)
                    .Select(tp => new AllTouristPackagesDTO
                    {
                        Id = tp.Id,
                        Name = tp.Name,
                        ReturnDate = tp.ReturnDate,
                        DateOfDeparture = tp.DateOfDeparture,   
                        BasePrice = tp.BasePrice,
                        Images = tp.Images,
                        Transportation = tp.Transportation,
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw new EntityNotFoundException<TouristPackage>();
            }
        }
        
        public async Task<Category?> FindByNameAsync(string name)
        {
            try
            {
                var category = await _table.FirstOrDefaultAsync(x => x.Name == name);

                if (category != null)
                {
                    Console.WriteLine($"Found category: {category.Name}");
                    
                }
                return category;

            }
            catch (Exception)
            {
                throw new SingleEntityRetrievalException<Category>();
            }
        }

        public async Task<Category?> FindByIdAsync(Guid id)
        {
            try
            {
                var category = await _table.FirstOrDefaultAsync(x => x.Id == id);

                if (category != null)
                {
                    Console.WriteLine($"Found category: {category.Id}");

                }
                return category;

            }
            catch (Exception)
            {
                throw new SingleEntityRetrievalException<Category>();
            }
        }

        public async Task<Category> AddAsync(Category category)
        {
            try
            {
                await _table.AddAsync(category);
                await _dataContext.SaveChangesAsync();
                return category;
            }
            catch (Exception)
            {
                throw new EntityInsertException<Category>();
            }
        }


    }
}
