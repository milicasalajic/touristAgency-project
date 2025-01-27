using Microsoft.EntityFrameworkCore;
using TouristAgency.Data;
using TouristAgency.Exceptions;
using TouristAgency.Model;
using TouristAgency.RepositoryInterfaces;

namespace TouristAgency.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {
        }
        public async Task<User> FindByUsernameAsync(string username)
        {


            try
            {
                return await _table.FirstOrDefaultAsync(x => x.UserName == username);
            }
            catch (Exception)
            {
                throw new SingleEntityRetrievalException<User>();
            }
        }
    }
}
