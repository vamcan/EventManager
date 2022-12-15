using EventManager.Core.Domain.Entities.User;

namespace EventManager.Core.Domain.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user, CancellationToken cancellationToken = default);
        Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<User?> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    }
}
