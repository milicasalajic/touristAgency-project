using TouristAgency.Model;

namespace TouristAgency.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User> FindByUsernameAsync(string username);
    }
}
