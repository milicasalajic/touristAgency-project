namespace TouristAgency.ServiceInterfaces
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);
    }
}
