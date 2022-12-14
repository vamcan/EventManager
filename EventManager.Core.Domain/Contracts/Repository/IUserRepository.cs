using EventManager.Core.Domain.Entities.User;

namespace EventManager.Core.Domain.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user,string password);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> FindByName(string userName);
        Task<bool> CheckPassword(User user, string password);
    }
}
