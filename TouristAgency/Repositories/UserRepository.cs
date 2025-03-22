using Microsoft.AspNetCore.Identity;
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
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            try
            {
                
                return await _table.FirstOrDefaultAsync(x => x.UserId == id);
                
            }
            catch (Exception)
            {
                throw new SingleEntityRetrievalException<User>();
            }
        }
        public async Task<bool> UpdateUserAsync(User user)
        {
            _dataContext.Users.Update(user);
            return await _dataContext.SaveChangesAsync() > 0;
        }
        public async Task<User> FindByEmailAsync(string email)
        {
            try
            {
                return await _table.FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception)
            {
                throw new SingleEntityRetrievalException<User>();
            }
        }

        public async Task<User> FindByUserNameAsync(string username)
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
        public async Task<bool> InsertAsync(User user)
        {
            try
            {
                await _table.AddAsync(user);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška prilikom dodavanja korisnika: {ex.Message}");
                throw new EntityInsertException<User>();
            }
        }

    }
}
