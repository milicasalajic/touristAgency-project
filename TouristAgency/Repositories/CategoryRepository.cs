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
        public async Task<IEnumerable<AllCategoriesDTO>> GetAllAsync()
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
    }
}
