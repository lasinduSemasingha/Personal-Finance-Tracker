using PersonalFinanceTracker.DTO;
using PersonalFinanceTracker.Entities;

namespace PersonalFinanceTracker.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<string?> AuthenticateUser(AuthenticationCredentials request);
        Task<bool> CreateUser(CreateUser request);
    }
}
