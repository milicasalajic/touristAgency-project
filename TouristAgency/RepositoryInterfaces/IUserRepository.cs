using TouristAgency.Model;

namespace TouristAgency.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByUserNameAsync(string username);
        Task<User> FindByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(Guid id);
        Task<bool> InsertAsync(User user);
        Task<bool> UpdateUserAsync(User user);
       
    }
}
