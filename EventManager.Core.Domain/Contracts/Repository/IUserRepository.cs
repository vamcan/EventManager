using EventManager.Core.Domain.Entities.User;

namespace EventManager.Core.Domain.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user, CancellationToken cancellationToken = default);
        Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
