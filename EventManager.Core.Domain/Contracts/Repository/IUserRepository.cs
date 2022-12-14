using EventManager.Core.Domain.Entities.User;

namespace EventManager.Core.Domain.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user,string password, CancellationToken cancellationToken = default);
        Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<User> FindByName(string userName, CancellationToken cancellationToken = default);
        Task<bool> CheckPassword(User user, string password, CancellationToken cancellationToken = default);
    }
}
